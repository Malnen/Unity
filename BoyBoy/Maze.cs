using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    List<Room> rooms = new List<Room>();
    public static Maze getInstance()
    {
        if (instance == null)
        {
            instance = new Maze();
        }
        return instance;
    }

    public IEnumerator generateMaze()
    {
        int i = 10;
        int j = 10;

        int generatedRoomsCount = 0;
        int loopDetection = 0;

        while (generatedRoomsCount < 100)
        {
            if (loopDetection >= 10)
            {
                Room room = rooms[Random.Range(0, rooms.Count)];
                i = room.coords.x;
                j = room.coords.y;
            }

            int k = i;
            int q = j;

            int random = Random.Range(0, 4);
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
                rooms.Add(room);
                room.setCoords(k, q);
                room.setParent(gameObject);
                generatedRoomsCount++;
                loopDetection = 0;
                i = k;
                j = q;
                room.gameObject.name += "[" + room.coords.x + ", " + room.coords.y + "]";
            }
            else
            {
                loopDetection++;
            }

            yield return new WaitForEndOfFrame();
        }

        rooms.Sort((r1, r2) => r1.coords.x.CompareTo(r2.coords.x));
        
        yield return 0;
    }

    bool checkIfRoomCanBeCreated(int k, int q)
    {
        foreach (Room room in rooms)
        {
            if (room.coords.x == k && room.coords.y == q)
            {
                return false;
            }
        }
        return true;
    }
}
