using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCollide : MonoBehaviour
{
    private Enemy enemy;
    private GameManager gm;
    private Rigidbody2D rbPlayer;
    private Image screen;

    [Header("Settings")]
    public Vector2 impactForce;
    public float killPopForce;

    [Header("State")]
    public bool damagingPlayer;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        enemy = this.GetComponent<Enemy>();
        screen = GameObject.FindGameObjectWithTag("UI").transform.Find("Screen").GetComponent<Image>();
        //if (GameObject.FindGameObjectWithTag("Player") != null)
        //rbPlayer = .GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rbPlayer == null)
        {
            try
            {
                rbPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            }
            catch (NullReferenceException ex)
            {
                Debug.Log("Player not spawned yet..");
                ex.ToString();
            }
        }
    }


    int damageTEMP = 50;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player...
        if (collision.gameObject.layer == 9)
        {
            // Player entered trigger
            //this.GetComponent<Life>().takeDamage(damageTEMP);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision Enter");
        if (collision.gameObject.layer == 9)
        {
            // Player collided with enemy
            damagePlayer(collision.GetContact(0).point);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 && !damagingPlayer)
        {
            // Player collided with enemy
            damagePlayer(collision.GetContact(0).point);
        }
    }

    public void damagePlayer(Vector2 hitPoint)
    {
        Debug.Log("Damaging player...");
        StartCoroutine(playerHitRoutine(hitPoint));
    }

    
    private IEnumerator playerHitRoutine(Vector2 hitPoint)
    {
        damagingPlayer = true;
        // Decrement player health
        GameManager.gameData.health--;

        // Damage FX on player, Flash sprite or something, shader perhaps
        // ...

        // Flash screen red
        //print("Start flash");
        float flashDuration = 0.05f;
        screen.color = Color.red;
        //yield return new WaitForSeconds(flashDuration);
        new WaitForSeconds(flashDuration);
        screen.color = Color.clear;
        //print("End flash");

        // Throw player back
        Vector2 force;
        if ((hitPoint.x - rbPlayer.transform.position.x) < 0)
        {
            force = new Vector2(-impactForce.x, impactForce.y);
        }
        else
        {
            force = new Vector2(impactForce.x, impactForce.y);
        }
        rbPlayer.AddForceAtPosition(force, hitPoint);


        // SFX
        // ...

        damagingPlayer = false;
        yield return null;
    }
}
