using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eniDie : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "playerAttack")
        {
            Destroy(gameObject,0.1f);
            playGameController.instance.AddScore(100);
        }
    }
}
