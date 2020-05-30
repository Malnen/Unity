using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Game : MonoBehaviour
{
    #region Singletone
    public static Game instance;
    public GameObject camera;
    public Text fps;
    public GameObject game;
    private void Awake()
    {
        instance = this;
    }

    #endregion

    private void Start()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(1280, 720, true);
        StartCoroutine(Maze.getInstance().generateMaze());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Maze.getInstance().generateMaze());
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Map.getInstance().toggleMap();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Map.getInstance().show();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Map.getInstance().hide();
        }
        fps.text = ((int)(1f / Time.deltaTime)).ToString();
    }
}
