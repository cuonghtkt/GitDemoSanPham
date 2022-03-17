using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuController : MonoBehaviour
{
    public Text textCoin, textScore,textHatThong;
    public GameObject[] items;

    public Animator animPlayer;
    public List<RuntimeAnimatorController> playeranimWait;
    public List<int> playerContList;
    public int playerConut;
    // Start is called before the first frame update
    void Start()
    {
        playerConut = SaveData.dataNow.playerNow;
        playerContList.Add(0);
    }
    void updateButtonBuy()
    {
        if (SaveData.dataNow.skill1 == true)
        {
            items[0].gameObject.SetActive(false);
        }
        if (SaveData.dataNow.skill2 == true)
        {
            items[1].gameObject.SetActive(false);
        }
        if (SaveData.dataNow.skill3 == true)
        {
            items[2].gameObject.SetActive(false);
        }
        if (SaveData.dataNow.skill4 == true)
        {
            items[3].gameObject.SetActive(false);
        }
        if (SaveData.dataNow.skill5 == true)
        {
            items[4].gameObject.SetActive(false);
        }
        if (SaveData.dataNow.player1 == true)
        {
            items[5].gameObject.SetActive(false);
        }
        if (SaveData.dataNow.player2 == true)
        {
            items[6].gameObject.SetActive(false);
        }
        if (SaveData.dataNow.player3 == true)
        {
            items[7].gameObject.SetActive(false);
        }
        if (SaveData.dataNow.trangbi1 == true)
        {
            items[8].gameObject.SetActive(false);
        }
        if (SaveData.dataNow.trangbi2 == true)
        {
            items[9].gameObject.SetActive(false);
        }
        if (SaveData.dataNow.trangbi3 == true)
        {
            items[10].gameObject.SetActive(false);
        }

    }
    public void buy(int so)
    {
        if (SaveData.dataNow.coin >= 200)
        {
            if (so == 0)
            {
                SaveData.dataNow.coin -= 200;
                SaveData.dataNow.skill1 = true;
            }
            if (so == 1)
            {
                SaveData.dataNow.coin -= 200;
                SaveData.dataNow.skill2 = true;
            }
            if (so == 2)
            {
                SaveData.dataNow.coin -= 200;
                SaveData.dataNow.skill3 = true;
            }
            if (so == 3)
            {
                SaveData.dataNow.coin -= 200;
                SaveData.dataNow.skill4 = true;
            }
            if (so == 4)
            {
                SaveData.dataNow.coin -= 200;
                SaveData.dataNow.skill5 = true;
            }
            if (so == 5)
            {
                SaveData.dataNow.coin -= 200;
                SaveData.dataNow.player1 = true;
            }
            if (so == 6)
            {
                SaveData.dataNow.coin -= 200;
                SaveData.dataNow.player2 = true;
            }
            if (so == 7)
            {
                SaveData.dataNow.coin -= 200;
                SaveData.dataNow.player3 = true;
            }
            if (so == 8)
            {
                SaveData.dataNow.coin -= 200;
                SaveData.dataNow.trangbi1 = true;
                SaveData.dataNow.Hp += 2;
            }
            if (so == 9)
            {
                SaveData.dataNow.coin -= 200;
                SaveData.dataNow.trangbi2 = true;
                SaveData.dataNow.Hp += 4;
            }
            if (so == 10)
            {
                SaveData.dataNow.coin -= 200;
                SaveData.dataNow.trangbi3 = true;
                SaveData.dataNow.Hp += 8;
            }
            SaveData.SaveDataFunction();
        }
    }
    // Update is called once per frame
    void Update()
    {
        textCoin.text = "" + SaveData.dataNow.coin;
        textScore.text = "" + SaveData.dataNow.score;
        textHatThong.text = "" + SaveData.dataNow.hatThong;
        updateButtonBuy();
        updatePLayer();
    }

    public void buttonNextPlayer(int i)
    {
        playerContList.Clear();
        playerContList.Add(0);

        if (SaveData.dataNow.player1) {  playerContList.Add(1); }
        if (SaveData.dataNow.player2) { playerContList.Add(2); }
        if (SaveData.dataNow.player3) { playerContList.Add(3); }

        playerConut += i;

        if (playerConut > playerContList.Count-1)
        {
            playerConut = 0;
        }
        else if (playerConut < 0)
        {
            playerConut = playerContList.Count-1;
        }
        SaveData.dataNow.playerNow = playerContList[playerConut];
        SaveData.SaveDataFunction();
    }
    public void updatePLayer()
    {
        animPlayer.runtimeAnimatorController = playeranimWait[SaveData.dataNow.playerNow];
    }
}
