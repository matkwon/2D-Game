using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 10;
    public int damage = 1;
    public float lifeTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy otherEnemy = other.GetComponent<Enemy>();
        if (otherEnemy != null)
        {
            otherEnemy.TakeDamage(damage);
        }
        else
        {
            Debug.Log("Hit something else");
        }
        
        Destroy(gameObject);
    }
}
