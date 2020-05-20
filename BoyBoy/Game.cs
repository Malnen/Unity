using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    #region Singletone
    public static Game instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject game;


    private void Start()
    {
        StartCoroutine(Maze.getInstance().generateMaze());
    }
    private void Update()
    {

    }
}
