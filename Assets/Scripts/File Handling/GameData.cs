using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameData
{
    public Scene currentScene;

    public EnemyDataSave[] onScreenEnemies;

    public float currentPositionX;
    public float currentPositionY;
    
    public int score;
    public int lives;
    public int health;

    public GameData ()
    {
        score = 0;
        lives = 5;
        health = 100;
    }
}

