using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scpitskill1 : MonoBehaviour
{
   public Rigidbody2D rbody;
    public Transform player;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Coins" && collision.gameObject.tag != "Ground" && collision.gameObject.tag != "playerAttack" && collision.gameObject.tag != "Ground" && collision.gameObject.tag != "skill5")
        {
            Destroy(gameObject);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Coins" && collision.gameObject.tag != "Ground" && collision.gameObject.tag != "playerAttack" && collision.gameObject.tag != "Ground" && collision.gameObject.tag != "skill5")
        {
            Destroy(gameObject);

        }
    }
    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        rbody.velocity = new Vector2(10*player.transform.localScale.x, 3);
        StartCoroutine(waitDestroy());
    }
    IEnumerator waitDestroy()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
