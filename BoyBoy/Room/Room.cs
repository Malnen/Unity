using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room 
{
    public static Room currentRoom;
    public Vector2Int coords;
    Floor floor;
    Wall northWall;
    Wall southWall;
    Wall eastWall;
    Wall westWall;
    RoomLayout roomLayout;
    public GameObject gameObject;
    public string type;
    int scale = 100;
    public bool seen = false;
    public bool defeated = false;
    public Cell mapCell;
    public Room[] connectedRooms = new Room[4];
    public bool showOnMiniMap = false;


    public Room()
    {
        gameObject = new GameObject();
        gameObject.name = "Room";
    }

    public void setParent(GameObject parent)
    {
        gameObject.transform.parent = parent.transform;
    }

    void moveObjectToPosition()
    {
        gameObject.transform.localPosition = new Vector3(coords.x, 0, coords.y) * scale;
        gameObject.transform.localScale *= scale;
    }

    public void setCoords(int i, int j)
    {
        coords = new Vector2Int(i, j);
        moveObjectToPosition();
    }

    void findConnectedRooms()
    {
        connectedRooms[0] = Maze.findRoomByCoords(new Vector2(coords.x + 1, coords.y));
        connectedRooms[1] = Maze.findRoomByCoords(new Vector2(coords.x - 1, coords.y));
        connectedRooms[2] = Maze.findRoomByCoords(new Vector2(coords.x, coords.y + 1));
        connectedRooms[3] = Maze.findRoomByCoords(new Vector2(coords.x, coords.y - 1));
    }
    public void initializeRoom()
    {
        findConnectedRooms();

        if (floor == null)
        {
            floor = new Floor(this);
        }

        if (northWall == null)
        {
            if (type.Contains("N"))
            {
                northWall = new Door(gameObject, "North"); 
                if (connectedRooms[0] != null)
                {
                    connectedRooms[0].southWall = northWall;
                }
            }
            else
            {
                northWall = new Wall(gameObject, "North");
            }
        }

        if (southWall == null)
        {
            if (type.Contains("S"))
            {
                southWall = new Door(gameObject, "South");
                if (connectedRooms[1] != null)
                {
                    connectedRooms[1].northWall = southWall;
                }
            }
            else
            {
                southWall = new Wall(gameObject, "South");
            }
        }

        if (eastWall == null)
        {
            if (type.Contains("E"))
            {
                eastWall = new Door(gameObject, "East");
                if (connectedRooms[2] != null)
                {
                    connectedRooms[2].westWall = eastWall;
                }
            }
            else
            {
                eastWall = new Wall(gameObject, "East");
            }
        }

        if (westWall == null)
        {
            if (type.Contains("W"))
            {
                westWall = new Door(gameObject, "West");
                if (connectedRooms[3] != null)
                {
                    connectedRooms[3].eastWall = westWall;
                }
            }
            else
            {
                westWall = new Wall(gameObject, "West");
            }
        }

        westWall.setScaleOne();
        eastWall.setScaleOne();
        northWall.setScaleOne();
        southWall.setScaleOne();

        chooseRoomLayout();
    }


    void chooseRoomLayout()
    {
        if (type.Length == 4)
        {
            roomLayout = new RoomLayoutFourDoors(gameObject);
        }
        if (type.Length == 3)
        {
            roomLayout = new RoomLayoutThreeDoors(gameObject);
        }
        if (type.Length == 2)
        {
            if (type.Contains("N") && type.Contains("S") || type.Contains("E") && type.Contains("W"))
            {
                roomLayout = new RoomLayoutTwoDoorsOpposite(gameObject);
            }
            else
            {
                roomLayout = new RoomLayoutTwoDoors(gameObject);
            }
        }
        if (type.Length == 1)
        {
            roomLayout = new RoomLayoutOneDoor(gameObject);
        }
    }
}
