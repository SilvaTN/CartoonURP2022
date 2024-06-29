using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadAndSpawnPrefabs : MonoBehaviour
{
    public GameObject prefab;
    private string filePath;

    void Start()
    {
        // Set the file path to the specific directory
        filePath = Application.dataPath + "/MusicalPlatformer/Scripts/player_positions.txt";

        // Load positions and spawn prefabs
        LoadPositionsAndSpawnPrefabs();
    }

    void LoadPositionsAndSpawnPrefabs()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("File not found: " + filePath);
            return;
        }

        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length == 2)
            {
                float x, y;
                if (float.TryParse(parts[0], out x) && float.TryParse(parts[1], out y))
                {
                    Vector3 spawnPosition = new Vector3(x, y+2f, 104.42f); // Adjust Z value as needed
                    Instantiate(prefab, spawnPosition, Quaternion.identity);
                }
                else
                {
                    Debug.LogWarning("Invalid position data: " + line);
                }
            }
            else
            {
                Debug.LogWarning("Invalid line format: " + line);
            }
        }
    }
}
