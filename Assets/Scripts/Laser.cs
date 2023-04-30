using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : Collectible
{
    public Image bombImage;
    public Image laserImage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Instantiate(explosion, transform.position, transform.rotation);
        Player otherPlayer = other.GetComponent<Player>();
        if (otherPlayer != null)
        {
            GameManager.gameManager.currentSpecial = GameManager.SpecialType.Laser;
            laserImage.color = Color.white;
            bombImage.color = new Color(0,0,0,0);
            Destroy(gameObject);
            audioSource.Play();
        }
    }
}
