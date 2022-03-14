using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Zoom : MonoBehaviour
{
    public new Camera camera;
    public GameObject player;
    public float speed;
    public float maxSize = 7.5f;
    public float minSize = 5.5f;
    float cameraSize = 6f;

    private void Update()
    {
        if (player != null)
        {
            cameraSize = 5f * player.transform.position.y;
        }
        if (cameraSize >= maxSize)
        {
            cameraSize = maxSize;
        }
        if (cameraSize <= minSize)
        {
            cameraSize = minSize;
        }
        if (Time.timeScale != 0)
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, cameraSize, Time.deltaTime / speed);
        }
    }
}
