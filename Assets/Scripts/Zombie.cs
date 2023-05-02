using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{

    public GameObject bulletPrefab;
    public Transform shotSpawner;
    public float nextFire;

    private Animator animZ;
    private float lastFire = 0f;
    private float lastBreath = -0.5f;
    private 

    // Start is called before the first frame update
    void Start()
    {
        animZ = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (targetDistance < attackDistance && targetDistance > -attackDistance)
        {
            if (Time.time - lastFire > nextFire - 0.5f)
            {
                animZ.SetBool("BuildUp", true);
            } else
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if (Time.time - lastFire > nextFire)
            {
                animZ.SetBool("BuildUp", false);
                Shoot();
                lastFire = Time.time;
                lastBreath = lastFire - 0.5f;
            }
        }
    }

    public void Shoot()
    {
        GameObject tempbullet = Instantiate(bulletPrefab, shotSpawner.position, shotSpawner.rotation);
        if (facingRight)
        {
            tempbullet.GetComponent<ZombieBullet>().speed *= -1;
            tempbullet.GetComponent<ZombieBullet>().transform.Translate((float)-0.5,0,0);
        }
    }
}
