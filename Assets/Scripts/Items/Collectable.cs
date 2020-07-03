using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private GameManager gm;
    public int scoreValue;
    public int batteryLifeIncrease;
    public int lifeIncrease;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Collect();
        }
    }

    public void Collect()
    {
        // Increment player score
        GameManager.gameData.score += Mathf.Abs(scoreValue);

        GameManager.player.GetComponent<PlayerTorch>().AddBattery(batteryLifeIncrease);
        GameManager.player.GetComponent<Life>().current += lifeIncrease;



        // SFX
        // ...

        // Destroy sprite
        Destroy(this.gameObject);
    }
}
