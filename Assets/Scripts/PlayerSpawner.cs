﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public bool spawnerOn;
    private GameObject player;
    public GameObject playerPrefab;
    

    void Update()
    {
        if (player == null && spawnerOn)
        {
            SpawnPlayerAtStart();

            //// Doesnt work..
            //if (GameManager.gameData != null)
            //{
            //    Load.LoadFile();
            //}
            //else
            //{
            //    SpawnPlayerAtStart();

            //}
        }
    }

    public void SpawnPlayerAtStart()
    {
        Debug.Log("Spawning player...");
        GameObject newPlayer = Instantiate(playerPrefab, null);
        newPlayer.transform.position = GameObject.FindGameObjectWithTag("StartLocation").transform.position;
        player = newPlayer;
    }
}
