using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall
{
    protected string type;
    protected GameObject gameObject;
 
    public Wall(GameObject parent, string type)
    {
        this.type = type;
        createGameObject();
        gameObject.transform.parent = parent.transform;
        positionWall();
        gameObject.isStatic = true;
    }
    protected virtual void createGameObject()
    {
        gameObject = MonoBehaviour.Instantiate((GameObject)Resources.Load("BlenderObjects/PrefabWall0"));
        gameObject.name = type + " Wall";
    }
    public void setScaleOne()
    {
        gameObject.transform.localScale = Vector3.one;
    }
    void positionWall()
    {
        if (type.Equals("North"))
        {
            gameObject.transform.localPosition = new Vector3(0.5f, 1, 0);
        }
        if (type.Equals("South"))
        {
            gameObject.transform.localPosition = new Vector3(-0.5f, 1, 0);
        }
        if (type.Equals("East"))
        {
            gameObject.transform.localPosition = new Vector3(0, 1, 0.5f);
            gameObject.transform.localRotation = Quaternion.Euler(-90, 90, 0);
        }
        if (type.Equals("West"))
        {
            gameObject.transform.localPosition = new Vector3(0, 1, -0.5f);
            gameObject.transform.localRotation = Quaternion.Euler(-90, 90, 0);
        }
    }
}
