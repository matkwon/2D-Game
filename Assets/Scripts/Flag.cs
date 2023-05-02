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
            PlayerPrefs.SetFloat("YourScore", PlayerPrefs.GetFloat("Coins", 0) + PlayerPrefs.GetFloat("Zombies", 0) + GameManager.gameManager.timeRemaining + GameManager.gameManager.health);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Victory");
        }
    }
}
