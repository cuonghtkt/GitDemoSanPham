using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hatThong : MonoBehaviour
{
     int conintValue = 1;
    Animator anin;
    AudioSource asCoin;
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
                playGameController.instance.AddHatThong(conintValue);
                asCoin.volume = SaveData.dataNow.vol;
                asCoin.Play();
                anin.SetBool("Destroy", true);
                Destroy(gameObject, 1.20f);
            }
        }
    }
}
