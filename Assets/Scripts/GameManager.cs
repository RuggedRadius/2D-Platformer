using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerData playerData;

    void Start()
    {
        playerData = this.GetComponentInChildren<PlayerData>();
    }
}
