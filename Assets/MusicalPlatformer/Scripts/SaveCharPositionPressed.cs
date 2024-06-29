using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveCharPositionPressed : MonoBehaviour
{
    private string filePath;

    void Start()
    {
        // Set the file path to the specific directory
        filePath = Application.dataPath + "/MusicalPlatformer/Scripts/player_positions.txt";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SavePosition();
        }
    }

    void SavePosition()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        string positionString = position.x + "," + position.y;

        // Append the position to the file
        File.AppendAllText(filePath, positionString + "\n");

        Debug.Log("Position saved: " + positionString);
    }
}