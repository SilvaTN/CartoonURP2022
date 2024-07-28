using UnityEngine;
using System.IO;

public class NoteSpawnerGPT : MonoBehaviour
{
    public GameObject noteVSingle, noteVDouble, noteVTriple;
    public GameObject noteCSingle, noteCDouble, noteCTriple;
    public GameObject noteXSingle, noteXDouble, noteXTriple;
    public GameObject noteZSingle, noteZDouble, noteZTriple;
    public GameObject noteRainbowSingle, noteRainbowDouble, noteRainbowTriple;
    public GameObject noteGoldSingle, noteGoldDouble, noteGoldTriple;
    public float offsetY = 0f;

    private string filePath;

    void Start()
    {
        filePath = Path.Combine(Application.dataPath, "MusicalPlatformer/Scripts/positionOfNotesGPT.txt");
        SpawnNotes();
    }

    void SpawnNotes()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Note data file not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        if (lines.Length == 0)
        {
            Debug.LogWarning("Note data file is empty.");
            return;
        }

        for (int i = 0; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');
            if (parts.Length < 5)
                continue;

            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), parts[0]);
            float x = float.Parse(parts[1]);
            float y = float.Parse(parts[2]);
            float z = float.Parse(parts[3]);
            string noteType = parts[4];

            // Determine the prefab to spawn
            GameObject prefab = null;
            switch (key)
            {
                case KeyCode.V:
                    prefab = GetPrefabForType(noteVSingle, noteVDouble, noteVTriple, noteType);
                    break;
                case KeyCode.C:
                    prefab = GetPrefabForType(noteCSingle, noteCDouble, noteCTriple, noteType);
                    break;
                case KeyCode.X:
                    prefab = GetPrefabForType(noteXSingle, noteXDouble, noteXTriple, noteType);
                    break;
                case KeyCode.Z:
                    prefab = GetPrefabForType(noteZSingle, noteZDouble, noteZTriple, noteType);
                    break;
                case KeyCode.R:
                    prefab = GetPrefabForType(noteRainbowSingle, noteRainbowDouble, noteRainbowTriple, noteType);
                    break;
                case KeyCode.G:
                    prefab = GetPrefabForType(noteGoldSingle, noteGoldDouble, noteGoldTriple, noteType);
                    break;
            }

            if (prefab != null)
            {
                Instantiate(prefab, new Vector3(x, y + offsetY, z), Quaternion.Euler(-90, 0, 0));
                // Skip spawning the next note based on note type
                if (noteType == "DoubleNote")
                {
                    i++;
                }
                else if (noteType == "TripleNote")
                {
                    i += 2;
                }
            }
        }
    }

    GameObject GetPrefabForType(GameObject single, GameObject doubleNote, GameObject tripleNote, string noteType)
    {
        switch (noteType)
        {
            case "SingleNote":
                return single;
            case "DoubleNote":
                return doubleNote;
            case "TripleNote":
                return tripleNote;
            default:
                return null;
        }
    }
}
