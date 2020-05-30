using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : PlaceableItem
{
    public GameObject gameObject;
    private const int rockCount = 10;
    public Rock(GameObject parent)
    {
        gameObject = chooseRock();
        gameObject.transform.parent = parent.transform;
        gameObject.transform.localPosition = new Vector3(0, 5f - (20f / 11f), 0);
        gameObject.transform.localScale = Vector3.one * (10f - 30f / 11f);
        gameObject.isStatic = true;
    }

    GameObject chooseRock()
    {
        GameObject rock;
        int random = Random.Range(0, rockCount);
        rock = MonoBehaviour.Instantiate((GameObject)Resources.Load("BlenderObjects/PrefabRock" + random));
        return rock;
    }
}
