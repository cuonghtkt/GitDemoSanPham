using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem2 : MonoBehaviour {
    public static void SaveListHouse(List<HouseData2> lsHouse, List<int> saveMap, int coint) {
        if (lsHouse != null) {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + GameController.mapNow + "house.fun";
            FileStream stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, lsHouse);

            path = Application.persistentDataPath + "/data.fun";
            stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, coint);

            path = Application.persistentDataPath + "/map.fun";
            stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, saveMap);
            Debug.Log(Application.persistentDataPath);
            stream.Close();
        }
    }
    public static List<HouseData2> LoadListHouse() {
        string path = Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + GameController.mapNow + "house.fun";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<HouseData2> data = formatter.Deserialize(stream) as List<HouseData2>;
            //data.Clear();
            stream.Close();
            return data;
        } else {
            return new List<HouseData2>();
        }
    }
    public static List<int> LoadLisMap() {
        string path = Application.persistentDataPath + "/map.fun";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<int> data = formatter.Deserialize(stream) as List<int>;
            //data.Clear();
            stream.Close();
            return data;
        } else {
            return new List<int>();
        }
    }

    public static int LoadGameData() {
        string path = Application.persistentDataPath + "/data.fun";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            int coint = (int)formatter.Deserialize(stream);
            stream.Close();
            return coint;
        } else {
            return 1000;
        }
    }
}
