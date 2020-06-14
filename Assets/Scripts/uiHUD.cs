using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiHUD : MonoBehaviour
{
    public Text scoreValue;

    private GameManager gm;
    private PlayerData playerData;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        playerData = gm.GetComponentInChildren<PlayerData>();
    }

    void Update()
    {
        scoreValue.text = playerData.score.ToString();
    }
}
