using UnityEngine;

public class ShopController : MonoBehaviour {
    GameController gameController;
   public GameObject notenough, canvas;
    public int costByHouse;
    public int costByLane;
    void Start() {
        gameController = gameObject.GetComponent<GameController>();
    }

    [System.Obsolete]
    public void buyHouse() {
        Debug.Log("a" + gameController.coin * (GameController.mapNow + 1) + "|" + costByHouse);
        if (gameController.coin  < costByHouse * (GameController.mapNow + 1)) {
            Debug.Log("Khong mua dc");
            Instantiate(notenough, new Vector3(0, 0, canvas.transform.position.z), Quaternion.identity,canvas.transform);
        } else {
            gameController.changeCoin(-(costByHouse * (GameController.mapNow + 1)));
            var i = Random.Range(1f, 100f);
            if (i <= 65) {
                gameController.buyHouseSuccess(0);
            } else if (i <= 90) {
                gameController.buyHouseSuccess(1);
            } else if (i <= 98.8f) {
                gameController.buyHouseSuccess(2);
            } else if (i <= 99.9f) {
                gameController.buyHouseSuccess(3);
            } else if (i <= 100) {
                gameController.buyHouseSuccess(4);
            }
        }

    }
    [System.Obsolete]
    public void buyLane() {
        if (gameController.coin < costByLane || gameController.lsSaveMap.Count+1 >= gameController.allMap.transform.childCount) {
            Debug.Log("Khong mua dc");
            Instantiate(notenough, new Vector3(0, 0, canvas.transform.position.z), Quaternion.identity, canvas.transform);
        } else {
            gameController.changeCoin(-costByLane);
            var i = 0;
            while (true) {
                i = (int)Random.Range(1f, 5f);
                bool kt = true;
                foreach (int obj in gameController.lsSaveMap) {
                    Debug.Log("test2" + i + "|" + obj);
                    if (i == obj) {
                        kt = false;
                    }
                }
                if (kt) {
                    break;
                }
            }
            gameController.allMap.transform.GetChild(i).gameObject.SetActive(true);
            gameController.lsSaveMap.Add(i);
        }

    }

}
