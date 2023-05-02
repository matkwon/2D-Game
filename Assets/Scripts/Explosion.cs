using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Explosion : MonoBehaviour
{
    GameManager gameManager;
    private DateTime tag;

    // Start is called before the first frame update
    void Start()
    {
        tag = DateTime.Now;
        gameManager = GameManager.gameManager;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy otherEnemy = other.GetComponent<Enemy>();
        if (otherEnemy != null)
        {
            otherEnemy.TakeBombDamage(gameManager.playerDamage, tag);
        }
    }
}
