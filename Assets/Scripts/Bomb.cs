using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : Collectible
{
    public Image bombImage;
    public Image laserImage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Instantiate(explosion, transform.position, transform.rotation);
        Player otherPlayer = other.GetComponent<Player>();
        if (otherPlayer != null)
        {
            GameManager.gameManager.currentSpecial = GameManager.SpecialType.Bomb;
            bombImage.color = Color.white;
            laserImage.color = new Color(0,0,0,0);
            Destroy(gameObject);
            audioSource.Play();
        }
    }
}
