using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    
    [SerializeField] GameObject noteLeft;
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 offset;
    Vector3 nextNotePos = new Vector3(136.91f, 2.764f, 104.42f); //Copied from PlayerMovement char's start position.;
    int currentIndex = 0;
    List<float> xPositionOfNotes = new List<float>() { 110.2f, 95f, 77.1f };
    int listSize;

    private void Start()
    {
        listSize = xPositionOfNotes.Count;
        if ((offset.x + offset.y + offset.z) <= 0)
        {
            offset = new Vector3(-10f, 2f, 0f);
        }
        nextNotePos.x = xPositionOfNotes[currentIndex];

        /*
        nextNotePos.x = xPositionOfNotes[currentIndex];
        Instantiate(noteLeft, nextNotePos, Quaternion.identity);

        currentIndex++;

        nextNotePos.x = xPositionOfNotes[currentIndex];
        Instantiate(noteLeft, nextNotePos, Quaternion.identity);
        */

    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentIndex < listSize)
        {
            if ((playerTransform.position.x + offset.x) <= nextNotePos.x)
            {
                Instantiate(noteLeft, nextNotePos, Quaternion.identity);
                currentIndex++;
                Debug.Log(currentIndex + " is the current index and list size is " + listSize);
                if (currentIndex < listSize)
                {
                    nextNotePos.x = xPositionOfNotes[currentIndex];
                    Debug.Log("xPositionOfNotes[currentIndex] is " + xPositionOfNotes[currentIndex]);
                }
            }     
        }
        

    }
    

}
