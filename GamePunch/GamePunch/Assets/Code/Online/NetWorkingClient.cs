using SocketIO;
using System;

public class NetWorkingClient : SocketIOComponent {
    // Start is called before the first frame update
    public override void Start() {
        base.Start();
    }

    // Update is called once per frame
    public override void Update() {
        base.Update();
    }
}

[Serializable]
public class PlayerData {
    public string id;
    public Position position = new Position();
    public Status status = new Status();
    public PlayerData()
    {
        position = new Position();
        status= new Status();
    }
}

[Serializable]
public class Position {
    public float x;
    public float y;
    public Position()
    {
        x = 0;
        y = 0;
    }
    public override string ToString() {
        return x + "|" + y;
    }
}

[Serializable]
public class Status
{
    public int lv = 0;
    public int pointLv = 0;
    public int exp = 0;
    public int hp = 0;
    public int attack = 0;
    public int speed = 0;
    public int hpNow;
    public Status()
    {
        hp = 10;
        hpNow = hp;
        attack = 2;
        speed = 3;
    }
}
