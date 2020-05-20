using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room 
{
    public Vector2Int coords;
    GameObject floor;
    public Room()
    {
        gameObject = new GameObject();
        gameObject.name = "Room";
    }

    public GameObject gameObject;

    public void setParent(GameObject parent)
    {
        gameObject.transform.parent = parent.transform;
    }

    void moveObjectToPosition()
    {
        if(floor == null)
        {
            floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.transform.parent = gameObject.transform;
            floor.name = "Floor";
            floor.transform.localPosition = Vector3.zero;
        }
        floor.transform.localPosition = new Vector3(coords.x, 0, coords.y);
    }

    public void setCoords(int i, int j)
    {
        coords = new Vector2Int(i, j);
        moveObjectToPosition();
    }
}
