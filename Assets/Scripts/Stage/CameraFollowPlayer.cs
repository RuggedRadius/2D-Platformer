using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    void Update()
    {
        try
        {
            this.transform.position = new Vector3(
            GameManager.player.transform.position.x,
            this.transform.position.y,
            this.transform.position.z
            );
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
    }
}
