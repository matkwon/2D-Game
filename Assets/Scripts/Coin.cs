using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : Collectible
{
    GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Instantiate(explosion, transform.position, transform.rotation);
        Player otherPlayer = other.GetComponent<Player>();
        if (otherPlayer != null)
        {
            audioSource.Play();
            otherPlayer.GetCoin();
            Destroy(gameObject);
        }
    }
}
