using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBar : MonoBehaviour
{
    private RectTransform bar;

    public Life pLife;
    public PlayerTorch torch;




    void Start()
    {
        bar = transform.Find("Fill").GetComponent<RectTransform>();
    }

    void Update()
    {
        if (GameManager.player != null && torch != null)
        {
            float adjValue = ((float)torch.batteryLifeCurrent / (float)torch.batteryLifeMax) * torch.batteryMaxHeight;
            bar.GetComponent<RectTransform>().sizeDelta = new Vector2(0.86f, adjValue);
        }
    }   
}
