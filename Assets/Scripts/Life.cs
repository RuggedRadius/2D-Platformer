using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int maximum;
    public int current;

    private GameManager gm;

    private void Awake()
    {
        GameManager.player = this.gameObject;
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();        
    }

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        if (current <= 0)
            Die();
    }

    public int getCurrent()
    {
        return current;
    }

    public void takeDamage(int amount)
    {
        // Clamp to 0
        amount = Mathf.Clamp(amount, 0, amount);

        // Apply damage
        current -= amount;

        // Clamp current
        current = Mathf.Clamp(current, 0, maximum);
    }

    public void takeHealing(int amount)
    {
        // Clamp to 0
        amount = Mathf.Clamp(amount, 0, amount);

        // Apply damage
        current += amount;

        // Clamp current
        current = Mathf.Clamp(current, 0, maximum);
    }

    public void Die()
    {
        Debug.Log(this.gameObject.name + " died.");

        // Kill enemy
        Destroy(this.gameObject);
    }
}
