using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scpitskill5 : MonoBehaviour
{
    public static bool skill5Die=true;
    public Transform player;
    public ConstantForce2D obj;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Coins" && collision.gameObject.tag != "playerAttack")
        {
            Destroy(gameObject);
            skill5Die = true;

        }
    }
    private void Start()
    {
        player = GameObject.Find("Player").transform;
        obj = GetComponent<ConstantForce2D>();
        transform.localScale = new Vector3(player.localScale.x*3,
              player.localScale.y*3, transform.position.z);
        obj.force = new Vector2(player.localScale.x*5, 0);
        StartCoroutine(waitDestroy());
    }
    IEnumerator waitDestroy()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
        skill5Die = true;
    }
}
