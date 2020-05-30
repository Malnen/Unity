using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : ShowHide
{
    #region Singleton
    private static Map instance;
    static List<Cell> cells = new List<Cell>();
    public List<Cell> seenCells = new List<Cell>();
    bool changing = false;
    public string mode = "Full";

    GameObject mapCamera;
    private Map()
    {
        map = new GameObject();
        map.name = "Map";
        map.layer = 8;
        map.transform.position = new Vector3(8, 4, 0);

        container = new GameObject();
        container.name = "Container";
        container.transform.parent = map.transform;
        container.transform.localPosition = new Vector3(0, 10, 0); ;

        backGround = MonoBehaviour.Instantiate((GameObject)Resources.Load("BackGround"));
        backGround.transform.parent = container.transform;
        backGround.name = "BackGround";
        backGround.layer = 8;
        backGround.transform.localPosition = Vector3.zero;

        cellsGameObject = new GameObject();
        cellsGameObject.transform.parent = backGround.transform;
        cellsGameObject.name = "Cells";
        cellsGameObject.layer = 8;
        cellsGameObject.transform.localPosition = Vector3.zero;

        cellContainer = new GameObject();
        cellContainer.transform.parent = cellsGameObject.transform;
        cellContainer.name = "Cell Container";
        cellContainer.layer = 8;
        cellContainer.transform.localPosition = Vector3.zero;

        mapCamera = GameObject.Find("Game/Map Camera");
        mapCamera.SetActive(false);

        button = new Button(MonoBehaviour.Instantiate((GameObject)Resources.Load("BlenderObjects/PrefabToggleMapButton")), map, toggleMap, "Toggle Button", new Vector3(-1, 10, 0));
        button.setScale(Vector3.one * 1.5f);
    }

    #endregion  

    public GameObject backGround;
    public GameObject map;
    public GameObject cellsGameObject;
    public GameObject cellContainer;
    GameObject container;
    Button button;
    public static Map getInstance()
    {
        if (instance == null)
        {
            instance = new Map();
        }
        return instance;
    }

    public void fillMap()
    {
        foreach (KeyValuePair<Vector2, Room> room in Maze.getInstance().getRooms())
        {
            Cell cell = new Cell(cellContainer, room.Value);
            cells.Add(cell);
            cell.updateCell();
            room.Value.mapCell = cell;
        }
        map.transform.localScale = Vector3.one;
    }

    (int size, Vector3 targetPosition) calculateSize()
    {
        int x;
        int y;
        int size = 1;
        Vector3 targetPosition = new Vector3();

        if (mode.Equals("Full"))
        {
            try
            {
                x = (Maze.getInstance().minMaxSeenRooms[0].coords.x + Maze.getInstance().minMaxSeenRooms[1].coords.x) / 2;
                y = (Maze.getInstance().minMaxSeenRooms[2].coords.y + Maze.getInstance().minMaxSeenRooms[3].coords.y) / 2;

                if (Maze.getInstance().minMaxSeenRooms[0].coords.x - Maze.getInstance().minMaxSeenRooms[1].coords.x > Maze.getInstance().minMaxSeenRooms[2].coords.y - Maze.getInstance().minMaxSeenRooms[3].coords.y)
                {
                    size = Maze.getInstance().minMaxSeenRooms[0].coords.x - Maze.getInstance().minMaxSeenRooms[1].coords.x + 3;
                }
                else
                {
                    size = Maze.getInstance().minMaxSeenRooms[2].coords.y - Maze.getInstance().minMaxSeenRooms[3].coords.y + 3;
                }
            }
            catch (NullReferenceException e)
            {
                x = 0;
                y = 0;
                size = 3;
            }
            targetPosition = -new Vector3(x, y, 0);
        }
        else if (mode.Equals("Mini"))
        {
            size = 6;
            targetPosition = -(Vector3)(Vector2)Room.currentRoom.coords;
        }

        return (size, targetPosition);
    }

    public IEnumerator spawnButton()
    {
        float time = 0.1f;
        float elapsedTime = 0;

        Vector3 startPosition = button.gameObject.transform.localPosition;
        Vector3 targetPosition = new Vector3(1, -2, 0); 
        
        while (time > elapsedTime)
        {
            button.gameObject.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        button.gameObject.transform.localPosition = targetPosition;
        yield return 0;
    }

    IEnumerator updateButton()
    {
        float time = 0.1f;
        float elapsedTime = 0;
        Vector3 startPosition = button.gameObject.transform.localPosition;
        if (mode.Equals("Hide"))
        {
            Vector3 targetPosition = new Vector3(1, 1, 0); 

            while (time > elapsedTime)
            {
                button.gameObject.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            button.gameObject.transform.localPosition = targetPosition;
        }
        else if (mode.Equals("Full"))
        {
            Vector3 targetPosition = new Vector3(1, -2, 0);

            while (time > elapsedTime)
            {
                button.gameObject.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            button.gameObject.transform.localPosition = targetPosition;
        }

        yield return 0;
    }
    public IEnumerator updateMap()
    {
        (int size, Vector3 targetPosition) = calculateSize();

        if (mode.Equals("Mini"))
        {
            Room.currentRoom.mapCell.updateCell();
            foreach (Cell cell in seenCells)
            {
                if (!cell.room.showOnMiniMap)
                {
                    cell.updateCell();
                }
            }
            yield return new WaitForSeconds(0.1f);
            yield return CoroutineSlave.slave.StartCoroutine(transformMap(size, targetPosition));
            foreach (Cell cell in seenCells)
            {
                if (cell.room.showOnMiniMap)
                {
                    cell.updateCell();
                }
            }
        }
        else
        {
            foreach (Cell cell in seenCells)
            {
                cell.updateCell();
            }
            yield return CoroutineSlave.slave.StartCoroutine(transformMap(size, targetPosition));
        }
        yield return 0;
    }

    public IEnumerator mapToggler()
    {
        changing = true;
        (int size, Vector3 targetPosition) = calculateSize();
        if (mode.Equals("Hide"))
        {
            hide();
            yield return new WaitForSeconds(0.2f);
            yield return CoroutineSlave.slave.StartCoroutine(updateButton());
        }
        else if (mode.Equals("Full"))
        {
            show();
            transformMapQucik(size, targetPosition);
            yield return CoroutineSlave.slave.StartCoroutine(updateButton());
            yield return CoroutineSlave.slave.StartCoroutine(updateMap());
        }
        else if (mode.Equals("Mini"))
        {
            yield return CoroutineSlave.slave.StartCoroutine(updateMap());
        }
        changing = false;
        yield return 0;
    }

    public void toggleMap()
    {
        if (!changing)
        {
            if (mode.Equals("Full"))
            {
                mode = "Mini";
            }
            else if (mode.Equals("Mini"))
            {
                mode = "Hide";
            }
            else
            {
                mode = "Full";
            }
            CoroutineSlave.slave.StartCoroutine(mapToggler());
        }
    }
    void transformMapQucik(int size, Vector3 targetPosition)
    {
        Vector3 startPosition = cellContainer.transform.localPosition;
        Vector3 startScale = cellsGameObject.transform.localScale;

        cellContainer.transform.localPosition = targetPosition;
        cellsGameObject.transform.localScale = new Vector3(1f / size, 1f / size, 2f);
    }
    IEnumerator transformMap(int size, Vector3 targetPosition)
    {
        float elapsedTime = 0;
        float time = 0.25f;

        Vector3 startPosition = cellContainer.transform.localPosition;
        Vector3 startScale = cellsGameObject.transform.localScale;
        while (elapsedTime < time)
        {
            cellContainer.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / time));
            cellsGameObject.transform.localScale = Vector3.Lerp(startScale, new Vector3(1f / size, 1f / size, 2f), (elapsedTime / time));

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        cellContainer.transform.localPosition = targetPosition;
        cellsGameObject.transform.localScale = new Vector3(1f / size, 1f / size, 2f);

        yield return 0;
    }
    IEnumerator transformMap(Vector3 targetPosition)
    {
        float elapsedTime = 0;
        float time = 0.25f;

        Vector3 startPosition = cellContainer.transform.localPosition;
        while (elapsedTime < time)
        {
            cellContainer.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / time));

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        cellContainer.transform.localPosition = targetPosition;

        yield return 0;
    }
    IEnumerator transformMap(int size)
    {
        float elapsedTime = 0;
        float time = 0.25f;

        Vector3 startScale = cellsGameObject.transform.localScale;
        while (elapsedTime < time)
        {
            cellsGameObject.transform.localScale = Vector3.Lerp(startScale, new Vector3(1f / size, 1f / size, 2f), (elapsedTime / time));

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        cellsGameObject.transform.localScale = new Vector3(1f / size, 1f / size, 2f);

        yield return 0;
    }


    public void addSeenCell(Cell cell)
    {
        if (!seenCells.Contains(cell))
        {
            seenCells.Add(cell);
        }
    }

    public void show()
    {
        if (!mapCamera.activeInHierarchy)
        {
            mapCamera.SetActive(true);
        }
        CoroutineSlave.slave.StartCoroutine(ShowHide.show(container, new Vector3(0, 0, 0)));
    }

    public void hide()
    {
        CoroutineSlave.slave.StartCoroutine(ShowHide.hide(container));
    }
}
