using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMap : MonoBehaviour
{

    private GameObject map;
    private Transform mapParent;
    private Transform worldParent;
    private Transform player;

    private bool mapShown;
    private bool mapInPossession;

    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map");
        mapParent = GameObject.FindGameObjectWithTag("Screen").transform;
        worldParent = GameObject.FindGameObjectWithTag("WorldCanavs").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        mapInPossession = true;
        ToggleImage(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Player input
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mapShown)
            {
                if (GetDistanceFromPlayer() < 100f)
                {
                    PickUpMap();
                }
                else
                {
                    Debug.Log("LEFT MAP BEHIND");
                }
            }
            else
            {
                if (mapInPossession)
                {
                    PutDownMap();
                    
                }
                else
                {
                    Debug.Log("LEFT MAP BEHIND");
                }
            }
        }

        //Debug.Log(GetDistanceFromPlayer());

        // Show or Hide the map
        //ToggleMap(mapShown);
    }

    private float GetDistanceFromPlayer()
    {
        //return Mathf.Abs(player.position.x - (map.GetComponentInChildren<RectTransform>().rect.position.x));
        //return Vector3.Distance(player.position, map.transform.position);
        return 10;
    }

    private void PutDownMap()
    {
        ToggleImage(true);
        map.transform.SetParent(worldParent);
        mapShown = true;
    }

    private void ToggleImage(bool value)
    {
        foreach (Image img in map.GetComponentsInChildren<Image>())
        {
            img.enabled = value;
        }
        //map.GetComponentInChildren<Image>().enabled = value;
        //map.GetComponentInChildren<Image>().GetComponentInChildren<Image>().enabled = value;
    }

    private void PickUpMap()
    {
        // Change to local parent
        map.transform.SetParent(mapParent);

        // Reset position
        Vector2 originalPosition = new Vector2(0, 0);
        map.GetComponentInChildren<RectTransform>().anchoredPosition = originalPosition;

        // Hide image
        ToggleImage(false);

        // Set to hidden
        mapShown = false;
    }
}
