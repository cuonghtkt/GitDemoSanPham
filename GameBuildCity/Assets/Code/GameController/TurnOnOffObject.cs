using UnityEngine;

public class TurnOnOffObject : MonoBehaviour {
    // Start is called before the first frame update

    public void turnOnOff(GameObject gameObject) {
        gameObject.active = !gameObject.active;
    }
    public void destroy(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
