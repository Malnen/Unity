using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomLayout 
{
    public GameObject layout;
    protected Field[,] fields = new Field[11, 11];
    public RoomLayout(GameObject parent)
    {
        layout = new GameObject();
        layout.name = "Layout";
        layout.transform.parent = parent.transform;
        layout.transform.localPosition = new Vector3(0, 0.5f, 0);
        layout.transform.localScale = Vector3.one;
        calculateFields();
        chooseLayout();
    }
    protected virtual void chooseLayout()
    {

    }
    protected void calculateFields()
    {
        for(int i = 0; i < fields.GetLength(0); i++)
        {
            for (int j = 0; j < fields.GetLength(1); j++)
            {
                fields[i, j] = new Field(layout, new Vector2(i, j));
                fields[i, j].field.transform.localPosition = new Vector3(-0.5f + (i + 1) * 1f / 12f, 0, -0.5f + (j + 1) * 1f / 12f);             
            }
        }
    }
}
