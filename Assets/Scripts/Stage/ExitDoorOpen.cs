using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorOpen : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;

    public bool doorsOpen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            doorsOpen = true;
            OpenDoors();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            doorsOpen = false;
            CloseDoors();
        }
    }

    public void OpenDoors()
    {
        leftDoor.transform.localPosition = new Vector3(-1f, 0, 0);
        rightDoor.transform.localPosition = new Vector3(1f, 0, 0);
    }

    public void CloseDoors()
    {
        leftDoor.transform.localPosition = Vector3.zero;
        rightDoor.transform.localPosition = Vector3.zero;
    }
}
