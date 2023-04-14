using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{

    public GameObject bulletPrefab;
    public Transform shotSpawner;
    public float nextFire;

    private float lastFire = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (targetDistance < attackDistance && targetDistance > -attackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if (Time.time - lastFire > nextFire)
            {
                Shoot();
                lastFire = Time.time;
            }
        }
    }

    public void Shoot()
    {
        GameObject tempbullet = Instantiate(bulletPrefab, shotSpawner.position, shotSpawner.rotation);
        if (facingRight)
        {
            tempbullet.GetComponent<Bullet>().speed *= -1;
            tempbullet.GetComponent<Bullet>().transform.Translate((float)-0.5,0,0);
        }
    }
}
