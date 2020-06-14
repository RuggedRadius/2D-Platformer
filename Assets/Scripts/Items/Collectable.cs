using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private GameManager gm;
    public int scoreValue;

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

        // Add to total, accumulating to an extra life at 100

        // SFX
        // ...

        // Destroy sprite
        Destroy(this.gameObject);
    }
}
