using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{
    RaycastHit2D hit;
    Ray2D rayLook;
    Rigidbody2D rb;

    [Header("State")]
    public bool movingLeft;
    public Vector3 currentMovement;

    [Header("Settings")]
    public bool ignoreEdges;
    public float movementSpeed;
    public float secondaryCheckDistance;

    [Header("Layer Settings")]
    public LayerMask walkableLayers;
    public LayerMask blockingLayers;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Mathf.Abs(rb.velocity.y) > 0.05f)
            return;

        // Detection
        if (movingLeft) 
        {
            rayLook = new Ray2D(transform.position + (Vector3.up / 10), Vector2.down + Vector2.left);
        } 
        else 
        {
            rayLook = new Ray2D(transform.position + (Vector3.up / 10), Vector2.down + Vector2.right);
        }


        if (!ignoreEdges)
        {
            // Initial look for walkable surface
            if (Physics2D.Raycast(rayLook.origin, rayLook.direction, 1.4f, walkableLayers))
            {
                // Found walkable surface moving forward
                //Debug.Log("Enemy hit walkable surface [DOWN + FORWARD]");
                SecondaryLookCast();
            }
            else
            {
                //Debug.Log("No surface detected ahead, reversing direction");
                movingLeft = !movingLeft;
            }
        }
        else
        {
            SecondaryLookCast();
        }

        // Determine Movement
        if (movingLeft)
            currentMovement = Vector3.left * Time.deltaTime * movementSpeed;
        else        
            currentMovement = Vector3.right * Time.deltaTime * movementSpeed;
            
        // Apply Movement
        this.transform.position += currentMovement;
    }

    private void SecondaryLookCast()
    {
        if (movingLeft)
        {
            rayLook = new Ray2D(transform.position + (Vector3.up / 10), Vector2.left);
        }
        else
        {
            rayLook = new Ray2D(transform.position + (Vector3.up / 10), Vector2.right);
        }

        // Horizontal secondary look for blocked path ahead
        //Debug.DrawRay(rayLook.origin, rayLook.direction);
        hit = Physics2D.Raycast(rayLook.origin, rayLook.direction, secondaryCheckDistance, blockingLayers);
        if (hit.collider != null)
        {
            //Debug.Log(hit.collider.gameObject.name);
            switch (hit.collider.gameObject.layer)
            {
                case 10:
                    //Debug.Log(hit.collider.gameObject.name);
                    break;

                default:
                    //Debug.Log("Enemy hit blocked path [FORWARD] on layer: " + hit.collider.gameObject.layer);
                    //Debug.Log("Distance: " + hit.distance);
                    movingLeft = !movingLeft;
                    break;
            }

        }
        else
        {
            //Debug.Log("Enemy path is clear");
        }
    }
}
