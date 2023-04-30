using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour
{
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy otherEnemy = other.GetComponent<Enemy>();
        if (otherEnemy != null)
        {
            otherEnemy.TakeDamage(gameManager.playerDamage+2);
            Destroy(gameObject);
        }
    }
}
