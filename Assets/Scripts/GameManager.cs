using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameData gameData;
    public static GameObject player;    

    private void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player");

        //Save.InitialiseSaveFile();

        Load.LoadFile();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Save.SaveFile(gameData);
            Debug.Log("Quick saved.");
        }
    }
}
