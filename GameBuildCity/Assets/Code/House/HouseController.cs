using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HouseController : MonoBehaviour {
    public HouseData2 data = new HouseData2();
    //data game 
    public int cointGet;
    public int timeClick;
    //get house save
    GameController gameController;
    //createhouse
    GameObject house;
    //movehouse
    Ray ray;
    RaycastHit2D[] hit2D;
    bool move = false;
    Vector3 vector3;
    //turn on/off move view house
    ScrollRect scrollRectView;

    //change color
    Image image;
    SpriteRenderer imageHouseObject, colorClaim, colorUpdate;
    //checkmatchhouse
    CheckHouseMatch checkMatchHouse;
    //int cost buy house;
    int cointBuyHouse;
    //changeText
    TextMeshPro textClaim, textUpdate;
    private void Awake() {
        image = gameObject.GetComponent<Image>();
    }
    private void Start() {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        scrollRectView = GameObject.Find("ScrollView").GetComponent<ScrollRect>();
        cointBuyHouse = gameController.GetComponent<ShopController>().costByHouse;
    }
    private void Update() {
        if (move) {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit2D = Physics2D.GetRayIntersectionAll(ray);
            if (hit2D.Length > 0) {
                vector3 = new Vector3();
                vector3 = hit2D[0].point;
                vector3.z = 0;
                house.transform.position = vector3;
                if (hit2D.Length > 2 && checkMatchHouse.checkMatch == false) {
                    imageHouseObject.color = new Color(imageHouseObject.color.r, imageHouseObject.color.g, imageHouseObject.color.b, 1f);
                } else {
                    imageHouseObject.color = new Color(imageHouseObject.color.r, imageHouseObject.color.g, imageHouseObject.color.b, 0.5f);
                }
            }

        }
    }
    private void OnMouseDown() {
        scrollRectView.enabled = false;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
        if (data.use == 0) {
            house = Instantiate(gameController.lsObjectSaveHouse[data.typeHouse]);
            house.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            house.GetComponent<CheckHouseMatch>().houseController = this;
            imageHouseObject = house.GetComponent<SpriteRenderer>();
            checkMatchHouse = house.GetComponent<CheckHouseMatch>();
            colorUpdate = house.transform.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
            colorClaim = house.transform.GetChild(0).GetChild(1).gameObject.GetComponent<SpriteRenderer>();
            textUpdate = house.transform.GetChild(0).GetChild(4).gameObject.GetComponent<TextMeshPro>();
            textClaim = house.transform.GetChild(0).GetChild(3).gameObject.GetComponent<TextMeshPro>();
            timeClick = house.GetComponent<CheckHouseMatch>().timeClick;
            cointGet = house.GetComponent<CheckHouseMatch>().coinGet;
            data.timeClickRun = timeClick;
            checkUpdate();
            move = true;
        }
    }
    public void loadHouseSave(HouseData2 houseDataSave, GameObject house) {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
        this.house = house;
        data = houseDataSave;
        house.GetComponent<CheckHouseMatch>().houseController = this;
        imageHouseObject = house.GetComponent<SpriteRenderer>();
        checkMatchHouse = house.GetComponent<CheckHouseMatch>();
        colorUpdate = house.transform.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        colorClaim = house.transform.GetChild(0).GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        textUpdate = house.transform.GetChild(0).GetChild(4).gameObject.GetComponent<TextMeshPro>();
        textClaim = house.transform.GetChild(0).GetChild(3).gameObject.GetComponent<TextMeshPro>();
        timeClick = house.GetComponent<CheckHouseMatch>().timeClick;
        cointGet = house.GetComponent<CheckHouseMatch>().coinGet;
        house.transform.position = new Vector3(data.positon[0], data.positon[1], data.positon[2]);
        StartCoroutine(runClickTime());
    }
    private void OnMouseUp() {
        move = false;
        scrollRectView.enabled = true;
        if (hit2D != null) {
            if (hit2D.Length <= 2 || checkMatchHouse.checkMatch == true) {
                Destroy(house);
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
            } else {
                Destroy(house.GetComponent<Rigidbody2D>());
                data.use = 1;
                data.positon[0] = vector3.x;
                data.positon[1] = vector3.y;
                data.positon[2] = vector3.z;
                StartCoroutine(runClickTime());
            }
        }
    }
    public IEnumerator runClickTime() {
        yield return new WaitForSeconds(1);
        data.timeClickRun -= 1;
        if (data.timeClickRun > 0) {
            StartCoroutine(runClickTime());
        } else {
            colorClaim.color = new Color(0.3f, 0.9f, 0.1f);
        }
    }
    public void claim() {
        if (data.timeClickRun <= 0) {
            data.timeClickRun = timeClick;
            colorClaim.color = new Color(1f, 0.9f, 0f);
            gameController.changeCoin(cointGet * data.lever);
            house.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(runClickTime());
        }
    }
    public void delete() {
        Destroy(house);
        data.use = 0;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
    }
    public void update() {
        if (cointBuyHouse * (((data.typeHouse + 1) + data.lever)) < gameController.coin && data.lever < 10) {
            gameController.changeCoin(-cointBuyHouse * (((data.typeHouse + 1) + data.lever)));
            data.lever += 1;
            house.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void checkUpdate() {
        textClaim.text = "Claim: " + cointGet * data.lever + "";
        textUpdate.text = "Update: " + cointBuyHouse * (((data.typeHouse + 1) + data.lever)) + "";
        if (cointBuyHouse * (((data.typeHouse + 1) + data.lever)) < gameController.coin && data.lever < 10) {
            colorUpdate.color = new Color(0.3f, 0.9f, 0.1f);
        } else {
            colorUpdate.color = new Color(1f, 0.9f, 0f);
        }


    }
}
