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

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("YourScore", 0);
        PlayerPrefs.SetFloat("Coins", 0);
        PlayerPrefs.SetFloat("Zombies", 0);
        PlayerPrefs.Save();
        UpdateHealthBar();
        UpdateCoins();
        UpdateHealthUI(200);
    }

    void Update()
    {
        if (GameManager.gameManager.timerIsRunning)
        {
            if (GameManager.gameManager.timeRemaining > 0)
            {
                GameManager.gameManager.timeRemaining -= Time.deltaTime;
                DisplayTime(GameManager.gameManager.timeRemaining);
            }
            else
            {
                PlayerPrefs.SetFloat("YourScore", PlayerPrefs.GetFloat("Coins", 0) + PlayerPrefs.GetFloat("Zombies", 0));
                PlayerPrefs.Save();
                GameManager.gameManager.timeRemaining = 0;
                GameManager.gameManager.timerIsRunning = false;
                DisplayTime(GameManager.gameManager.timeRemaining);
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
        GameManager.gameManager.timerIsRunning = false;
        SceneManager.LoadScene("Victory");
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
