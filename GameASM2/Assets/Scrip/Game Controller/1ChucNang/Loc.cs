using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loc : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(),
                gameObject.GetComponent<Collider2D>());
        }
    }
}
