using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxLoop : MonoBehaviour
{
    public float Speed=5;
    public GameObject[] mang;
    // Update is called once per frame
    void Update()
    {
        Speed += 0.00000015f;
        mang[0].transform.Translate(Vector3.left * Speed * Time.timeScale, Space.World);
        mang[1].transform.Translate(Vector3.left * Speed * Time.timeScale, Space.World);
        mang[2].transform.Translate(Vector3.left * Speed * Time.timeScale, Space.World);
        mang[3].transform.Translate(Vector3.left * Speed * Time.timeScale, Space.World);

        mang[4].transform.Translate(Vector3.left * Speed * Time.timeScale, Space.World);
        mang[5].transform.Translate(Vector3.left * Speed * Time.timeScale, Space.World);
        mang[6].transform.Translate(Vector3.left * Speed * Time.timeScale, Space.World);
        if (mang[0].transform.position.x <= -10)
        {
            HoiSinh(mang[0]);
        }
        if (mang[1].transform.position.x <= -10)
        {
            HoiSinh(mang[1]);
        }
        if (mang[2].transform.position.x <= -10)
        {
            HoiSinh(mang[2]);
        }
        if (mang[3].transform.position.x <= -10)
        {
            HoiSinh(mang[3]);
        }
        if (mang[4].transform.position.x <= -10)
        {
            HoiSinh(mang[4]);
        }
        if (mang[5].transform.position.x <= -10)
        {
            HoiSinh(mang[5]);
        }
        if (mang[6].transform.position.x <= -10)
        {
            HoiSinh(mang[6]);
        }
    }
    void HoiSinh(GameObject obj)
    {
        float i = UnityEngine.Random.Range(0, 3);
        obj.transform.position = new Vector3(30, i, 0);
    }

}
