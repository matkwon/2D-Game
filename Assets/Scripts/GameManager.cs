using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int health = 5;
    public int damage = 1;
    public float fireRate = 0.5f;
    public int coins;
    
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
