using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class Maze
{
    #region Singleton
    private static Maze instance;

    private Maze()
    {
        gameObject = new GameObject();
        gameObject.name = "Maze";
    }

    #endregion

    GameObject gameObject;
    NavMeshSurface navMeshSurface;
    Dictionary<Vector2, Room> seenRooms = new Dictionary<Vector2, Room>();
    Dictionary<Vector2, Room> roomDictionary = new Dictionary<Vector2, Room>();
    public static bool generating = false;

    public Room[] minMaxRooms = new Room[4]; // 0 1 x, 2 3 y
    public Room[] minMaxSeenRooms = new Room[4]; // 0 1 x, 2 3 y
    public static Maze getInstance()
    {
        if (instance == null)
        {
            instance = new Maze();
            instance.gameObject.AddComponent<NavMeshSurface>();
            instance.navMeshSurface = instance.gameObject.GetComponent<NavMeshSurface>();
        }
        return instance;
    }

    public IEnumerator generateMaze()
    {
        if (!generating)
        {
            generating = true;
            if (roomDictionary.Count > 0)
            {
                yield return CoroutineSlave.slave.StartCoroutine(clearMaze());
            }


            int size = 20;
            int i = 0;
            int j = 0;
            int generatedRoomsCount = 0;
            int loopDetection = 0;

            while (generatedRoomsCount < size)
            {
                if (loopDetection >= 10)
                {
                    Room room = getRandomRoom();
                    i = room.coords.x;
                    j = room.coords.y;
                }

                int k = i;
                int q = j;

                int random = UnityEngine.Random.Range(0, 4);
                switch (random)
                {
                    case 0:
                        k++;
                        break;
                    case 1:
                        k--;
                        break;
                    case 2:
                        q++;
                        break;
                    case 3:
                        q--;
                        break;
                }

                if (checkIfRoomCanBeCreated(k, q))
                {
                    Room room = new Room();
                    room.setCoords(k, q);
                    room.setParent(gameObject);
                    generatedRoomsCount++;
                    loopDetection = 0;
                    i = k;
                    j = q;
                    room.gameObject.name += "[" + room.coords.x + ", " + room.coords.y + "]";
                    checkMinMaxRoom(room);
                    roomDictionary.Add(room.coords, room);
                }
                else
                {
                    loopDetection++;
                }

                yield return new WaitForEndOfFrame();
            }

            foreach (KeyValuePair<Vector2, Room> room in roomDictionary)
            {
                setRoomType(room.Value);
                yield return new WaitForEndOfFrame();
            }

            foreach (KeyValuePair<Vector2, Room> room in roomDictionary)
            {
                room.Value.initializeRoom();
                yield return new WaitForEndOfFrame();
            }

          /*  rooms.Sort(delegate (Room room1, Room room2)
            {
                if (room1.coords.x == room2.coords.x && room2.coords.y == room1.coords.y) return 0;
                else if (room1.coords.x < room2.coords.x || room1.coords.x <= room2.coords.x && room1.coords.y < room2.coords.y) return -1;
                else if (room1.coords.x > room2.coords.x || room1.coords.x >= room2.coords.x && room1.coords.y > room2.coords.y) return 1;
                else return 0;
            });*/

            navMeshSurface.BuildNavMesh();

            foreach (KeyValuePair<Vector2, Room> room in roomDictionary)
            {
                room.Value.gameObject.SetActive(false);
                yield return new WaitForEndOfFrame();
            }

            Room.currentRoom = getRandomRoom();
            Map.getInstance().fillMap();
            changeRoom(Room.currentRoom);
            Player.getInstance().GetPlayerController().playerWarp(Room.currentRoom.gameObject.transform.position + new Vector3(0, 50, 0));
            yield return new WaitForSeconds(0.5f);
            Map.getInstance().show();
            CoroutineSlave.slave.StartCoroutine(Map.getInstance().spawnButton());
            yield return CoroutineSlave.slave.StartCoroutine(Map.getInstance().updateMap());

            generating = false;
        }

        yield return 0;
    }

    public Room getRandomRoom()
    {
        Room room;
        List<Room> rooms = new List<Room>(roomDictionary.Values);
        room = rooms[UnityEngine.Random.Range(0, rooms.Count)];
        return room;
    }
    void checkMinMaxRoom(Room room)
    {

        if (minMaxRooms[0] == null)
        {
            minMaxRooms[0] = room;
        }
        if (minMaxRooms[1] == null)
        {
            minMaxRooms[1] = room;
        }
        if (minMaxRooms[2] == null)
        {
            minMaxRooms[2] = room;
        }
        if (minMaxRooms[3] == null)
        {
            minMaxRooms[3] = room;
        }

        if (minMaxRooms[0].coords.x < room.coords.x)
        {
            minMaxRooms[0] = room;
        }
        if (minMaxRooms[1].coords.x > room.coords.x)
        {
            minMaxRooms[1] = room;
        }
        if (minMaxRooms[2].coords.y < room.coords.y)
        {
            minMaxRooms[2] = room;
        }
        if (minMaxRooms[3].coords.y > room.coords.y)
        {
            minMaxRooms[3] = room;
        }
    }
    void checkSeenRoom(Room room)
    {
        if (minMaxSeenRooms[0] == null)
        {
            minMaxSeenRooms[0] = room;
        }
        if (minMaxSeenRooms[1] == null)
        {
            minMaxSeenRooms[1] = room;
        }
        if (minMaxSeenRooms[2] == null)
        {
            minMaxSeenRooms[2] = room;
        }
        if (minMaxSeenRooms[3] == null)
        {
            minMaxSeenRooms[3] = room;
        }

        if (minMaxSeenRooms[0].coords.x < room.coords.x)
        {
            minMaxSeenRooms[0] = room;
        }
        if (minMaxSeenRooms[1].coords.x > room.coords.x)
        {
            minMaxSeenRooms[1] = room;
        }
        if (minMaxSeenRooms[2].coords.y < room.coords.y)
        {
            minMaxSeenRooms[2] = room;
        }
        if (minMaxSeenRooms[3].coords.y > room.coords.y)
        {
            minMaxSeenRooms[3] = room;
        }
    }

    void checkShowOnMiniMap(Room room)
    {
        if(Mathf.Abs(room.coords.x - Room.currentRoom.coords.x) < 3 && Mathf.Abs(room.coords.y - Room.currentRoom.coords.y) < 3)
        {
            room.showOnMiniMap = true;
        }
        else
        {
            room.showOnMiniMap = false;
        }
    }
    void addSeenRoom(Room room)
    {
        try
        {
            seenRooms.Add(room.coords, room);
        }
        catch (ArgumentException e) { }
    }

    public void changeRoom(Room newRoom)
    {
        Room oldRoom = Room.currentRoom;
        Map.getInstance().addSeenCell(oldRoom.mapCell);
        Map.getInstance().addSeenCell(newRoom.mapCell);

        foreach (Room room in Room.currentRoom.connectedRooms)
        {
            if (room != null)
            {
                room.seen = true;
                addSeenRoom(room);
                Map.getInstance().addSeenCell(room.mapCell);
                checkSeenRoom(room);
                room.gameObject.SetActive(false);
                foreach (Room connectedRoom in room.connectedRooms)
                {
                    if (connectedRoom != null)
                    {
                        addSeenRoom(connectedRoom);
                        connectedRoom.gameObject.SetActive(false);
                    }
                }
            }
        }

        Room.currentRoom = newRoom;
        Room.currentRoom.gameObject.SetActive(true);
        oldRoom.seen = true;
        newRoom.seen = true;
        addSeenRoom(oldRoom);
        addSeenRoom(newRoom);
        newRoom.defeated = true;


        foreach (Room room in Room.currentRoom.connectedRooms)
        {
            if (room != null)
            {
                room.seen = true;
                Map.getInstance().addSeenCell(room.mapCell);
                checkSeenRoom(room);
                addSeenRoom(room);
                room.gameObject.SetActive(true);
                foreach (Room connectedRoom in room.connectedRooms)
                {
                    if (connectedRoom != null)
                    {
                        addSeenRoom(connectedRoom);
                        connectedRoom.gameObject.SetActive(true);
                    }
                }
            }
        }

        foreach (KeyValuePair<Vector2, Room> room in seenRooms)
        {
            checkShowOnMiniMap(room.Value);
        }

        CoroutineSlave.slave.StartCoroutine(Map.getInstance().updateMap());

    }

    public IEnumerator clearMaze()
    {
        List<Room> rooms = new List<Room>(roomDictionary.Values);
        foreach (Room room in rooms)
        {
            MonoBehaviour.Destroy(room.gameObject);
            yield return new WaitForEndOfFrame();
        }
        roomDictionary.Clear();
        yield return 0;
    }
    bool checkIfRoomCanBeCreated(int k, int q)
    {
        Room room;
        return !roomDictionary.TryGetValue(new Vector2(k, q), out room);
    }

    public static Room findRoomByCoords(Vector2 coords)
    {
        Room room;
        getInstance().roomDictionary.TryGetValue(coords, out room);
        return room;
    }

    public Dictionary<Vector2, Room> getRooms()
    {
        return roomDictionary;
    }
    public Dictionary<Vector2, Room> getSeenRooms()
    {
        return seenRooms;
    }
    void setRoomType(Room room)
    {
        Room r;

        if(roomDictionary.TryGetValue(new Vector2(room.coords.x + 1, room.coords.y), out r)) // check up
        {
            room.type += "N";
        }
        if (roomDictionary.TryGetValue(new Vector2(room.coords.x - 1, room.coords.y), out r)) // check down
        {
            room.type += "S";
        }
        if (roomDictionary.TryGetValue(new Vector2(room.coords.x, room.coords.y + 1), out r)) // check right
        {
            room.type += "E";
        }
        if (roomDictionary.TryGetValue(new Vector2(room.coords.x, room.coords.y - 1), out r)) // check left
        {
            room.type += "W";
        }
        room.gameObject.name += room.type;
    }
}
