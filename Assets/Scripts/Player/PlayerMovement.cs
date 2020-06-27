using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer playerSprite;
    private Animator anim;
    private PlayerGrounded pGrounded;
    private PlayerDustFX dust;

    [Header("Jump")]
    [Range(1f, 1000f)] public float jumpPower;
    private float jumpTimer = 0;
    private float JumpTime = 1.5f;

    [Header("Horizontal Movement")]
    public float fHorizontalVelocity;
    [Range(0f, 1000f)] public float horizontalPower;
    [Range(0f, 100f)] public float horizontalSpeedMax;    
    [Range(0f, 1f)] public float fHorizontalDampingWhenStopping = 0.5f;
    [Range(0f, 1f)] public float fHorizontalDampingWhenTurning = 0.5f;
    [Range(0f, 1f)] public float fHorizontalDampingBasic = 0.5f;

    // States
    public bool grounded;

    // Timers
    public float lastJumpTimer = 0f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        playerSprite = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        pGrounded = this.GetComponent<PlayerGrounded>();
        dust = this.GetComponent<PlayerDustFX>();

        // Init animator
        anim.SetBool("alive", true);
    }


    void Update()
    {
        // Update grounded status
        grounded = pGrounded.grounded;

        // Decrement jump timer
        jumpTimer -= Time.deltaTime;
        
        // Determine horizontal movement
        fHorizontalVelocity = rb.velocity.x;
        fHorizontalVelocity += Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f) 
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.deltaTime * 10f);
        } 
        else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizontalVelocity))
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.deltaTime * 10f);
        }
        else
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingBasic, Time.deltaTime * 10f);
        }
        rb.velocity = new Vector2(fHorizontalVelocity, rb.velocity.y);

        // Flip sprite if necessary
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

        if (Input.GetKey(KeyCode.Space))
        {
            if (jumpTimer > 0)
            {
                Debug.Log("extra jumping");
                rb.AddForce(new Vector2(0, jumpPower / 50), ForceMode2D.Impulse);
            }
        }

        UpdateAnimator();
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        anim.SetTrigger("jump");
        dust.CreateJumpDust();
    }

    private void UpdateAnimator()
    {
        anim.SetFloat("movement", Input.GetAxisRaw("Horizontal"));
        anim.SetBool("grounded", grounded);
    }
}
