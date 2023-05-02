using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Enemy : MonoBehaviour
{

    public int health;
    public float speed;
    public float attackDistance;
    public GameObject deathAnimation;
    public Slider healthBar;
    public GameObject damageAudioGO;
    public GameObject dieAudioGO;
    private AudioSource damageAudio;
    private AudioSource dieAudio;

    protected Animator anim;
    protected bool facingRight = false;
    protected Transform target;
    protected float targetDistance;
    protected Rigidbody2D rb;
    protected SpriteRenderer sprite;
    protected GameManager gameManager;

    private List<DateTime> bombs = new List<DateTime>();

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        target = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        damageAudio = damageAudioGO.GetComponent<AudioSource>();
        dieAudio = dieAudioGO.GetComponent<AudioSource>();
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
            GameManager.gameManager.zombies = GameManager.gameManager.zombies + 1;
            Instantiate(deathAnimation, transform.position, transform.rotation);
            gameObject.SetActive(false);
            dieAudio.Play();
        }
        else
        {
            StartCoroutine(TakeDamageCoroutine());
            damageAudio.Play();
        }
    }

    public void TakeBombDamage(int damage, DateTime tag)
    {
        Debug.Log("A");
        Debug.Log(tag);
        for (int i = 0; i < bombs.Count; i++)
        {
            Debug.Log("B");
            Debug.Log(bombs[i]);
            if (bombs[i] == tag) return;
        }
        bombs.Add(tag);
        TakeDamage(damage);
    }

    IEnumerator TakeDamageCoroutine()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
