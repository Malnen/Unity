using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Wall
{
    public Door(GameObject parent,string type) : base(parent, type)
    {

    }
    protected override void createGameObject()
    {
        gameObject = MonoBehaviour.Instantiate((GameObject)Resources.Load("BlenderObjects/PrefabDoor0"));
        gameObject.name = type + " Door";
    }
}
