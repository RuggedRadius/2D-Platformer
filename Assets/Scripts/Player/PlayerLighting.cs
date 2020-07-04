using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerLighting : MonoBehaviour
{
    private PlayerMovement pMovement;
    private PlayerGrounded pGrounded;
    private PlayerTorch torch;
    private Light2D visionLight;
    

    float sideInnerAngle = 60f;
    float sideOuterAngle = 135f;
    float frontInnerAngle = 360f;
    float frontOuterAngle = 360f;

    private Animator anim;
    AnimatorStateInfo currentAnimInfo;
    bool jumping;

    void Start()
    {
        pMovement = this.GetComponent<PlayerMovement>();
        pGrounded = this.GetComponent<PlayerGrounded>();
        torch = this.GetComponent<PlayerTorch>();
        visionLight = GameManager.player.transform.Find("Vision Light").GetComponent<Light2D>();
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        currentAnimInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (torch.on)
        {
            // Torch is on
            visionLight.enabled = true;

            if (currentAnimInfo.IsName("Front Idle"))
            {
                // Front facing
                visionLight.pointLightInnerAngle = frontInnerAngle;
                visionLight.pointLightOuterAngle = frontOuterAngle;
                //visionLight.pointLightOuterRadius = 4;
            }
            //else if (currentAnimInfo.IsName("Side Jump") ||
            //        currentAnimInfo.IsName("Front Jump"))
            //{
            //    // Jumping
            //    visionLight.pointLightInnerAngle = frontInnerAngle;
            //    visionLight.pointLightOuterAngle = frontOuterAngle;
            //    //visionLight.pointLightOuterRadius = 4;
            //}
            else
            {
                // Side facing
                //visionLight.pointLightOuterRadius = 8;
                visionLight.pointLightInnerAngle = sideInnerAngle;
                visionLight.pointLightOuterAngle = sideOuterAngle;

                if (pMovement.playerSprite.flipX)
                {
                    visionLight.transform.eulerAngles = Vector3.forward * 90;
                }
                else
                {
                    visionLight.transform.eulerAngles = Vector3.back * 90;
                }
            }
        }
        else
        {
            // Torch is off
            visionLight.enabled = false;
        }
    }
}
