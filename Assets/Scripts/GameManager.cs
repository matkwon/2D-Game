using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public int health = 200;
    public int playerDamage = 1;
    public int enemyDamage = 10;
    public int spikeDamage = 25;
    public float fireRate = 0.5f;
    public int coins = 0;
    public enum SpecialType{None, Bomb, Laser}
    public SpecialType currentSpecial = SpecialType.None;
    
    public static GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null) gameManager = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
