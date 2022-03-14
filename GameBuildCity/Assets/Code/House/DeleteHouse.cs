using UnityEngine;

public class DeleteHouse : MonoBehaviour {
    public CheckHouseMatch checkHouseMatch;
    private void Start() {
        checkHouseMatch = gameObject.transform.parent.parent.gameObject.GetComponent<CheckHouseMatch>();
    }
    private void OnMouseDown() {
        checkHouseMatch.houseController.delete();
    }
}
