using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    #region Singletone
    private static Player instance;
    PlayerController playerController;
    private Player()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }
    #endregion

    public GameObject player; 
    public static Player getInstance()
    {
        if (instance == null)
        {
            instance = new Player();
        }
        return instance;
    }
    public PlayerController GetPlayerController()
    {
        return playerController;
    }
}
