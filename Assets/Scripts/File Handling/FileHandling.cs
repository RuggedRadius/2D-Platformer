using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FileHandling
{

    


    public static bool SaveData()
    {
        try
        {
            Debug.Log("Saving myData to file...");
            string location = string.Format("{0}\\data\\{1}", Application.dataPath, "playerData.dat");
            FileInfo dataFile = new FileInfo(location);
            using (FileStream stream = new FileStream(dataFile.FullName, FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, GameManager.playerData);

                Debug.Log("Player data saved successfully.");
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Error: failed to save data to file.");
            Debug.Log(ex.Message);

            return false;
        }
    }
    public static bool LoadData()
    {
        Console.WriteLine("Loading data from file...");
        try
        {
            string dir = Application.dataPath;
            string dirRoot = dir.Replace("/Assets", "");

            string location = string.Format("{0}\\data\\{1}", dirRoot, "playerData.dat");
            FileInfo dataFile = new FileInfo(location);

            using (Stream stream = File.Open(dataFile.FullName, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                // Deserilise stream of binary data into object
                GameManager.playerData = (PlayerData) binaryFormatter.Deserialize(stream);

                Debug.Log("Player data file loaded.");

                return true;
            }
        }
        catch (FileNotFoundException ex)
        {
            Debug.LogError("No data exists.");
            GameManager.playerData = new PlayerData();
            SaveData();
            LoadData();
            return false;
        }
    }

}

