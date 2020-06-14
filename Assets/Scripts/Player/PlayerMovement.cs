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
    public float currentInput;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        playerSprite = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();

        // Start grounded check
        StartCoroutine(checkGround());

        // Init animator
        anim.SetBool("alive", true);
    }

    void Update()
    {
        // Update animator
        currentInput = Input.GetAxisRaw("Horizontal");        
        currentMovement = Vector2.right * currentInput * Time.deltaTime * horizontalPower;
        this.transform.position += new Vector3(currentInput * Time.deltaTime * horizontalPower, 0f, 0f);

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

        UpdateAnimator();

        Camera.main.transform.position = new Vector3(
            this.transform.position.x,
            Camera.main.transform.position.y,
            Camera.main.transform.position.z
            );
    }

    private void UpdateAnimator()
    {
        anim.SetFloat("movement", currentInput);
        anim.SetBool("grounded", grounded);
    }


    
    Ray ray;
    float groundedDistance = 1f;
    Vector2 origin;
    Vector2 direction;
    ContactFilter2D filter;
    RaycastHit2D hit;
    public bool grounded;

    private IEnumerator checkGround()
    {
        
        LayerMask layerMask = LayerMask.GetMask("Walkable");

        // Change this to while alive later when health/life is implemented!!
        while (true)
        {
            // Set position and direction of shot
            origin = this.transform.position;
            direction = -this.transform.up;

            // Shoot the shot
            hit = Physics2D.Raycast(origin, direction, 1f, layerMask);
            


            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.name);

                // Grounded
                setGrounded(true);
            }
            else
            {
                // Not grounded
                setGrounded(false);
            }
            

            yield return null;
        }
    }

    /// <summary>
    /// Sets grounded status and updates animator values.
    /// </summary>
    /// <param name="value"></param>
    private void setGrounded(bool value)
    {
        grounded = value;
        anim.SetBool("grounded", value);
    }
}
