using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeVol : MonoBehaviour
{
    public Slider vol;
    public Slider vol2;
    public AudioSource aS;
    // Start is called before the first frame update
    void Start()
    {
        vol.value = SaveData.dataNow.vol;
        vol2.value = SaveData.dataNow.volbackground;
    }

    // Update is called once per frame
    void Update()
    {
        SaveData.dataNow.vol = vol.value;
        SaveData.dataNow.volbackground = vol2.value;
        aS.volume = SaveData.dataNow.volbackground;
    }
}
