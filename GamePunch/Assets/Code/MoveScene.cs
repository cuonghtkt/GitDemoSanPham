using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void loadLever(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
        Time.timeScale = 1;
    }
    public void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
