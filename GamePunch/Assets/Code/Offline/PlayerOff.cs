using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerOff : MonoBehaviour
{
    public PlayerData player;
    //attack
    public GameObject dead;
    public Transform punchPivot, punchRight, punchLeft;
    public Collider2D cldPunchLeft, cldPunchRight;
    public bool typeHand = true;
    const float BARREL_PIVOT_OFFSET = 90.0f;
    private float oldrot = 0;
    public Animator anin;
    public Transform status;
    public bool protect = false;
    public int restore = 0;
    private void Awake()
    {
    }
    void Start()
    {
        player = new PlayerData();
        player.id = gameObject.name;
        anin = gameObject.GetComponent<Animator>();
        changeStatusView();
        addEventToTextView();
        //di chuyen 1 it de khong bi trung len nhau
        transform.position += new Vector3(1, 1, 0) * 0.001f;
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
            player.status.hpNow += 10*player.status.hp/100;
            
            if(player.status.hpNow > player.status.hp)
            {
                player.status.hpNow = player.status.hp;
            }
            updateHp();
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
        }
    }

    private void attack()
    {
        if (Input.GetMouseButton(0))
        {
            if (punchRight.localEulerAngles.z == 0 && punchLeft.localEulerAngles.z == 0 && anin.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                anin.Play(typeHand ? "attack 1" : "attack 2");
                StartCoroutine(onOffAttack());
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
    private void changeStatusView()
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.name.Equals("Player") && !protect && collision.collider.tag.Equals("Eni"))
        {
            player.status.hpNow -= System.Convert.ToInt16(2 * GameControllerOff.leverGame);// (GameControllerOff.leverGame/2<1?1: GameControllerOff.leverGame/2)
            restore = 3;
            StartCoroutine(waitProtect());
            updateHp();
            if (player.status.hpNow <= 0)
            {
                Time.timeScale = 0;
                GameControllerOff.leverGame = 1f;
               GameObject deadCanves =  Instantiate(dead, new Vector2(0,0), Quaternion.identity, GameObject.Find("Canvas").transform);
                deadCanves.transform.localPosition = new Vector2(0, 0);
            }
        }
    }
    IEnumerator waitProtect()
    {
        protect = true;
        yield return new WaitForSeconds(.5f);
        protect = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Food"))
        {
            tinhLv(1);
            collision.gameObject.transform.position = new Vector2(Random.Range(-14.5f, 14.6f), Random.Range(-14.5f, 14.6f));
        }
        if (collision.tag.Equals("Eni") && !protect)
        {
            player.status.hpNow -= System.Convert.ToInt16(1 * GameControllerOff.leverGame);// (GameControllerOff.leverGame/2<1?1: GameControllerOff.leverGame/2)
            restore = 3;
            StartCoroutine(waitProtect());
            updateHp();
            if (player.status.hpNow <= 0)
            {
                SceneManager.LoadScene(2, LoadSceneMode.Single);
                GameControllerOff.leverGame = 1f;
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
    private void updateHp()
    {
        transform.GetChild(2).localScale = new Vector2(((float)player.status.hpNow / (float)player.status.hp) * 10, 1);
    }
}
