using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    void Update()
    {
        transform.position = Player.getInstance().player.transform.position;
        transform.position += new Vector3(0, 40, -20);
    }
}
