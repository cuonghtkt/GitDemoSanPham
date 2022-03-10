using SocketIO;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SocketIOComponent socket;
    public List<GameObject> lsObjectSave;
    public GameObject playerAll;
    public GameObject foodAll;

    public Dictionary<string, GameObject> lsPlayer;
    public GameObject player;

    const float BARREL_PIVOT_OFFSET = 90.0f;
    // Start is called before the first frame update
    private void Awake()
    {
        socket = gameObject.GetComponent<NetWorkingClient>();
        lsPlayer = new Dictionary<string, GameObject>();
        socket.On("new_player_id", (E) =>
        {
            player = Instantiate(lsObjectSave[0], new Vector3(0, 0, 0), Quaternion.identity);
            player.name = E.data.ToString().Replace("\"", "");
            GameObject.Find("Main Camera").transform.SetParent(player.transform);
        });
        socket.On("new_player", (E) =>
        {
            GameObject playerEni = Instantiate(lsObjectSave[0], new Vector3(0, 0, 0), Quaternion.identity);
            playerEni.name = E.data.ToString().Replace("\"", "");
            Destroy(playerEni.GetComponent<Player>());
            lsPlayer.Add(playerEni.name, playerEni);
        });
        socket.On("updatePosition", (E) =>
        {
            string[] data = E.data.ToString().Replace("\"", "").Split('|');
            string id = data[0];
            float x = float.Parse(data[1]);
            float y = float.Parse(data[2]);
            GameObject ni = lsPlayer[id];
            ni.transform.position = new Vector3(x, y, 0);
        });
        socket.On("updateRotation", (E) =>
        {
            string[] data = E.data.ToString().Replace("\"", "").Split('|');
            string id = data[0];
            GameObject ni = lsPlayer[id];
            ni.transform.GetChild(1).rotation = Quaternion.Euler(0, 0, float.Parse(data[1]) - BARREL_PIVOT_OFFSET);
        });
        socket.On("attack", (E) =>
        {
            string[] data = E.data.ToString().Replace("\"", "").Split('|');
            string id = data[0];
            GameObject ni = lsPlayer[id];
            ni.transform.GetComponent<Animator>().Play(data[1] == "1" ? "attack 1" : "attack 2");
        });

        socket.On("loseHp", (E) =>
        {
            Debug.Log("asdf" + E.data);
            string[] data = E.data.ToString().Replace("\"", "").Split('|');
            string id = data[0];
            if (id.Equals(player.name))
            {
                if (data[1].Equals("0"))
                {
                    player.transform.position = new Vector2(0, 0);
                    player.GetComponent<Player>().player = new PlayerData();
                    data[1] = player.GetComponent<Player>().player.status.hp + "";
                    socket.Emit("updatePosition", player.GetComponent<Player>().player.position.ToString());
                    player.GetComponent<Player>().changeStatusView();
                }
                else
                {
                    player.GetComponent<Player>().player.status.hpNow = System.Convert.ToInt16((System.Convert.ToDouble(data[1].Replace(".", ",")) * player.GetComponent<Player>().player.status.hp) / 10);
                    player.GetComponent<Player>().restore = 3;
                    player.GetComponent<Player>().updateHp();
                }
            }
            else
            {
                GameObject ni = lsPlayer[id];
                if (data[1].Equals("0"))
                {
                    ni.transform.position = new Vector2(0, 0);
                    ni.transform.GetChild(2).localScale = new Vector2(10, 0);
                    data[1] = "10";
                    player.GetComponent<Player>().tinhLv(10);
                }
                else
                {
                    ni.transform.GetChild(2).localScale = new Vector2(float.Parse(data[1].Replace(".", ",")), 1);
                }

            }
        });
        socket.On("restoreHp", (E) =>
        {
            Debug.Log("asd" + E.data.ToString());
            string[] data = E.data.ToString().Replace("\"", "").Split('|');
            if (data[0].Equals(player.name))
            {
                player.GetComponent<Player>().player.status.hpNow += 1;
                player.GetComponent<Player>().updateHp();
            }
            else
            {
                GameObject ni = lsPlayer[data[0]];
                ni.transform.GetChild(2).localScale = new Vector2(float.Parse(data[1].Replace(".", ",")), 1);
            }
        });
        socket.On("updateStatus", (E) =>
        {
            string[] data = E.data.ToString().Replace("\"", "").Split('|');
            string id = data[0];
            GameObject ni = lsPlayer[id];
            ni.transform.GetChild(2).localScale = new Vector2(float.Parse(data[1].Replace(".", ",")), 1);
        });
        socket.On("changeFood", (E) =>
        {
            string[] data = E.data.ToString().Replace("\"", "").Split('|');
            if (foodAll.transform.childCount < 100)
            {
                GameObject obj = Instantiate(lsObjectSave[1], new Vector3(float.Parse(data[1]), float.Parse(data[2]), 0), Quaternion.identity, foodAll.transform) as GameObject;
                obj.name = data[0];
            }
            else
            {
                foodAll.transform.GetChild(int.Parse(data[0])).position = new Vector2(float.Parse(data[1]), float.Parse(data[2]));
            }
        });
        socket.On("player_disconnect", (E) =>
        {
            Destroy(lsPlayer[E.data.ToString().Replace("\"", "")]);
            lsPlayer.Remove(E.data.ToString().Replace("\"", ""));
        });

    }
}
