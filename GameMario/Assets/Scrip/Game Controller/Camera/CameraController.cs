using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public  Transform playerTransform;
    public float height;
    void Update()
    {
        if (scpitskill5.skill5Die==false)
        {
            GameObject[] test = GameObject.FindGameObjectsWithTag("skill5");
            playerTransform = test[0].transform;
            transform.position = new Vector3(playerTransform.position.x,
                height + playerTransform.position.y, transform.position.z);
        }
        else
        {
            try
            {
                playerTransform = GameObject.Find("/Player").transform;
                transform.position = new Vector3(playerTransform.position.x,
              height + playerTransform.position.y, transform.position.z);
            }
            catch  {
                transform.position = new Vector3(0,0,-10);
            }
           
        }


    }
}
