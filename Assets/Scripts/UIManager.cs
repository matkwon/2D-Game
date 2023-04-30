using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI coinsText;
    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthBar();
        UpdateCoins();
        UpdateHealthUI(200);
    }

    public void UpdateHealthUI(int health)
    {
        healthText.text = health.ToString();
        healthBar.value = health;
    }

    public void UpdateCoins()
    {
        coinsText.text = GameManager.gameManager.coins.ToString();
    }

    public void UpdateHealthBar()
    {
        healthBar.maxValue = GameManager.gameManager.health;
    }

}
