using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI yourScore;
    public TextMeshProUGUI bestScore;
    void Start()
    {
        float highScore = PlayerPrefs.GetFloat("HighScore", 0);
        float score = PlayerPrefs.GetFloat("YourScore", 0);
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", score);
            PlayerPrefs.Save();
        }
        yourScore.text = "Your Score: " + score.ToString("F2");
        bestScore.text = "Best Score: " + highScore.ToString("F2");
    }
    public void Replay()
    {
        SceneManager.LoadScene("Game");
        GameManager.gameManager.timeRemaining = 90;
        GameManager.gameManager.timerIsRunning = true;
        
    }
    public void Menu()
    {
        SceneManager.LoadScene("Start");
    }
}
