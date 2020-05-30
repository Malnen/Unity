using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public GameObject gameObject;
    public Room room;

    public Vector3 temporaryPosition;

    public Cell(GameObject parent, Room room)
    {
        gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        gameObject.transform.parent = parent.transform;
        this.room = room;
        gameObject.transform.localPosition =Vector3.one * (Vector2)room.coords;
        gameObject.layer = 8;
        gameObject.name = "Cell[" + room.coords.x + ", " + room.coords.y + "]";
    }


    public void updateCell()
    {
        if (!room.showOnMiniMap && Map.getInstance().mode.Equals("Mini"))
        {
            if (gameObject.transform.localScale.x > 0)
            {
                CoroutineSlave.slave.StartCoroutine(hideCell());
            }
        }
        else if (Map.getInstance().mode.Equals("Hide"))
        {
            if (gameObject.transform.localScale.x > 0)
            {
                gameObject.transform.localScale = Vector3.zero;
            }
        }
        else
        {
            if (room.coords.x == Room.currentRoom.coords.x && room.coords.y == Room.currentRoom.coords.y)
            {
                CoroutineSlave.slave.StartCoroutine(changePosition(new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -0.6f)));
                CoroutineSlave.slave.StartCoroutine(changeColor(((Material)Resources.Load("Materials/RoomCurrent")).color));

                if (gameObject.transform.localScale.x < 1)
                {
                    CoroutineSlave.slave.StartCoroutine(showCell());
                }
            }
            else if (room.defeated)
            {
                CoroutineSlave.slave.StartCoroutine(changePosition(new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -0.3f)));
                CoroutineSlave.slave.StartCoroutine(changeColor(((Material)Resources.Load("Materials/RoomDefeated")).color));

                if (gameObject.transform.localScale.x < 1)
                {
                    CoroutineSlave.slave.StartCoroutine(showCell());
                }
            }
            else if (room.seen)
            {
                CoroutineSlave.slave.StartCoroutine(changePosition(new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0)));
                CoroutineSlave.slave.StartCoroutine(changeColor(((Material)Resources.Load("Materials/RoomSeen")).color));

                if (gameObject.transform.localScale.x < 1)
                {
                    CoroutineSlave.slave.StartCoroutine(showCell());
                }
            }
            else
            {
                CoroutineSlave.slave.StartCoroutine(changePosition(new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0.6f)));
                CoroutineSlave.slave.StartCoroutine(changeColor(((Material)Resources.Load("Materials/RoomNotSeen")).color));
                if (gameObject.transform.localScale.x > 0)
                {
                    CoroutineSlave.slave.StartCoroutine(hideCell());
                }
            }
        }

    }


    public IEnumerator changePosition(Vector3 position)
    {
        float elapsedTime = 0;
        float time = 0.25f;
        Vector3 startPosition = gameObject.transform.localPosition;


        while (elapsedTime < time)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x,gameObject.transform.localPosition.y, Mathf.Lerp(startPosition.z, position.z, (elapsedTime / time)));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, position.z);
        yield return 0;
    }

    IEnumerator hideCell()
    {
        float elapsedTime = 0;
        float time = 0.25f;

        while (elapsedTime < time)
        {
            gameObject.transform.localScale = new Vector3(1,1, Mathf.Lerp(1, 0, (elapsedTime / time)));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        gameObject.transform.localScale = Vector3.zero;
        yield return 0;
    }
    IEnumerator showCell()
    {
        float elapsedTime = 0;
        float time = 0.25f;

        while (elapsedTime < time)
        {
            gameObject.transform.localScale = new Vector3(1, 1, Mathf.Lerp(0, 1, (elapsedTime / time)));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        gameObject.transform.localScale = Vector3.one;
        yield return 0;
    }
    IEnumerator changeColor(Color color)
    {
        float elapsedTime = 0;
        float time = 0.25f;
        Color startColor = gameObject.GetComponent<MeshRenderer>().material.color;

        while(elapsedTime < time)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(startColor, color, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        gameObject.GetComponent<MeshRenderer>().material.color = color;

        yield return 0;
        
    }
}
