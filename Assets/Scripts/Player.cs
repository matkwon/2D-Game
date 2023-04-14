using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 5f;
    public float jumpForce = 600;
    public GameObject bulletPrefab;
    public Transform shotSpawner;

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
    private int maxHealth;

    private bool isDead = false;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        anim = GetComponent<Animator>();

        gameManager = GameManager.gameManager;

        SetPlayerStatus();
        health = maxHealth;
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
                GameObject tempbullet = Instantiate(bulletPrefab, shotSpawner.position, shotSpawner.rotation);
                if (crouched && !onGround) {
                    tempbullet.transform.eulerAngles = new Vector3(0,0,-90);
                    if (!facingRight)
                        tempbullet.GetComponent<Bullet>().transform.Translate((float)-5,(float)-4.3,0);
                    else
                        tempbullet.GetComponent<Bullet>().transform.Translate((float)-5,(float)-2.8,0);
                }
                else if (!facingRight && !lookingUp)
                {
                    tempbullet.GetComponent<Bullet>().speed *= -1;
                    tempbullet.GetComponent<Bullet>().transform.Translate((float)-3.5,0,0);
                }
                else if (!facingRight && lookingUp)
                {
                    tempbullet.transform.eulerAngles = new Vector3(0,0,90);
                    tempbullet.GetComponent<Bullet>().transform.Translate((float)3.5,-3,0);
                }
                else if (facingRight && lookingUp)
                {
                    tempbullet.transform.eulerAngles = new Vector3(0,0,90);
                    tempbullet.GetComponent<Bullet>().transform.Translate((float)3.5,(float)-4.3,0);
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
    }

    public void SetPlayerStatus()
    {
        fireRate = gameManager.fireRate;
        maxHealth = gameManager.health;
    }
}
