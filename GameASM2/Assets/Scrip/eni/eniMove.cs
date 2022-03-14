using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eniMove : MonoBehaviour
{
    public float speed = 40;
    private bool canMove = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            GetComponent<Rigidbody2D>().velocity =
                new Vector2(transform.localScale.y, 0) * speed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canMove = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canMove = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canMove = false;
        }
    }
}
