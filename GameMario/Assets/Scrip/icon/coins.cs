using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coins : MonoBehaviour
{
    int conintValue = 10;
    public GameObject particle;
    AudioSource asCoin;
    Vector2 vitri;
    Animator anin;
    private void Start()
    {
        anin = GetComponent<Animator>();
        asCoin = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (anin.GetBool("Destroy") == false)
            {
                playGameController.instance.AddCoins(conintValue);
                anin.SetBool("Destroy", true);
                asCoin.volume = SaveData.dataNow.vol;
                asCoin.Play();
                Destroy(gameObject, 1.20f);
                     creatParticle();
            }
        }
    }
    void creatParticle()
    {
        vitri.x = transform.position.x;
        vitri.y = transform.position.y;
        Instantiate(particle, vitri, Quaternion.identity);
    }
}
