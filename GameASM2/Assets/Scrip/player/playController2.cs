using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController2 : MonoBehaviour
{
    Rigidbody2D rbody;
    Animator anim;
    public float minspeed = 5f, maxspeed = 8, height = 8.7f, speed;
    public bool doublejump = false;
    public bool jumped, ground = false;
    bool walking;
    float walkTime;
    public int moveState;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        State();
        roiChet();
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumped = true;
            if (ground)
            {
                rbody.velocity = new Vector2(rbody.velocity.x, height);
                doublejump = true;
            }
            else
            {
                if (doublejump)
                {
                    rbody.gravityScale = 1;
                    rbody.velocity = new Vector2(rbody.velocity.x, 6f);
                    doublejump = false;
                }
            }
        }
        if (jumped)
        {
            rbody.gravityScale += .1f;
        }
    }

    private void roiChet()
    {
        if (gameObject.transform.position.y < -10)
        {
            gameObject.SetActive(false);
            GameController.instance.GameOver();
            Time.timeScale = 0;
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

        transform.Translate(dir * speed * Time.fixedDeltaTime);
        if (walkTime < 3f && walking)
        {
            speed += .025f;
        }
        else if (walkTime > 3f)
        {
            speed = maxspeed;
        }
    }
    void Jump()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Eni")
        {
            gameObject.SetActive(false);
            Time.timeScale = 0;
            GameController.instance.GameOver();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = true;
            jumped = false;
            doublejump = false;

            anim.SetBool("Jump", false);
            rbody.gravityScale = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Eni")
        {
            gameObject.SetActive(false);
            Time.timeScale = 0;
            GameController.instance.GameOver();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = false;
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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }
    }



}
