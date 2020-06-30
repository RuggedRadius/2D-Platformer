using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDustFX : MonoBehaviour
{
    public GameObject prefabImpact;
    public GameObject prefabSlide;
    public GameObject prefabJump;

    public float verticalOffset;


    public void CreateImpactDust()
    {
        Vector3 dustPos = Vector3.up * verticalOffset;
        Vector3 finalPos = GameManager.player.transform.position - dustPos;
        Instantiate(prefabImpact, finalPos, Quaternion.Euler(Vector3.zero));
    }

    public void CreateSlideDust()
    {
        ParticleSystem ps = GameManager.player.transform.Find("dustSlide").GetComponent<ParticleSystem>();
        System.Threading.Tasks.Task.Run(() => {
            ps.Play();
            new WaitForSeconds(0.5f);
            //System.Threading.Thread.Sleep(500);
            ps.Stop();
        });
        
    }

    public void CreateJumpDust()
    {
        Vector3 dustPos = Vector3.up * verticalOffset;
        Vector3 finalPos = GameManager.player.transform.position - dustPos;
        Instantiate(prefabJump, finalPos, Quaternion.Euler(Vector3.zero));
    }
}
