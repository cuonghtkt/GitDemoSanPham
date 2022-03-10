using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eni : MonoBehaviour
{
    public GameObject player;
    public Transform listEni;
    public GameControllerOff gameControllerOff;
    private Vector3 wayPointPos;

    public int hp = 10;
    public int speed = 3;
    public int hpNow;
    public bool protect = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        listEni = GameObject.Find("ListEni").transform;
        gameControllerOff = GameObject.Find("GameController").GetComponent<GameControllerOff>();
        hp = System.Convert.ToInt16(System.Convert.ToDouble(hp) * GameControllerOff.leverGame);
        hpNow = hp;
        speed = System.Convert.ToInt16((GameControllerOff.leverGame / 2.5 < 1 ? 1 : GameControllerOff.leverGame / 2.5) * speed);
    }

    // Update is called once per frame
    void Update()
    {
        wayPointPos = new Vector2(player.transform.position.x, player.transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("PlayerPunch") && !protect)
        {
            StartCoroutine(waitProtect());
            hpNow -= player.GetComponent<PlayerOff>().player.status.attack;
            transform.GetChild(2).localScale = new Vector2(((float)hpNow / (float)hp) * 10, 1);
            if (hpNow <= 0)
            {
                Destroy(gameObject);
                player.GetComponent<PlayerOff>().tinhLv(10);
                if (gameControllerOff != null)
                {
                    GameControllerOff.leverGame += 0.2f;
                    if (listEni.childCount <= 1)
                    {
                        gameControllerOff.funCreateEni();
                    }
                }
            }
        }
        IEnumerator waitProtect()
        {

            protect = true;
            yield return new WaitForSeconds(.5f);
            protect = false;
        }
    }
}
