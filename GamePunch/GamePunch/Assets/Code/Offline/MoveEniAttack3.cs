using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEniAttack3 : MonoBehaviour
{
    private GameObject player;
    private Vector3 wayPointPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        wayPointPos = new Vector2(player.transform.position.x, player.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPointPos, 5 * Time.deltaTime);
        if(wayPointPos.x== transform.position.x && wayPointPos.y==transform.position.y)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }

}
