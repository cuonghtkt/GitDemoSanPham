using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDown : MonoBehaviour
{
    Rigidbody2D rbody;
   // bool roi = false;
    // Start is called before the first frame update
    void Start()
    {

        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(waitRoi());
        }
    }
    IEnumerator waitRoi()
    {
        rbody.gravityScale = .01f;
        yield return new WaitForSeconds(1);
        rbody.gravityScale = 2f;
    }
}
