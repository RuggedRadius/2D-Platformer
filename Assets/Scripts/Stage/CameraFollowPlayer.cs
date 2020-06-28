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

            if (!IsPlayerVisible())
            {
                // Player is not visible on main camera
                Debug.Log("PLAYER NOT VISIBLE");
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
    }

    bool IsPlayerVisible()
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        var point = GameManager.player.transform.position;
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
                return false;
        }
        return true;
    }

    private void MoveUpLevel()
    {
        Camera.main.transform.position = new Vector3(
            Camera.main.transform.position.x,
            Camera.main.transform.position.y + Screen.height,
            Camera.main.transform.position.z
            );
    }

    private void MoveDownLevel()
    {
        Camera.main.transform.position = new Vector3(
            Camera.main.transform.position.x,
            Camera.main.transform.position.y - Screen.height,
            Camera.main.transform.position.z
            );
    }


}
