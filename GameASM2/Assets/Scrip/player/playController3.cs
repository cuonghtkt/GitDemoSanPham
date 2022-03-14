using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playController3 : MonoBehaviour
{
    Rigidbody2D rbody;
    Animator anim;
    public float minspeed = 5f, maxspeed = 8, height = 8.7f, speed;
    public bool doublejump = false;
    public bool jumped, ground = false,taokhoi=true;
    public AudioSource asJump;
    bool walking;
    float walkTime;
    public int moveState;
    private int eniAttack=1;

    public GameObject khoi,taoKhoi;

    public List<RuntimeAnimatorController> playeranimRun;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = playeranimRun[SaveData.dataNow.playerNow];
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetBool("Run")|| anim.GetBool("Jump"))
        {
            if (taokhoi)
            {
                StartCoroutine(ieTaoKhoi());
            }
        }
        State();
        roiChet();
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumped = true;
            if (ground)
            {
                rbody.velocity = new Vector2(rbody.velocity.x, height);
                asJump.volume =SaveData.dataNow.vol;
                asJump.Play();
                doublejump = true;
            }
            else
            {
                if (doublejump)
                {
                    asJump.Play();
                    rbody.gravityScale = 2;
                    rbody.velocity = new Vector2(rbody.velocity.x, height);
                    doublejump = false;
                }
            }
        }
        if (jumped)
        {
            anim.Play("jump");
            anim.SetBool("Jump", true);
            if (rbody.gravityScale <= 5)
            {
                rbody.gravityScale += .1f;
            }
        }
    }
    IEnumerator ieTaoKhoi()
    {
        Instantiate(khoi, taoKhoi.transform.position, transform.rotation);
        taokhoi = false;
        yield return new WaitForSeconds(0.1f);
        taokhoi = true;
    }
    private void roiChet()
    {
        if (gameObject.transform.position.y < -10)
        {
            die();
        }
    }

    private void FixedUpdate()
    {
        if (!Input.GetKey(KeyCode.RightArrow) || !(Input.GetKey(KeyCode.LeftArrow)))
        {
            moveState = 0;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveState = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveState = 2;
        }

    }
    void Move(Vector3 dir)
    {
        walking = true;
        speed = Mathf.Clamp(speed, minspeed, maxspeed);
        walkTime += Time.deltaTime;

        transform.Translate(dir * speed * Time.deltaTime* eniAttack);
        if (walkTime < 3f && walking)
        {
            speed += .025f;
        }
        else if (walkTime > 3f)
        {
            speed = maxspeed;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = true;
            jumped = false;
            doublejump = false;
            rbody.gravityScale = 2;
            anim.SetBool("Jump", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            anim.Play("idle");
        }
        if (collision.gameObject.tag == "Eni")
        {
            rbody.velocity = new Vector3(transform.localScale.x * 5 * -1, 5, 0);
            StartCoroutine(EniAttack());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Eni")
        {

            rbody.velocity = new Vector3(transform.localScale.x * 5 * -1, 5, 0);
            StartCoroutine(EniAttack());

        }
        if (collision.gameObject.tag == "playerAttack")
        {
            playGameController.instance.AddHatThong(1);
            Destroy(collision.gameObject);

        }
    }
    IEnumerator EniAttack()
    {
        eniAttack = 0;
        yield return new WaitForSeconds(0.5f);
        eniAttack = 1;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = false;
            doublejump = true;
        }
    }

    void State()
    {
        switch (moveState)
        {
            case 1:
                anim.SetBool("Run", true);
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),
                    transform.localScale.y, transform.localScale.z);
                Move(Vector3.right);
                break;
            case 2:
                anim.SetBool("Run", true);
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x),
                    transform.localScale.y, transform.localScale.z);
                Move(Vector3.left);
                break;
            default:
                walking = false;
                walkTime = 0;
                speed = minspeed;
                anim.SetBool("Run", false);
                break;
        }
       
        /* if (Input.GetKey(KeyCode.UpArrow))
         {
             anim.SetBool("Jump", true);
         }
         else
         {
             anim.SetBool("Jump", false);
         }*/
    }

    void die()
    {
        playGameController.instance.GameOver();
        Destroy(gameObject);
    }
}
