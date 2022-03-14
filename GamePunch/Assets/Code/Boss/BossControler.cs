using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControler : MonoBehaviour
{
    public GameObject food;
    public Transform lsFood;
    // Start is called before the first frame update
    void Start()
    {

        createFood();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void createFood()
    {
        for (int i = 0; i < 20; i++)
        {
            Instantiate(food, new Vector2(Random.Range(-14.5f, 14.6f), Random.Range(-14.5f, 14.6f)), Quaternion.identity, lsFood);
        }
    }
}
