using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieFly : MonoBehaviour
{

    public int health;
    public float speed;
    public float shootingRate;
    public GameObject bulletPrefab;
    public Transform shootingPosition;
    public Transform player;
    public float attackDistance;
    public float amplitude;
    public float frequency;
    public Slider healthBar;
    public GameObject deathAnimation;
    public GameObject damageAudioGO;
    public GameObject dieAudioGO;
    private AudioSource damageAudio;
    private AudioSource dieAudio;

    private SpriteRenderer sprite;
    private bool facingRight = false;
    private float shootCooldown;
    private Vector3 startPosition;
    private List<DateTime> bombs = new List<DateTime>();

    void Start()
    {
        shootCooldown = 0f;
        startPosition = transform.position;
        sprite = GetComponent<SpriteRenderer>();
        damageAudio = damageAudioGO.GetComponent<AudioSource>();
        dieAudio = dieAudioGO.GetComponent<AudioSource>();
    }

    void Update()
    {
        MoveEnemy();
        Shoot();
    }

    void MoveEnemy()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < attackDistance)
        {
            float direction = Mathf.Sign(player.position.x - transform.position.x);
            if (direction > 0 && !facingRight)
                Flip();
            else if (direction < 0 && facingRight)
                Flip();
            transform.position += new Vector3(direction * speed * Time.deltaTime, Mathf.Sin((Time.time * frequency) * 2 * Mathf.PI) * amplitude * Time.deltaTime, 0f);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        Vector3 barScale = healthBar.transform.localScale;
        barScale.x *= -1;
        healthBar.transform.localScale = barScale;
    }

    void Shoot()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
        else
        {
            shootCooldown = 5f;
            GameObject bullet = Instantiate(bulletPrefab, shootingPosition.position, Quaternion.identity);
            ZombieBullet bulletScript = bullet.GetComponent<ZombieBullet>();
            bulletScript.direction = Vector3.down;
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(health);
        health -= damage;
        Debug.Log(health);
        healthBar.value = health;
        if (health <= 0)
        {
            PlayerPrefs.SetFloat("Zombies", PlayerPrefs.GetFloat("Zombies", 0) + 1);
            PlayerPrefs.Save();
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
        for (int i = 0; i < bombs.Count; i++)
        {
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
