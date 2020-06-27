using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDustFX : MonoBehaviour
{
    public GameObject prefabImpact;
    public GameObject prefabSlide;
    public GameObject prefabJump;

    public void CreateImpactDust()
    {
        // Create dust
        GameObject newDust = Instantiate(prefabImpact);

        // Position dust
        float x = GameManager.player.transform.position.x;
        float y = GameManager.player.transform.position.y - 0.8f;
        float z = GameManager.player.transform.position.z;
        newDust.transform.position = new Vector3(x, y, z);

        // Play particles
        newDust.GetComponentInChildren<ParticleSystem>().Play();
    }

    public void CreateSlideDust()
    {
        // Create dust
        GameObject newDust = Instantiate(prefabSlide);

        // Position dust
        float x = GameManager.player.transform.position.x;
        float y = GameManager.player.transform.position.y - 0.8f;
        float z = GameManager.player.transform.position.z;
        newDust.transform.position = new Vector3(x, y, z);

        // Play particles
        newDust.GetComponentInChildren<ParticleSystem>().Play();
    }

    public void CreateJumpDust()
    {
        // Create dust
        GameObject newDust = Instantiate(prefabJump);

        // Position dust
        float x = GameManager.player.transform.position.x;
        float y = GameManager.player.transform.position.y - 0.8f;
        float z = GameManager.player.transform.position.z;
        newDust.transform.position = new Vector3(x, y, z);

        // Play particles
        newDust.GetComponentInChildren<ParticleSystem>().Play();
    }
}
