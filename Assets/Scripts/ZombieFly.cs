using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFly : MonoBehaviour
{
    public float speed;
    public float shootingRate;
    public GameObject bulletPrefab;
    public Transform shootingPosition;
    public Transform player;
    public float stopDistance;
    public float amplitude;
    public float frequency;

    private float shootCooldown;
    private Vector3 startPosition;

    void Start()
    {
        shootCooldown = 0f;
        startPosition = transform.position;
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
        if (distanceToPlayer > stopDistance)
        {
            float direction = Mathf.Sign(player.position.x - transform.position.x);
            transform.position += new Vector3(direction * speed * Time.deltaTime, Mathf.Sin((Time.time * frequency) * 2 * Mathf.PI) * amplitude * Time.deltaTime, 0f);
        }
    }

    void Shoot()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, shootingPosition.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
        }
    }
}
