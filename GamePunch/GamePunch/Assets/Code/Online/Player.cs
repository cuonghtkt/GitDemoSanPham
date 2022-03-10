using SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public SocketIOComponent socket;
    public PlayerData player;
    //attack
    public Transform punchPivot, punchRight, punchLeft;
    public Collider2D cldPunchLeft, cldPunchRight;
    public bool typeHand = true;
    const float BARREL_PIVOT_OFFSET = 90.0f;
    private float oldrot = 0;
    public Animator anin;
    public Transform status;
    public int restore = 0;
    public List<string> lsAttack = new List<string>();
    private void Awake()
    {
    }
    void Start()
    {
        socket = GameObject.Find("GameController").GetComponent<NetWorkingClient>();
        status = GameObject.Find("Status").transform;

        player.id = gameObject.name;
        anin = gameObject.GetComponent<Animator>();

        changeStatusView();
        addEventToTextView();
        //di chuyen 1 it de khong bi trung len nhau
        transform.position += new Vector3(1, 1, 0) * 0.001f;
        socket.Emit("updatePosition", player.position.ToString());
        StartCoroutine(funRestore());
    }

    // Update is called once per frame
    void Update()
    {
        checkAiming();
        attack();
        movePlayer();
    }

    IEnumerator funRestore()
    {
        yield return new WaitForSeconds(1);
        restore -= 1;
        if (restore <= 0 && player.status.hpNow < player.status.hp)
        {
            socket.Emit("restoreHp");
        }
        StartCoroutine(funRestore());
    }
    private void movePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.position += new Vector3(horizontal, vertical, 0) * player.status.speed * Time.deltaTime;
        player.position.x = Mathf.Round(gameObject.transform.position.x * 1000.0f) / 1000.0f;
        player.position.y = Mathf.Round(gameObject.transform.position.y * 1000.0f) / 1000.0f;
        if (horizontal != 0 || vertical != 0)
        {
            socket.Emit("updatePosition", player.position.ToString());
        }
    }
    private void checkAiming()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dif = mousePosition - transform.position;
        dif.Normalize();
        float rot = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
        if (rot != oldrot)
        {
            oldrot = rot;
            punchPivot.rotation = Quaternion.Euler(0, 0, rot - BARREL_PIVOT_OFFSET);
            socket.Emit("updateRotation", rot + "");
        }
    }

    private void attack()
    {
        if (Input.GetMouseButton(0))
        {
            if (punchRight.localEulerAngles.z == 0 && punchLeft.localEulerAngles.z == 0 && anin.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                lsAttack.Clear();
                anin.Play(typeHand ? "attack 1" : "attack 2");
                StartCoroutine(onOffAttack());
                socket.Emit("attack", typeHand ? "1" : "2");
                if (typeHand)
                {
                    cldPunchRight.enabled = true;
                }
                else
                {
                    cldPunchLeft.enabled = true;
                }
                typeHand = !typeHand;
            }
        }
    }
    IEnumerator onOffAttack()
    {
        yield return new WaitForSeconds(0.35f);
        cldPunchLeft.enabled = false;
        cldPunchRight.enabled = false;
    }
    public void changeStatusView()
    {
        status.GetChild(0).GetComponent<Text>().text = "Exp: " + player.status.exp;
        status.GetChild(1).GetComponent<Text>().text = "Lever: " + player.status.lv;
        status.GetChild(2).GetComponent<Text>().text = "Point: " + player.status.pointLv;
        status.GetChild(3).GetComponent<Text>().text = "Attack: " + player.status.attack;
        status.GetChild(4).GetComponent<Text>().text = "Speed: " + player.status.speed;
        status.GetChild(5).GetComponent<Text>().text = "Hp: " + player.status.hp;
    }
    private void addEventToTextView()
    {
        status.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { clickUpdateStatus("A"); });
        status.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate { clickUpdateStatus("S"); });
        status.GetChild(5).GetComponent<Button>().onClick.AddListener(delegate { clickUpdateStatus("H"); });
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Food"))
        {
            socket.Emit("changeFood", collision.name);
            tinhLv(1);

        }
        if (punchRight.localEulerAngles.z != 0 || punchLeft.localEulerAngles.z != 0)
        {
            bool kt = true;
            foreach (string id in lsAttack)
            {
                if (collision.name.Equals(id))
                {
                    kt = false;
                }
            }
            if (kt && !collision.tag.Equals("Food"))
            {
                socket.Emit("loseHp", collision.name + "|" + player.status.attack + "");
                lsAttack.Add(collision.name);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (player.status.hpNow > 0)
            {
                socket.Emit("updatePosition", player.position.ToString());
            }
        }
    }
    public void tinhLv(int a)
    {
        player.status.exp += a;
        if (player.status.exp >= player.status.lv * 5)
        {
            while (player.status.exp >= player.status.lv * 5)
            {
                if (player.status.lv == 0)
                {
                    player.status.exp -= 1;
                }
                player.status.exp -= player.status.lv * 5;
                player.status.lv += 1;
                player.status.pointLv += 1;
                player.status.hpNow = player.status.hp;
                updateHp();
                socket.Emit("updateHp", player.status.hpNow + "|" + player.status.hp + "");
            }
            checkPoint();
        }
        changeStatusView();
    }
    private void checkPoint()
    {
        if (player.status.pointLv > 0)
        {
            status.GetChild(3).GetComponent<Text>().color = Color.green;
            status.GetChild(4).GetComponent<Text>().color = Color.green;
            status.GetChild(5).GetComponent<Text>().color = Color.green;
        }
        else
        {

            status.GetChild(3).GetComponent<Text>().color = Color.black;
            status.GetChild(4).GetComponent<Text>().color = Color.black;
            status.GetChild(5).GetComponent<Text>().color = Color.black;
        }
    }
    private void clickUpdateStatus(string a)
    {
        if (player.status.pointLv > 0)
        {
            player.status.pointLv -= 1;
            switch (a)
            {
                case "H":
                    player.status.hp += 5;
                    player.status.hpNow += 5;
                    updateHp();
                    socket.Emit("updateHp", player.status.hpNow + "|" + player.status.hp + "");

                    break;
                case "A":
                    player.status.attack += 2;
                    break;
                case "S":
                    player.status.speed += 1;
                    break;
            }
            changeStatusView();
            checkPoint();
        }
    }

    public void updateHp()
    {
        transform.GetChild(2).localScale = new Vector2(((float)player.status.hpNow / (float)player.status.hp) * 10, 1);
    }
}
