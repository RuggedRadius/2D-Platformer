using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public float cameraDepth = -10f;

    [Header("Movement Settings")]
    public float offset;
    public float offsetSmoothing;

    [Header("Dead Zone Settings")]
    public float dzHorizontalMargin;
    public float dzVerticalMargin;


    private void Start()
    {
        transform.position = GameObject.FindGameObjectWithTag("StartLocation").transform.position;
    }

    void Update()
    {
        try
        {
            Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(GameManager.player.transform.position);


            if (playerScreenPosition != Vector3.zero)
            {
                if (playerScreenPosition.x < dzHorizontalMargin ||
                    playerScreenPosition.x > Screen.width - dzHorizontalMargin ||
                    playerScreenPosition.y < dzVerticalMargin ||
                    playerScreenPosition.y > Screen.height - dzVerticalMargin)
                {
                    Vector3 playerNewCamPosition = new Vector3(
                        GameManager.player.transform.position.x,
                        GameManager.player.transform.position.y,
                        cameraDepth
                        );
                    transform.position = Vector3.Lerp(transform.position, playerNewCamPosition, offsetSmoothing * Time.deltaTime);
                }
            }



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
