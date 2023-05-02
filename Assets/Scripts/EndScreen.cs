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
        UpdateRemainingTimes();
    }

    public void UpdateRemainingTimes()
    {
        float highScore = PlayerPrefs.GetFloat("HighScore", 0);
        float score = GameManager.gameManager.currentScore;
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
        
    }
    public void Menu()
    {
        SceneManager.LoadScene("Start");
    }
}
