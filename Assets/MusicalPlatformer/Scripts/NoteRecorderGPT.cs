using UnityEngine;
using System.IO;

public class NoteRecorderGPT : MonoBehaviour
{
    private string filePath;

    void Start()
    {
        // Define the file path in the Assets folder
        filePath = Path.Combine(Application.dataPath, "MusicalPlatformer/Scripts/positionOfNotesGPT.txt");
        // Ensure the directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.C) ||
            Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Z))
        {
            RecordNote();
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

        // Check if the file exists and count the existing records
        int existingRecordCount = 0;
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            existingRecordCount = lines.Length;
        }

        // Only record if the new index exceeds the existing records
        int keyPressIndex = existingRecordCount;
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
