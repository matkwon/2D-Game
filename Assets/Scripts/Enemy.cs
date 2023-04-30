using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public int health;
    public float speed;
    public float attackDistance;
    public GameObject deathAnimation;
    public Slider healthBar;

    protected Animator anim;
    protected bool facingRight = false;
    protected Transform target;
    protected float targetDistance;
    protected Rigidbody2D rb;
    protected SpriteRenderer sprite;

    GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        target = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        targetDistance = transform.position.x - target.position.x;
        if(transform.position.x > target.position.x && facingRight)
        {
            Flip();
        }
        else if(transform.position.x < target.position.x && !facingRight)
        {
            Flip();
        }
    }

    protected void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        Vector3 barScale = healthBar.transform.localScale;
        barScale.x *= -1;
        healthBar.transform.localScale = barScale;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.value = health;
        if (health <= 0)
        {
            Instantiate(deathAnimation, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(TakeDamageCoroutine());
        }
    }

    IEnumerator TakeDamageCoroutine()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
