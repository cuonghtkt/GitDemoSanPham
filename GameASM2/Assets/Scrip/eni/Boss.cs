using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
   public int Hp=15;
    public GameObject linh1, linh2;
    public Animator anim;
    public bool nextSkill = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BossAttack());
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }

        if (nextSkill)
        {
            nextSkill = false;
            StartCoroutine(BossAttack());
        }
    }
    IEnumerator BossAttack()
    {
        yield return new WaitForSeconds(1);
        int a = Random.Range(0, 3);
        if (a == 1)
        {
            anim.SetBool("skill2", true);
        }else if (a == 2)
        {
            anim.SetBool("skill3", true);
        }
        else if (a == 0)
        {
            anim.SetBool("skill1", true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "playerAttack")
        {
            Hp -= 1;
        }
    }
    void taoCrep1()
    {
        Instantiate(linh1, gameObject.transform.position, transform.rotation);
    }
    void taoCrep2()
    {
        Instantiate(linh2, gameObject.transform.position, transform.rotation);
    }
    void allFalse()
    {
        anim.SetBool("skill1", false);
        anim.SetBool("skill2", false);
        anim.SetBool("skill3", false);
        nextSkill = true;
    }
}
