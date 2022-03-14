using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSelect : MonoBehaviour
{
    public GameObject[] obj;
    public void anHienSelect(int count)
    {
        obj[count].SetActive(!obj[count].activeSelf);
        SaveData.SaveDataFunction();
    }
}
