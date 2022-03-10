using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eni4Rotation : MonoBehaviour
{
    private GameObject player;
    private float oldrot = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
            Vector3 position = player.transform.position;
            Vector3 dif = position - transform.parent.position;
            dif.Normalize();
            float rot = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
            if (rot != oldrot)
            {
                oldrot = rot;

                transform.rotation = Quaternion.Euler(0, 0, rot - 180);
            }
    }
}
