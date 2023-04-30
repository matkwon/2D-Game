using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI coinsText;
    public Slider healthBar;
    public float speed = 5f;
    public float jumpForce = 600;
    public GameObject bulletPrefab;
    public GameObject laserPrefab;
    public GameObject bombPrefab;
    public Transform shotSpawner;
    public Transform laserSpawner;
    public Image bombImage;
    public Image laserImage;
    public GameObject background;
    public GameObject shotAudioGameObject;
    private AudioSource shotAudioSource;

    private GameObject shotPrefab;
    private Animator anim;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool jump;
    private bool onGround = false;
    private Transform groundCheck;
    private float hforce = 0;
    private bool crouched;
    private bool lookingUp;
    private bool reloading;
    private float fireRate = 0.5f;
    private float nextFire;

    private int health;
    private int coins;
    private int maxHealth;

    private enum SpecialType{None, Bomb, Laser}
    private SpecialType currentSpecial = SpecialType.None;

    private bool isDead;

    protected SpriteRenderer sprite;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        anim = GetComponent<Animator>();

        gameManager = GameManager.gameManager;

        isDead = false;
        SetPlayerStatus();
        health = maxHealth;
        coins = gameManager.coins;

        bombImage.color = new Color(0,0,0,0);
        laserImage.color = new Color(0,0,0,0);

        shotPrefab = bulletPrefab;
        shotAudioSource = shotAudioGameObject.GetComponent<AudioSource>();

        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead) {

            onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
            
            if (onGround)
            {
                anim.SetBool("Jump", false);
            }
            
            if (Input.GetButtonDown("Jump") && onGround && !reloading)
            {
                jump = true;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if(rb.velocity.y > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                }
            }

            if (Input.GetButtonDown("Crouch"))
            {
                anim.SetBool("Crouched", true);
            }
            if (Input.GetButtonDown("Reload"))
            {
                anim.SetBool("Reloading", true);
            }

            if (Input.GetButtonDown("Shoot") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                anim.SetTrigger("Shoot");
                GameObject tempbullet;
                if (currentSpecial == SpecialType.Laser)
                {
                    tempbullet = Instantiate(shotPrefab, laserSpawner.position, laserSpawner.rotation);
                }
                else
                    tempbullet = Instantiate(shotPrefab, shotSpawner.position, shotSpawner.rotation);
                if (crouched && !onGround) {
                    tempbullet.transform.eulerAngles = new Vector3(0,0,0);
                    if (!facingRight)
                        if (currentSpecial == SpecialType.Laser)
                        {
                            tempbullet.GetComponent<LaserBullet>().speed *= -1;
                            tempbullet.GetComponent<LaserBullet>().transform.Translate((float)-3.3,(float)-3.8,0);
                        }
                        else
                        {
                            tempbullet.GetComponent<Bullet>().speed *= -1;
                            tempbullet.GetComponent<Bullet>().transform.Translate((float)-3.3,(float)-3.8,0);
                        }
                    else
                        if (currentSpecial == SpecialType.Laser)
                            tempbullet.GetComponent<LaserBullet>().transform.Translate((float)1,(float)-3.8,0);
                        else
                            tempbullet.GetComponent<Bullet>().transform.Translate((float)-1,(float)-3.8,0);
                }
                else if (!facingRight && !lookingUp)
                    if (currentSpecial == SpecialType.Laser)
                    {
                        tempbullet.GetComponent<LaserBullet>().speed *= -1;
                        tempbullet.GetComponent<LaserBullet>().transform.Translate((float)0.0,0,0);
                    }
                    else
                    {
                        tempbullet.GetComponent<Bullet>().speed *= -1;
                        tempbullet.GetComponent<Bullet>().transform.Translate((float)-3.5,0,0);
                    }
                else if (!facingRight && lookingUp)
                    if (currentSpecial == SpecialType.Laser)
                    {
                        tempbullet.transform.eulerAngles = new Vector3(0,0,90);
                        tempbullet.GetComponent<LaserBullet>().transform.Translate((float)1.6,(float)-1.0,0);
                    }
                    else
                    {
                        tempbullet.transform.eulerAngles = new Vector3(0,0,90);
                        tempbullet.GetComponent<Bullet>().transform.Translate((float)3.5,-3,0);
                    }
                else if (facingRight && lookingUp)
                    if (currentSpecial == SpecialType.Laser)
                    {
                        tempbullet.transform.eulerAngles = new Vector3(0,0,90);
                        tempbullet.GetComponent<LaserBullet>().transform.Translate((float)1.6,(float)1.1,0);
                    }
                    else
                    {
                        tempbullet.transform.eulerAngles = new Vector3(0,0,90);
                        tempbullet.GetComponent<Bullet>().transform.Translate((float)3.5,(float)-4.3,0);
                    }
                shotAudioSource.Play();
            }

            if (Input.GetButtonDown("Special"))
            {
                if (gameManager.currentSpecial == GameManager.SpecialType.Bomb)
                {
                    shotPrefab = bombPrefab;
                    currentSpecial = SpecialType.Bomb;
                }
                else if (gameManager.currentSpecial == GameManager.SpecialType.Laser)
                {
                    shotPrefab = laserPrefab;
                    currentSpecial = SpecialType.Laser;
                }
            }

            lookingUp = Input.GetButton("Up");
            crouched = Input.GetButton("Down");
            reloading = Input.GetButton("Reload");

            anim.SetBool("LookUp", lookingUp);
            anim.SetBool("Crouched", crouched);
            anim.SetBool("Reloading", reloading);

            if ((crouched || lookingUp || reloading) && onGround)
            {
                hforce = 0;
            }

        }
    }

    private void FixedUpdate()
    {
        if (!isDead) {

            if (!crouched && !lookingUp && !reloading)
            hforce = Input.GetAxisRaw("Horizontal");

            anim.SetFloat("Speed", Mathf.Abs(hforce));
            
            rb.velocity = new Vector2(hforce * speed, rb.velocity.y);

            if(hforce > 0 && !facingRight)
            {
                Flip();
            }
            else if(hforce < 0 && facingRight)
            {
                Flip();
            }
            if (jump)
            {
                anim.SetBool("Jump", true);
                jump = false;
                rb.AddForce(Vector2.up * jumpForce);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        Vector3 bgScale = background.transform.localScale;
        bgScale.x *= -1;
        background.transform.localScale = bgScale;
    }
    
    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            health -= damage;
            healthText.text = health.ToString();
            healthBar.value = health;
            if (health <= 0) Death();
            else StartCoroutine(TakeDamageCoroutine());
        }
    }

    public void Death()
    {
        health = 0;
        anim.SetTrigger("Death");
        isDead = true;
        SceneManager.LoadScene("Death");
    }
    
    public void SpikeDamage(int damage)
    {
        TakeDamage(damage);
        if (!isDead) rb.AddForce(Vector2.up * jumpForce * 0.4f);
    }
    
    public void Heal(int increase)
    {
        health += increase;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthText.text = health.ToString();
        healthBar.value = health;
        StartCoroutine(HealCoroutine());
    }

    public void GetCoin()
    {
        coins++;
        coinsText.text = coins.ToString();
    }

    IEnumerator TakeDamageCoroutine()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
    IEnumerator HealCoroutine()
    {
        sprite.color = Color.green;
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.white;
    }

    public void SetPlayerStatus()
    {
        fireRate = gameManager.fireRate;
        maxHealth = gameManager.health;
    }

}
