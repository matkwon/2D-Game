using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 900f;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public GameObject victoryScreen;
    public static Timer timer;

    private void Start()
    {
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                PlayerPrefs.SetFloat("YourScore", PlayerPrefs.GetFloat("Coins", 0) + PlayerPrefs.GetFloat("Zombies", 0));
                PlayerPrefs.Save();
                timeRemaining = 0;
                timerIsRunning = false;
                DisplayTime(timeRemaining);
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
        timerIsRunning = false;
        SceneManager.LoadScene("Victory");
    }
}
