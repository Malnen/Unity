using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field 
{
    public PlaceableItem item;
    public Vector2 coords;
    public GameObject field;
    public Field(GameObject parent, Vector2 coords)
    {
        field = new GameObject();
        field.transform.parent = parent.transform;
        field.name = "Field" + "[" + coords.x + ", " + coords.y + "]";
    }
}
