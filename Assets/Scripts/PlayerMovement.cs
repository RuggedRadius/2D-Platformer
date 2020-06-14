using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 currentMovement;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Animator anim;

    [Header("Settings")]
    public float jumpPower;
    public float horizontalPower;
    public float horizontalSpeedMax;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        playerSprite = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();

        // Init animator
        anim.SetBool("alive", true);
    }

    void Update()
    {
        currentMovement = Vector2.right * Input.GetAxis("Horizontal") * Time.deltaTime * horizontalPower;
        this.transform.position += new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * horizontalPower, 0f, 0f);

        if (!Mathf.Approximately(0, currentMovement.x))
        {
            if (currentMovement.x > 0)
            {
                playerSprite.flipX = false;
                
            }
            else
            {
                playerSprite.flipX = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //print("Space pushed");
            if (Mathf.Abs(rb.velocity.y) < 0.001f)
            {
                //print("Jumping");
                rb.AddForce(new Vector2(0, jumpPower * (rb.mass / 10f)), ForceMode2D.Impulse);
                anim.SetTrigger("jump");
            }
            else
            {
                //print("Falling, cant jump");
            }
        }

        // Update animator
        anim.SetFloat("movement", currentMovement.x);

        Camera.main.transform.position = new Vector3(
            this.transform.position.x,
            Camera.main.transform.position.y,
            Camera.main.transform.position.z
            );
    }
}
