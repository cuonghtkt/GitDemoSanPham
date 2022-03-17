using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public GameObject loading, position;
    private void Start()
    {
        Time.timeScale = 1;
    }
    public void LoadSccene(string i)
    {
        StartCoroutine(Loading(i));
    }
    public void ReloadScene()
    {
        string i = SceneManager.GetActiveScene().name + "";
        StartCoroutine(Loading(i));
    }
    public void loadNextScene()
    {
        int i = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(Loading2(i));
        
    }
    IEnumerator Loading(string i)
    {
        Time.timeScale = 1;
        if (position == null)
        {
            Instantiate(loading);
        }
        else
        {
            Instantiate(loading, position.transform.position, transform.rotation);
        }
        //playGameController.instance.updataPhanThuong();
        SaveData.SaveDataFunction();
        yield return new WaitForSeconds(1.9f);
        SceneManager.LoadScene(i);
    }
    IEnumerator Loading2(int i)
    {
        Time.timeScale = 1;
        if (position == null)
        {
            Instantiate(loading);
        }
        else
        {
            Instantiate(loading, position.transform.position, transform.rotation);
        }
       // playGameController.instance.updataPhanThuong();
        SaveData.SaveDataFunction();
        yield return new WaitForSeconds(1.9f);
        SceneManager.LoadScene(i);
    }
}
