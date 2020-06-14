using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int id;
    public EnemyType type;
    public int scoreValue;

    public EnemyMovement movement;
    public EnemyCollide collision;

    void Start()
    {
        movement = this.GetComponent<EnemyMovement>();
        collision = this.GetComponent<EnemyCollide>();
    }
}
