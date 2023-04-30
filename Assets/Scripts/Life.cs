using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Collectible
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player otherPlayer = other.GetComponent<Player>();
        if (otherPlayer != null)
        {
            audioSource.Play();
            otherPlayer.Heal(40);
            Destroy(gameObject);
        }
    }
}
