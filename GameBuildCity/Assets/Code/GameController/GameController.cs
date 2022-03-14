using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public List<Sprite> lsImageViewHouse;
    public List<GameObject> lsObjectSaveHouse;
    public GameObject imageViewSave;
    public RectTransform allViewHouse;
    public static GameObject showNow;
    List<HouseData2> dataHouses;
    public int coin;
    public TextMeshProUGUI textCoin;

    //map orther
    public GameObject allMap;
    public static int mapNow = 0;
    public GameObject mapObject;
    public List<int> lsSaveMap;
    // Start is called before the first frame update
    [System.Obsolete]
    void Start() {
        mapObject.GetComponent<SpriteRenderer>().sprite = allMap.transform.GetChild(mapNow).gameObject.GetComponent<Image>().sprite;
        changeCoin(SaveSystem2.LoadGameData());
        dataHouses = SaveSystem2.LoadListHouse();
        loadMap();
        GameObject houseView, houseObject;
        for (int i = 0; i < dataHouses.Count; i++) {
            houseView = funCreateHouse(dataHouses[i].typeHouse - 5 * mapNow);
            if (dataHouses[i].use == 1) {
                houseObject = Instantiate(lsObjectSaveHouse[dataHouses[i].typeHouse]);
                houseView.GetComponent<HouseController>().loadHouseSave(dataHouses[i], houseObject);
            }
        }
    }
    private void loadMap() {
        lsSaveMap = SaveSystem2.LoadLisMap();
        foreach (int obj in lsSaveMap) {
            allMap.transform.GetChild(obj).gameObject.SetActive(true);
        }
    }

    [System.Obsolete]
    private void saveData() {
        dataHouses.Clear();
        HouseController[] allChildren = GameObject.Find("View").GetComponentsInChildren<HouseController>();
        foreach (HouseController obj in allChildren) {
            dataHouses.Add(obj.data);
        }
        SaveSystem2.SaveListHouse(dataHouses, lsSaveMap, coin);
    }

    [System.Obsolete]
    private void OnApplicationPause(bool pause) {
        if (pause) {
            saveData();
        }
    }

    [System.Obsolete]
    private void OnApplicationQuit() {
        saveData();
    }

    public void changeCoin(int value) {
        coin += value;
        textCoin.text = coin + "";
    }

    [System.Obsolete]
    public void buyHouseSuccess(int i) {
        funCreateHouse(i);
    }

    [System.Obsolete]
    private GameObject funCreateHouse(int i) {
        i = i + 5 * mapNow;
        GameObject newHouseView = Instantiate(imageViewSave, imageViewSave.transform.position, Quaternion.identity);
        newHouseView.name = allViewHouse.GetChildCount() + "";
        newHouseView.transform.SetParent(allViewHouse);
        newHouseView.transform.localScale = new Vector3(1, 1, 1);
        newHouseView.transform.localPosition = new Vector3(0, 0, 0);
        newHouseView.GetComponent<HouseController>().data.typeHouse = i;
        newHouseView.GetComponent<Image>().sprite = lsImageViewHouse[i];
        allViewHouse.sizeDelta += new Vector2(110, 0);
        return newHouseView;
    }

    [System.Obsolete]
    public void moveMap(int i) {
        saveData();
        mapNow = i;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
