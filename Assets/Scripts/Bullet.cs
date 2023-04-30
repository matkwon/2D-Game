using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 10;
    public float lifeTime = 1.5f;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
        gameManager = GameManager.gameManager;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy otherEnemy = other.GetComponent<Enemy>();
        Player otherPlayer = other.GetComponent<Player>();
        if (otherEnemy != null)
        {
            otherEnemy.TakeDamage(gameManager.playerDamage);
            Destroy(gameObject);
        }
        if (otherPlayer != null)
        {
            otherPlayer.TakeDamage(gameManager.enemyDamage);
            Destroy(gameObject);
        }
        
    }
}
