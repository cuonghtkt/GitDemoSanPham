using UnityEngine;

public class CheckHouseMatch : MonoBehaviour {
    //check not match object
    public bool checkMatch = false;
    public HouseController houseController;
    public int coinGet, timeClick;
    [System.Obsolete]
    private void OnMouseDown() {
        if (gameObject.transform.GetChild(0).gameObject.active) {
            GameController.showNow.active = false;
        } else {
            houseController.checkUpdate();
            if (GameController.showNow != null) {
                GameController.showNow.active = false;
            }
            GameController.showNow = gameObject.transform.GetChild(0).gameObject;
            GameController.showNow.active = true;
        }

    }
    private void OnTriggerStay2D(Collider2D collision) {
        if (enabled) {
            if (collision.gameObject.name != "Map" && collision.gameObject.name != "PlaceHouseMove") {
                checkMatch = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (enabled) {
            if (collision.gameObject.name != "Map" && collision.gameObject.name != "PlaceHouseMove") {
                checkMatch = false;
            }
        }
    }
}
