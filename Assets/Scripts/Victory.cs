using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Victory : MonoBehaviour
{
    public TextMeshProUGUI yourScore;
    public TextMeshProUGUI bestScore;

    public void UpdateRemainingTimes()
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

    void Start()
    {
        UpdateRemainingTimes();
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
