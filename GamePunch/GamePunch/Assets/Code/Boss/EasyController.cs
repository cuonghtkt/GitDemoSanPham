using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyController : MonoBehaviour
{
    public float hp;
    public float wait;
    public GameObject eni, player;
    public Transform listEni;
    void Start()
    {
        StartCoroutine(createEni());
    }

    IEnumerator createEni()
    {
        yield return new WaitForSeconds(wait);
        if (gameObject.name.Equals("Fire"))
        {
            Instantiate(eni, transform.position, Quaternion.identity);
        }
        else
        {
            if (listEni.childCount <= 1)
            {
                Instantiate(eni, transform.position, Quaternion.identity, listEni);
            }
        }

        StartCoroutine(createEni());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("PlayerPunch"))
        {
            hp -= player.GetComponent<PlayerOff>().player.status.attack;
            if (hp <= 0)
            {
                Destroy(gameObject);
                player.GetComponent<PlayerOff>().tinhLv(10);
                if (transform.parent.childCount == 2)
                {
                    if (transform.parent.GetChild(1).gameObject.name.Equals("Hard"))
                    {
                        transform.parent.GetChild(1).gameObject.SetActive(true);
                    }
                }
                if(transform.parent.name.Equals("Hard")&& transform.parent.childCount <= 1)
                {
                    Destroy(GameObject.Find("Boss"));
                }
            }
        }
    }
}
