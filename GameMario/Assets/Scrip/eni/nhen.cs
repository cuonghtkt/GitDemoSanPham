using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nhen : MonoBehaviour
{
    Animator anim;
    public int moveInt = 0;
    float vitri;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(move());
        vitri = transform.position.y;
    }
    private void Update()
    {
        if (moveInt == 1)
        {
            transform.Translate(Vector2.up * -0.005f);
        }
        else if(moveInt==2)
        {
            transform.Translate(Vector2.up * 0.005f);
        }
        else
        {
            transform.Translate(Vector2.up * 0);
        }
        if (transform.position.y > vitri)
        {
            transform.Translate(Vector2.up * 0);
        }
    }
    IEnumerator move()
    {
        yield return new WaitForSeconds(3);
        anim.SetBool("Move", true);

    }
    // Update is called once per frame
    public void change()
    {
        anim.SetBool("Move", false);
        StartCoroutine(move());
    }
    public void Move2()
    {
        moveInt += 1;
        if (moveInt == 3)
        {
            moveInt = 0;
        }
    }

}
