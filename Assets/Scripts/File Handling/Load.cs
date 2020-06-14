
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Load
{
    private static BinaryFormatter binaryFormatter = new BinaryFormatter();

    public static void LoadFile()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            // Get file
            using (FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open))
            {
                // Load file
                GameManager.gameData = (GameData) binaryFormatter.Deserialize(file);

                //LoadScene();
                LoadEnemyData();
                LoadObjects();
                LoadPlayerPosition();


                Debug.Log("Game data loaded");
            }
        }
        else
        {
            Debug.Log("No game data found");
        }
    }

    private static void LoadEnemyData()
    {
        Debug.Log("Loading " + GameManager.gameData.onScreenEnemies.Length + " on screen enemies");

        // Get all active enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            foreach (EnemyDataSave enemyDataSave in GameManager.gameData.onScreenEnemies)
            {
                if (enemyDataSave.id == enemy.GetComponent<Enemy>().id)
                {
                    // Position
                    enemy.transform.position = new Vector3(enemyDataSave.positionX, enemyDataSave.positionY, 0);

                    // Set ALL enemies details
                    // ...
                    // Health
                    // Direction

                    Debug.Log("Loaded " + enemyDataSave.type + " at X: " + enemyDataSave.positionX + " Y: " + enemyDataSave.positionY);

                    break;
                }
            }

            // If not found, assume already killed
            //enemy.GetComponent<Life>().Die();
        }
    }

    private static void LoadObjects()
    {

    }

    private static void LoadScene()
    {
        SceneManager.LoadScene(GameManager.gameData.currentScene.name);
    }

    private static void LoadPlayerPosition()
    {
        // Get position
        float positionX = GameManager.gameData.currentPositionX;
        float positionY = GameManager.gameData.currentPositionY;

        // Place player at position
        if (GameManager.player != null)
        {
            GameManager.player.transform.position = new Vector3(positionX, positionY, 0);
        }
        else
        {
            Debug.Log("No player position to position");
        }
    }
}

