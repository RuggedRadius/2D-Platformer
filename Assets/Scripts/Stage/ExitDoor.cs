using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.layer == 9)
        {
            Debug.Log("Level Complete");

            // ...
        }
    }
}
