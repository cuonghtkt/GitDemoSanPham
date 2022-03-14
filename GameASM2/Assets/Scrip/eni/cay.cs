using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cay : MonoBehaviour
{
    Vector2 vitri;
    public GameObject doc;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(attack());
    }

    // Update is called once per frame
    IEnumerator attack()
    {
        yield return new WaitForSeconds(Random.Range(2,4));
        creatParticle();
        StartCoroutine(attack());
    }
    void creatParticle()
    {
        vitri.x = transform.position.x;
        vitri.y = transform.position.y-0.7f;
        Instantiate(doc, vitri, Quaternion.identity);
    }
}
