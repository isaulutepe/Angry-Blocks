using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public Text scoreText;
    public Text bestScoreText;

    private GameController gameController;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
    private void Update()
    {
        scoreText.text = gameController.score.ToString();
        if (gameController.score > PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", gameController.score);
        }
        bestScoreText.text = "Best " + PlayerPrefs.GetInt("BestScore");
    }
    public void TryAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0); //Ýlk sahneyi yeniden yükle.
    }
    public void Home()
    {
        gameController.gameOver.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Pause() //Durdur
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    public void UnPause() //Devam
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
