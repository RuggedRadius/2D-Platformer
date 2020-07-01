using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public SpriteRenderer playerSprite;
    private Animator anim;
    private PlayerGrounded pGrounded;
    private PlayerDustFX dust;
    private PlayerLighting lighting;

    [Header("Jump")]
    [Range(1f, 1000f)] public float jumpPower;
    private float jumpTimer = 0;
    private float JumpTime = 0.75f;

    [Header("Horizontal Movement")]
    public float fHorizontalVelocity;
    public float horizontalSlideSpeed = 5;
    [Range(0f, 1000f)] public float horizontalPower;
    [Range(0f, 100f)] public float horizontalSpeedMax;    
    [Range(0f, 1f)] public float fHorizontalDampingWhenStopping = 0.5f;
    [Range(0f, 1f)] public float fHorizontalDampingWhenTurning = 0.5f;
    [Range(0f, 1f)] public float fHorizontalDampingBasic = 0.5f;

    float slideCreatedTimer = 0f;
    float slideTimerReset = 2f;

    // States
    public bool grounded;
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
        pGrounded = this.GetComponent<PlayerGrounded>();
        dust = this.GetComponent<PlayerDustFX>();
        lighting = this.GetComponent<PlayerLighting>();

        // Init animator
        anim.SetBool("alive", true);

        this.transform.position = new Vector3(transform.position.x, transform.position.y, -12);
    }


    void Update()
    {
        // Update grounded status
        grounded = pGrounded.grounded;

        // Decrement jump timer
        jumpTimer -= Time.deltaTime;
        slideCreatedTimer += Time.deltaTime;
        
        // Determine horizontal movement
        fHorizontalVelocity = rb.velocity.x;
        fHorizontalVelocity += Input.GetAxisRaw("Horizontal");
        anim.SetFloat("movement", Input.GetAxisRaw("Horizontal"));

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

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f) 
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.deltaTime * 10f);

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
        else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizontalVelocity))
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.deltaTime * 10f);
        }
        else
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingBasic, Time.deltaTime * 10f);
        }
        fHorizontalVelocity = Mathf.Clamp(fHorizontalVelocity, -horizontalSpeedMax, horizontalSpeedMax); // Clamp speed
        rb.velocity = new Vector2(fHorizontalVelocity, rb.velocity.y);



        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Check for grounded
            if (grounded)
            {
                Jump();
                jumpTimer = JumpTime;
            }
            else
            {
                print("Not grounded, cant jump");
            }
        }
        // Extra jump
        if (Input.GetKey(KeyCode.Space))
        {
            if (jumpTimer > 0)
            {
                Debug.Log("extra jumping");
                rb.AddForce(new Vector2(0, jumpPower * (jumpTimer/JumpTime) * 0.05f), ForceMode2D.Impulse);
            }
        }

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

    private void Jump()
    {
        dust.CreateJumpDust();
        rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        anim.SetTrigger("jump");
    }
}
