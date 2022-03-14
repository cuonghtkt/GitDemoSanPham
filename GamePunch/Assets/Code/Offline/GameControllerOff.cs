using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerOff : MonoBehaviour
{

    public static float leverGame=1f;

    public List<GameObject> lsObjectSave;
    public Transform lsFood,lsEni;

    void Start()
    {
        createFood();
        Instantiate(lsObjectSave[1], new Vector2(Random.Range(-14.5f, 14.6f), Random.Range(-14.5f, 14.6f)), Quaternion.identity,GameObject.Find("ListEni").transform);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("" + leverGame);
    }
    private void createFood() {
        for(int i = 0; i < 20; i++)
        {
            GameObject food = Instantiate(lsObjectSave[0], new Vector2(Random.Range(-14.5f,14.6f), Random.Range(-14.5f, 14.6f)), Quaternion.identity, lsFood);
        }
    }
    public void funCreateEni()
    {
        StartCoroutine(createNewEni());
    }
    IEnumerator createNewEni()
    {
        yield return new WaitForSeconds(0);
        for (float i = 1; i <=GameControllerOff.leverGame; i++)
        {
            Instantiate(lsObjectSave[Random.Range(1, 5)], new Vector2(Random.Range(-14.5f, 14.6f), Random.Range(-14.5f, 14.6f)), Quaternion.identity, lsEni);
        }
    }
}
