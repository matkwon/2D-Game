using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Player player = other.collider.GetComponent<Player>();
            if (player != null)
            {
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play();
                player.SpikeDamage(GameManager.gameManager.spikeDamage);
            }
        }
    }
}
