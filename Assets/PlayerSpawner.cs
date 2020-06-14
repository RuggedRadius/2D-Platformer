using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    GameObject player;
    public GameObject playerPrefab;


    void Update()
    {
        if (player == null)
        {
            Debug.Log("Spawning player...");
            GameObject newPlayer = Instantiate(playerPrefab, null);
            newPlayer.transform.position = GameObject.FindGameObjectWithTag("StartLocation").transform.position;

            player = newPlayer;
        }
    }
}
