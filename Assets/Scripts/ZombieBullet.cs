using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBullet : MonoBehaviour
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
        Player otherPlayer = other.GetComponent<Player>();
        if (otherPlayer != null || other.tag == "Ground" || other.tag == "Spike")
        {
            if (otherPlayer != null) otherPlayer.TakeDamage(gameManager.enemyDamage);
            Destroy(gameObject);
        }
        
    }
}
