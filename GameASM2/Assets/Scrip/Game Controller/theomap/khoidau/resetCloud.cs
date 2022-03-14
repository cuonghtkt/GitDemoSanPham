using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetCloud : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -10)
        {
            Vector2 move = new Vector2(15, 0);
            move.y = transform.position.y;
            transform.position = move;
        }
    }
}
