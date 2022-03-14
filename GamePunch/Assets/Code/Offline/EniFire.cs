using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EniFire : MonoBehaviour
{
    private GameObject player;
    private float oldrot = 0;
    public GameObject fire;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent.GetComponent<Eni>().speed = 0;
        player = GameObject.Find("Player");
        Debug.Log(player.name);
        StartCoroutine(waitAttack());
    }

    // Update is called once per frame
    void Update()
    {
        checkAniming();
    }

    private void checkAniming()
    {
        Vector3 position = player.transform.position;
        Vector3 dif = position - transform.parent.position;
        dif.Normalize();
        float rot = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
        if (rot != oldrot)
        {
            oldrot = rot;

            transform.rotation = Quaternion.Euler(0, 0, rot - 90);
        }
    }
    IEnumerator waitAttack()
    {
        yield return new WaitForSeconds(1);
        Instantiate(fire, transform.GetChild(0).position, Quaternion.identity);
        StartCoroutine(waitAttack());

    }
}
