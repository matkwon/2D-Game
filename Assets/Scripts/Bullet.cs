using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 10;
    public float lifeTime = 1.5f;
    public Vector3 direction = Vector3.right;

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
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy otherEnemy = other.GetComponent<Enemy>();
        ZombieFly otherFly = other.GetComponent<ZombieFly>();
        if (otherEnemy != null || otherFly != null || other.tag == "Ground" || other.tag == "Spike")
        {
            if (otherEnemy != null) otherEnemy.TakeDamage(gameManager.playerDamage);
            else if (otherFly != null) otherFly.TakeDamage(gameManager.playerDamage);
            Destroy(gameObject);
        }
        
    }
}
