
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Save
{
    private static BinaryFormatter binaryFormatter = new BinaryFormatter();


    public static void SaveFile(GameData data)
    {
        using (FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd"))
        {
            // Gather all save-able information
            GatherAllSaveData();

            // Write to file
            binaryFormatter.Serialize(file, data);

            Debug.Log("Game data saved.");
        }
    }

    private static void GatherAllSaveData()
    {
        GatherScene();
        GatherPlayerPosition();
        GatherEnemyData();
    }

    private static void GatherScene()
    {
        // Save scene
        GameManager.gameData.currentScene = SceneManager.GetActiveScene();
    }
    private static void GatherPlayerPosition()
    {
        // Get current position
        if (GameManager.player != null)
        {
            GameManager.gameData.currentPositionX = GameManager.player.transform.position.x;
            GameManager.gameData.currentPositionY = GameManager.player.transform.position.y;
        }
        else
        {
            Debug.Log("No player position data saved.");
        }
    }
    private static void GatherEnemyData()
    {
        // Get all active enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Make sure each enemy has unique ID (Can be removed later***)
        if (ValidUnqiueEnemyIDs(enemies))
        {
            // Create list of on screen enemies
            List<EnemyDataSave> enemyData = new List<EnemyDataSave>();

            // Populate list of on screen enemies
            foreach (GameObject enemy in enemies)
            {
                enemyData.Add(new EnemyDataSave(enemy.GetComponent<Enemy>()));
            }

            // Add list to save data
            GameManager.gameData.onScreenEnemies = enemyData.ToArray();
        }
        else
        {
            Debug.LogError("Error saving file: Duplicate enemy IDs");
        }


    }

    private static bool ValidUnqiueEnemyIDs(GameObject[] enemies)
    {
        // Create list of game id ints
        List<int> idList = new List<int>();
        foreach (GameObject enemy in enemies)
        {
            idList.Add(enemy.GetComponent<Enemy>().id);
        }

        // Convert list to array
        int[] ids = idList.ToArray();

        // Compare list to distinct self to determine if it contains duplicates
        if (ids.Length != ids.Distinct().Count())
        {
            // Fail
            Debug.LogError("Duplicate enemy IDs exist!");
            return false;
        }
        else
        {
            // Pass
            return true;
        }
    }

    

    public static void InitialiseSaveFile()
    {
        Debug.Log("Initialising save file...");

        // Create new data
        GameData data = new GameData();

        // Set position
        data.currentScene = SceneManager.GetActiveScene();
        data.currentPositionX = 5;
        data.currentPositionY = 5;

        // Add new game data
        GameManager.gameData = data;

        // Save data
        SaveFile(GameManager.gameData);
    }
}

