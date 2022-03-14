using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class dulieugame {
    public int coin=1000;
    public int score, Hp = 3;
    public int hatThong;
    public float vol, volbackground;
    public int playerNow;
    public string name;
    public bool skill1, skill2, skill3, skill4, skill5;
    public bool player1, player2, player3;
    public bool trangbi1, trangbi2, trangbi3;
    public int mapDaMo;
};
public class SaveData : MonoBehaviour {

    public static SaveData instance;
    public static dulieugame dataNow = new dulieugame();
    public dulieugame dataNow2 = new dulieugame();

    private void Awake() {
        dataNow = LoadData();
        if (instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }
    private void Start() {
        dataNow2 = dataNow;
    }
    private void Update() {
        dataNow = dataNow2;

        if (Input.GetKey(KeyCode.P)) {
            resetdata();
        }
        if (Input.GetKey(KeyCode.O)) {
            SaveDataFunction();
        }
    }
    public static void SaveDataFunction() {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, dataNow);
        stream.Close();
        Debug.Log("Save thanh cong");
    }
    public static dulieugame LoadData() {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            dulieugame dulieugame = formatter.Deserialize(stream) as dulieugame;
            stream.Close();
            Debug.Log("Load thanh cong");
            dulieugame.hatThong = 100;
            return dulieugame;
        } else {
            return new dulieugame();
        }
    }
    public void resetdata() {
        dataNow = new dulieugame();
        dataNow.score = 1;
        dataNow.coin = 5;
        SaveDataFunction();
        dataNow = LoadData();
    }

}
