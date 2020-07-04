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
    RaycastHit2D hit;
    LayerMask layerMask;
    public bool grounded;

    float rayVerticalOffset = 0.5f;
    float groundedDistance = 0.75f;
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

        sinceGroundedTimer = groundedTime;
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
        origin = new Vector3(this.transform.position.x, this.transform.position.y - rayVerticalOffset, this.transform.position.z);

        // Create layermask
        LayerMask lm = LayerMask.GetMask("Player", "Default");
        int mask = 1 << 8;
        //lm = LayerMask.NameToLayer("Player");

        // Shoot the shot
        hit = Physics2D.Raycast(origin, Vector3.down, groundedDistance, mask);
        Debug.DrawRay(origin, Vector3.down, Color.red, 1f);
        Debug.DrawLine(origin, origin + Vector2.down, Color.blue, 1f);

        // If hit..
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == 8)
            {
                // Walkable
                setGrounded(true);
            }
            // Grounded
            Debug.Log(hit.collider.name + " hit on " + hit.collider.gameObject.layer);
                
            
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
                    // Create dust
                    dust.CreateImpactDust();

                    // Shake screen
                    Camera.main.GetComponent<ScreenShake>().ShakeScreen(0.5f, 2);
                }
            }
        }
    }
}
