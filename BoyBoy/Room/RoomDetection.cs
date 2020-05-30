using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetection : MonoBehaviour
{
    Room room;
    public Room getRoom()
    {
        return room;
    }
    public void setRoom(Room room)
    {
        this.room = room;
    }
}
