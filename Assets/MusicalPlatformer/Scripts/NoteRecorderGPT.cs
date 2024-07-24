using UnityEngine;
using System.IO;
using System;

public class NoteRecorderGPT : MonoBehaviour
{
    private string filePath;
    private int existingRecordCount;
    private int keyPressIndex;

    void Start()
    {
        // Define the file path in the Assets folder
        filePath = Path.Combine(Application.dataPath, "MusicalPlatformer/Scripts/positionOfNotesGPT.txt");
        // Ensure the directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        // Check if the file exists and count the existing records
        existingRecordCount = 0;
        keyPressIndex = 0;
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            existingRecordCount = lines.Length;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.C) ||
            Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Z) ||
            Input.GetKeyDown(KeyCode.R)) 
        {
            RecordNote();
            keyPressIndex++;
        }
    }

    void RecordNote()
    {
        // Determine the key pressed
        KeyCode key = KeyCode.None;
        if (Input.GetKeyDown(KeyCode.V)) key = KeyCode.V;
        else if (Input.GetKeyDown(KeyCode.C)) key = KeyCode.C;
        else if (Input.GetKeyDown(KeyCode.X)) key = KeyCode.X;
        else if (Input.GetKeyDown(KeyCode.Z)) key = KeyCode.Z;

        Vector3 position = transform.position;
        string newRecord = $"{key.ToString()},{position.x},{position.y},{position.z}";

        string[] lines = File.Exists(filePath) ? File.ReadAllLines(filePath) : new string[0];
        if (keyPressIndex < lines.Length)
        {
            // Replace existing line but keep the noteType unchanged
            string[] parts = lines[keyPressIndex].Split(',');
            string noteType = parts[4];
            newRecord = $"{key.ToString()},{position.x},{position.y},{position.z},{noteType}";          
            lines[keyPressIndex] = newRecord;
        }
        else
        {
            // Append new record
            newRecord += ",SingleNote";
            Array.Resize(ref lines, lines.Length + 1);
            lines[lines.Length - 1] = newRecord;
        }

        // Write all lines back to the file
        File.WriteAllLines(filePath, lines);
    }
}








/* 
using UnityEngine;
using System.IO;

public class NoteRecorderGPT : MonoBehaviour
{
    private string filePath;
    private int existingRecordCount;
    private int keyPressIndex;

    void Start()
    {
        // Define the file path in the Assets folder
        filePath = Path.Combine(Application.dataPath, "MusicalPlatformer/Scripts/positionOfNotesGPT.txt");
        // Ensure the directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        // Check if the file exists and count the existing records
        existingRecordCount = 0;
        keyPressIndex = 0;
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            existingRecordCount = lines.Length;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.C) ||
            Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Z))
        {
            RecordNote();
            keyPressIndex++;
        }        
    }

    void RecordNote()
    {
        // Determine the key pressed
        KeyCode key = KeyCode.None;
        if (Input.GetKeyDown(KeyCode.V)) key = KeyCode.V;
        else if (Input.GetKeyDown(KeyCode.C)) key = KeyCode.C;
        else if (Input.GetKeyDown(KeyCode.X)) key = KeyCode.X;
        else if (Input.GetKeyDown(KeyCode.Z)) key = KeyCode.Z;

        if (keyPressIndex >= existingRecordCount)
        {
            // Record the key, position, and note type
            Vector3 position = transform.position;
            string noteType = "SingleNote";
            string newRecord = $"{key.ToString()},{position.x},{position.y},{position.z},{noteType}";

            // Append the data to the file
            File.AppendAllText(filePath, newRecord + "\n");
        }
        else
        {
            Debug.Log("Note already recorded at this index.");
        }
    }
}

*/
