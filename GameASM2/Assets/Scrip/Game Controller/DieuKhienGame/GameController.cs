using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Text scoreText;
    public GameObject gameOverText;

    public GameObject gameWin;
    public bool isGameOver = false;

    private int score = 0;


    private void Start()
    {
        Time.timeScale = 1;
        updateScore(10);
    }
    private void Awake()
    {
        if (instance == null)
        {
           // DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void AddScore(int score)
    {
        this.score += score;
        scoreText.text = "Score:" + this.score;
        updateScore(score);
    }

    public void GameOver()
    {
        gameOverText.SetActive(true);
        Time.timeScale = 0;

        isGameOver = true;
    }
    public void updateScore(int scoreAdd)
    {
        int score = PlayerPrefs.GetInt("score");
        score += scoreAdd;
        PlayerPrefs.SetInt("score", score);
    }
    public void winGame()
    {
        gameWin.SetActive(true);
    }
    public void ModeSelect()
    {
        StartCoroutine(LoadAfterDelay());
    }
    IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
