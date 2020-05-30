using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor
{
    GameObject gameObject;
    public string type;
    RoomDetection roomDetection;
    public Floor(Room room)
    {
        gameObject = chooseFloor();
        gameObject.transform.parent = room.gameObject.transform;
        gameObject.name = "Floor";
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localScale = Vector3.one;
        gameObject.isStatic = true;
        gameObject.AddComponent<RoomDetection>();
        roomDetection = gameObject.GetComponent<RoomDetection>();
        roomDetection.setRoom(room);
    }

    public Room getRoom()
    {
        return roomDetection.getRoom();
    }
    GameObject chooseFloor()
    {
        GameObject floor;
        int random = Random.Range(0, 1);
        type = "PrefabFloor" + random;
        floor = MonoBehaviour.Instantiate((GameObject)Resources.Load("BlenderObjects/" + type));
       
        return floor;
    }
}
