using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTorch : MonoBehaviour
{
    private BatteryBar bar;

    public int batteryLifeMax = 100;
    public int batteryLifeCurrent = 100;

    public float batteryMaxHeight = 1.8f;
    public float batteryWidth = 0.86f;

    public float powerConsumptionDelay;

    public bool on;

    void Start()
    {
        bar = GameObject.FindGameObjectWithTag("BatteryBar").GetComponent<BatteryBar>();
        bar.torch = this;
        bar.pLife = this.GetComponent<Life>();

        SetLight(false);
    }

    private void Update()
    {
        Mathf.Clamp(batteryLifeCurrent, 0, batteryLifeMax);
    }

    public void AddBattery(int amount)
    {
        if ((batteryLifeCurrent + amount) > batteryLifeMax)
        {
            batteryLifeCurrent = batteryLifeMax;
        }
        else
        {
            batteryLifeCurrent += amount;
        }
    }

    public void SetLight(bool value)
    {
        on = value;

        if (on)
        {
            StartCoroutine(ConsumePower());
        }
    }

    public void ToggleLight()
    {
        on = !on;

        if (on)
        {
            StartCoroutine(ConsumePower());
        }
    }

    private IEnumerator ConsumePower()
    {
        while (on)
        {
            batteryLifeCurrent--;

            if (batteryLifeCurrent <= 0)
            {
                on = false;
            }

            yield return new WaitForSeconds(powerConsumptionDelay);
        }
    }
}
