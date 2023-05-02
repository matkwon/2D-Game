using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    // Start is
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Instantiate(explosion, transform.position, transform.rotation);
        Player otherPlayer = other.GetComponent<Player>();
        if (otherPlayer != null)
        {
            GameManager.gameManager.currentScore = GameManager.gameManager.coins + GameManager.gameManager.zombies + GameManager.gameManager.timeRemaining + GameManager.gameManager.health;
            SceneManager.LoadScene("Victory");
        }
    }
}
