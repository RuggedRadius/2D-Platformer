using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [HideInInspector] public Life pLife;

    public PlayerTorch torch;


    void Update()
    {
        testLifeBar();
        testBattery();
    }

    private void testLifeBar()
    {
        try
        {
            if (pLife == null && GameManager.player != null)
            {
                pLife = GameManager.player.GetComponent<Life>();
            }

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                pLife.takeHealing(5);
            }
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                pLife.takeDamage(5);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
    void testBattery()
    {
        if (torch == null && GameManager.player != null)
        {
            torch = GameManager.player.GetComponent<PlayerTorch>();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            torch.ToggleLight();
        }
    }
}
