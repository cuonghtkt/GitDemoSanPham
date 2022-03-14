using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Hp : MonoBehaviour
{
    public static GameController instance;
    public static int hp = 0;
    private bool coTheTruHP = true;
    public Slider imgHP;
    public Text textHP;
    public AudioSource asNhanDame;
    // Start is called before the first frame update
    void Start()
    {
        hp = SaveData.dataNow.Hp;
        imgHP.maxValue = hp;
        textHP.text = hp + "";
        imgHP.value = hp;
    }

    // Update is called once per frame
    void Update()
    {
        textHP.text = hp + "";
        imgHP.value = hp;
    }
    public void mauNhanVat(int score)
    {
        if (coTheTruHP == true)
        {
            asNhanDame.volume = SaveData.dataNow.vol;
            asNhanDame.Play();
            StartCoroutine(truHp(score));
            hp -= score;
            if (hp <= 0)
            {
                playGameController.instance.GameOver();
                Destroy(gameObject);
            }
        }

    }
    IEnumerator truHp(int score)
    {
        coTheTruHP = false;
        yield return new WaitForSeconds(1);
        coTheTruHP = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Eni")
        {
           mauNhanVat(1);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Eni")
        {
            mauNhanVat(1);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Eni")
        {
            mauNhanVat(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Eni")
        {
           mauNhanVat(1);
        }
    }
}
