using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameManager gm;

    private Animator anim;


    void Start()
    {
        anim = this.GetComponent<Animator>();
        enemiesMask = LayerMask.GetMask("Enemy");
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            MeleeAttack();            
        }
    }


    RaycastHit2D hit;
    Vector2 origin;
    Vector2 direction;
    LayerMask enemiesMask;
    float meleeDistance = 2f;
    int damageMelee = 50;
    private void MeleeAttack()
    {
        origin = this.transform.position + (-transform.up * 0.5f);
        direction = this.transform.forward;

        // Attack
        Debug.Log("Attacking");
        hit = Physics2D.Raycast(origin, direction, meleeDistance, enemiesMask);
        Debug.DrawRay(origin, Vector2.right, Color.red, 1f);

        // If something hit...
        if (hit.collider != null)
        {
            try
            {
                Debug.Log("Attacked: " + hit.collider.name + " for " + damageMelee + " damage.");

                // Get the hit enemy
                Life enemyLife = hit.collider.gameObject.GetComponent<Life>();

                // Apply damage
                enemyLife.takeDamage(damageMelee);

                // Increment player's score
                GameManager.gameData.score += Mathf.Abs(hit.collider.gameObject.GetComponent<Enemy>().scoreValue);
            }
            catch (Exception ex)
            {
                Debug.LogError("Could not find enemy script on gameobject on enemy layer");
                ex.ToString();
            }
        }

        // Update animator
        anim.SetTrigger("attack");
    }
}
