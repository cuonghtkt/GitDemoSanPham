using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEniAttack2 : MonoBehaviour
{
    private GameObject player;
    private Vector3 wayPointPos, position, dif;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        attack();
        StartCoroutine(waitNewAttack());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPointPos, 5 * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }
    IEnumerator waitNewAttack()
    {
        yield return new WaitForSeconds(1);
        attack();
        StartCoroutine(waitNewAttack());
    }
    private void attack()
    {

        wayPointPos = new Vector2(player.transform.position.x, player.transform.position.y);
        position = player.transform.position;
        dif = position - transform.position;
        dif.Normalize();
        float rot = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot - 90);
    }
}
