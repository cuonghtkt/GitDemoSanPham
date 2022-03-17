using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMushroom : MonoBehaviour
{
    public GameObject Mushroom;
    public float time = 4, max = 50,x1 =-8 ,x2=8;
    public float y = 4;
    float dem=0;
    Vector2 vitri;
    private void Start()
    {
        CreatMushroom();
        StartCoroutine(randomCreat());
    }
    IEnumerator randomCreat()
    {
        yield return new WaitForSeconds(time);
        if (dem < max)
        {
            CreatMushroom();
            dem += 1;
            StartCoroutine(randomCreat());
        }
    }
    void CreatMushroom()
    {
        vitri.x = Random.Range(x1, x2);
        vitri.y = y;
        Instantiate(Mushroom, vitri, transform.rotation,transform.parent);
    }
}
