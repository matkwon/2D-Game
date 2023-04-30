using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Instantiate(explosion, transform.position, transform.rotation);
        Player otherPlayer = other.GetComponent<Player>();
        if (otherPlayer != null) otherPlayer.Death();
    }
}
