using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button
{
    public GameObject gameObject;
    ButtonComponent buttonComponent;

    public Button(GameObject gameObejcet,GameObject parent, Action action,string name, Vector3 position)
    {
        gameObject = gameObejcet;
        gameObject.AddComponent<ButtonComponent>();
        buttonComponent = gameObject.GetComponent<ButtonComponent>();
        buttonComponent.action = action;
        gameObject.transform.parent = parent.transform;
        gameObject.name = name;
        gameObject.transform.localPosition = position;
    }
    
    public void setScale(Vector3 scale)
    {
        gameObject.transform.localScale = scale;
    }
}
