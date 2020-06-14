using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            print("Player destroyed");
        }
    }
}
