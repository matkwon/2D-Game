using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI coinsText;
    public Slider healthBar;
    public TextMeshProUGUI timeText;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager;
        gameManager.currentScore = 0;
        gameManager.timeRemaining = 120;
        gameManager.coins = 0;
        gameManager.zombies = 0;
        gameManager.timerIsRunning = true;
        UpdateHealthBar();
        UpdateCoins();
        UpdateHealthUI(200);
    }

    void Update()
    {
        if (gameManager.timerIsRunning)
        {
            if (gameManager.timeRemaining > 0)
            {
                gameManager.timeRemaining -= Time.deltaTime;
                DisplayTime(gameManager.timeRemaining);
            }
            else
            {
                PlayerPrefs.SetFloat("YourScore", PlayerPrefs.GetFloat("Coins", 0) + PlayerPrefs.GetFloat("Zombies", 0));
                PlayerPrefs.Save();
                gameManager.timeRemaining = 0;
                gameManager.timerIsRunning = false;
                DisplayTime(gameManager.timeRemaining);
                SceneManager.LoadScene("Death");
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ShowVictoryScreen()
    {
        gameManager.timerIsRunning = false;
        SceneManager.LoadScene("Victory");
    }

    public void UpdateHealthUI(int health)
    {
        healthText.text = health.ToString();
        healthBar.value = health;
    }

    public void UpdateCoins()
    {
        coinsText.text = gameManager.coins.ToString();
    }

    public void UpdateHealthBar()
    {
        healthBar.maxValue = gameManager.health;
    }

}
