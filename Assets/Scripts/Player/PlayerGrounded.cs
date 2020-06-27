using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{
    private PlayerDustFX dust;
    PlayerMovement pMovement;
    Animator anim;

    Ray ray;
    Vector2 origin;
    Vector2 direction;
    RaycastHit2D hit;
    LayerMask layerMask;

    public bool grounded;
    float groundedDistance = 0.9f;
    float sinceGroundedTimer = 0f;
    float groundedTime = 2f;

    float airBorneTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        pMovement = this.GetComponent<PlayerMovement>();
        anim = this.GetComponent<Animator>();
        dust = this.GetComponent<PlayerDustFX>();

        layerMask = LayerMask.GetMask("Walkable");
    }

    // Update is called once per frame
    void Update()
    {
        if (!grounded)
        {
            // Update airborne timer
            airBorneTimer += Time.deltaTime;
        }

        // Update timer
        sinceGroundedTimer += Time.deltaTime;


        // Set position and direction of shot
        origin = this.transform.position;
        direction = Vector3.down;

        // Shoot the shot
        hit = Physics2D.Raycast(origin, direction, groundedDistance, layerMask);
        Debug.DrawRay(origin, direction, Color.red, 1f);

        // If hit..
        if (hit.collider != null)
        {
            // Grounded
            setGrounded(true);
        }
        else if (sinceGroundedTimer < groundedTime)
        {
            // Still "Grounded" for responsiveness
            setGrounded(true);
        }
        else
        {
            // Not grounded
            setGrounded(false);
        }
    }

    /// <summary>
    /// Sets grounded status and updates animator values.
    /// </summary>
    /// <param name="value"></param>
    private void setGrounded(bool value)
    {
        if (grounded == value)
        {
            return;
        }
        else
        {
            // Update values
            grounded = value;
            anim.SetBool("grounded", value);

            // If leaving grounding
            if (value == false)
            {
                // Clear airborne timer
                airBorneTimer = 0;

                // Reset grounded timer
                sinceGroundedTimer = groundedTime;
            }
            else // Re-grounding
            {
                if (airBorneTimer > 2f)
                {
                    dust.CreateImpactDust();
                }
            }
        }
    }
}
