using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public float gameTime;
    public static int score;
    public Text scoreText;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            InvokeRepeating("Countdown", 1.0f, 1.0f);
        }
    }

    public void AddPoints(int points)
    {
        score += points;
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Countdown()
    {
        gameTime--;
        if (gameTime <= 0f)
        {
            CancelInvoke("Countdown");
            LoadNextLevel();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
