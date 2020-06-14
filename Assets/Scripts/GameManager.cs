using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static PlayerData playerData;

    private FileHandling fileHandling;

    public GameManager ()
    {
        Debug.Log("Creating Game Manager..");

        
    }

    private void Awake()
    {
        FileHandling.LoadData();
    }

    void Start()
    {
        playerData = this.GetComponentInChildren<PlayerData>();
    }
}
