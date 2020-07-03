using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public SpriteRenderer playerSprite;
    private Animator anim;
    private PlayerDustFX dust;

    [Header("Horizontal Movement")]
    public float fHorizontalVelocity;
    public float horizontalSlideSpeed = 5;
    [Range(0f, 50f)] public float horizontalPower;
    [Range(0f, 100f)] public float horizontalSpeedMax;    
    [Range(0f, 1f)] public float fHorizontalDampingWhenStopping = 0.5f;
    [Range(0f, 1f)] public float fHorizontalDampingWhenTurning = 0.5f;
    [Range(0f, 1f)] public float fHorizontalDampingBasic = 0.5f;

    float slideCreatedTimer = 0f;
    float slideTimerReset = 2f;

    // States
    public bool idle;
    public bool idleTimerRunning;
    public float idleTimer;
    public int idleTicker = 0;


    // Timers
    public float lastJumpTimer = 0f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        playerSprite = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        dust = this.GetComponent<PlayerDustFX>();

        // Init animator
        anim.SetBool("alive", true);

        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }


    void Update()
    {
        slideCreatedTimer += Time.deltaTime;

        // Determine horizontal movement
        fHorizontalVelocity = rb.velocity.x;
        fHorizontalVelocity += Input.GetAxisRaw("Horizontal");
        anim.SetFloat("movement", fHorizontalVelocity);

        // Check for idle
        if (!idleTimerRunning)
        {
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                idleTicker++;
                if (idleTicker > 20)
                {
                    idleTicker = 0;
                    StartCoroutine(idleTimerStart());                    
                }
            }
        }        

        // Moving
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f) 
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.deltaTime * horizontalPower);

            if (slideCreatedTimer > slideTimerReset)
            {
                // Slide
                if (fHorizontalVelocity > horizontalSlideSpeed || fHorizontalVelocity < -horizontalSlideSpeed)
                {
                    // Reset timer
                    slideCreatedTimer = 0f;

                    // Create dust
                    dust.CreateSlideDust();
                }
            }
        } 
        // Turning around
        else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizontalVelocity))
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.deltaTime * 10f);
        }
        // Idle
        else
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingBasic, Time.deltaTime * 10f);
        }

        fHorizontalVelocity = Mathf.Clamp(fHorizontalVelocity, -horizontalSpeedMax, horizontalSpeedMax); // Clamp speed

        rb.velocity = new Vector2(fHorizontalVelocity, rb.velocity.y);


        // Flip sprite if necessary
        HandleSpriteDirection();
    }

    private IEnumerator idleTimerStart ()
    {
        idleTimerRunning = true;
        idle = true;
        idleTimer = 0f;
        
        anim.SetBool("idle", true);

        while (Input.GetAxisRaw("Horizontal") == 0)
        {
            idleTimer += Time.deltaTime;
            yield return null;
        }

        idle = false;
        idleTimerRunning = false;        
        anim.SetBool("idle", false);

        yield return null;
    }

    private void HandleSpriteDirection()
    {

        if (!Mathf.Approximately(0, rb.velocity.x))
        {
            if (rb.velocity.x > 0)
            {
                playerSprite.flipX = false;
            }
            else
            {
                playerSprite.flipX = true;
            }
        }
    }


}
