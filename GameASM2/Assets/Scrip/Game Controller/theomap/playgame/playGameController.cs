using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playGameController : MonoBehaviour
{
    public static playGameController instance;
    public Text textCoin, textScore, textHatThong;

    public GameObject gameOverText;
    public GameObject gameWin;
    public bool isGameOver = false;


    private int score = 0;
    private int coins = 0;
    public int hatThong = 0;

    public AudioSource asEniDie;
    // Start is called before the first frame update
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
    void Start()
    {
        Time.timeScale = 1;
        hatThong = SaveData.dataNow.hatThong;
        textHatThong.text = "" + this.hatThong;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddScore(int score)
    {
        asEniDie.volume = SaveData.dataNow.vol;
        asEniDie.Play();
        this.score += score;
        textScore.text = "" + this.score; updataPhanThuong();
    }
    public void AddCoins(int coins)
    {
        this.coins += coins;
        textCoin.text = "" + this.coins; updataPhanThuong();
    }
    public void AddHatThong(int hatThong)
    {
        this.hatThong += hatThong;
        textHatThong.text = "" + this.hatThong; updataPhanThuong();
    }
    public void GameOver()
    {
        gameOverText.SetActive(true);
        Time.timeScale = 0;



        isGameOver = true;
        updataPhanThuong();
    }
    public void winGame()
    {
        Time.timeScale = 0;
        gameWin.SetActive(true);
        updataPhanThuong();
    }
    public void updataPhanThuong()
    {
        SaveData.dataNow.score += score;
        SaveData.dataNow.coin += coins;
        SaveData.dataNow.hatThong = hatThong;
        SaveData.SaveDataFunction();
    }
}
