using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour
{
    public GameObject explosion;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy otherEnemy = other.GetComponent<Enemy>();
        ZombieFly otherFly = other.GetComponent<ZombieFly>();
        if (otherEnemy != null || otherFly != null || other.tag == "Ground" || other.tag == "Spike")
        {
            if (otherEnemy != null || otherFly != null) Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
