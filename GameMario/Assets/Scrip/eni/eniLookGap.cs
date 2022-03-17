using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eniLookGap : MonoBehaviour
{
    public GameObject obstacleObject;
    int i = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            i = 1;
            obstacleObject.transform.localScale =
                new Vector3(obstacleObject.transform.localScale.x,
                obstacleObject.transform.localScale.y,
                obstacleObject.transform.localScale.z);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground"&&i==1)
        {
            i = 0;
            obstacleObject.transform.localScale =
                new Vector3(obstacleObject.transform.localScale.x,
                -obstacleObject.transform.localScale.y,
                obstacleObject.transform.localScale.z);
        }
    }
}
