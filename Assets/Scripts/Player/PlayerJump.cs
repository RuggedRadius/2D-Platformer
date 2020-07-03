using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerGrounded pGrounded;
    private PlayerDustFX dust;

    [Range(100f, 350f)] public float jumpPower;
    private float jumpTimer = 0;
    private float JumpTime = 0.75f;

    public bool grounded;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        pGrounded = this.GetComponent<PlayerGrounded>();
        dust = this.GetComponent<PlayerDustFX>();
    }

    void Update()
    {
        // Update grounded status
        grounded = pGrounded.grounded;

        // Decrement jump timer
        jumpTimer -= Time.deltaTime;
        

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
                rb.AddForce(new Vector2(0, jumpPower * (jumpTimer / JumpTime) * 0.05f), ForceMode2D.Impulse);
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
