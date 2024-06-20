using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    
    [SerializeField] GameObject noteV;
    [SerializeField] GameObject noteC;
    [SerializeField] GameObject noteX;
    [SerializeField] GameObject noteZ;
    [SerializeField] Transform playerTransform;
    [SerializeField] float screenEdge;
    [SerializeField] float groundNoteHeight;
    [SerializeField] float maxNoteHeight;
    [SerializeField] float midNoteHeight;
    [SerializeField] float minNoteHeight;
    Vector3 nextNotePos = new Vector3(136.91f, 2.764f, 104.42f); //Copied from PlayerMovement char's start position.;
    int currentIndex = 0;
    private double frac;
    //Ctrl+E+W to change wrap around.
    List<float> xPositionOfNotes = new List<float>() {
        //0 is start of first verse segment (-100)
        100.26f, 96.23893f, 91.95826f, 85.67049f, 81.1129f, 78.97399f, 74.63135f, 71.26995f, 66.45798f, 62.42423f, //10
        58.22083f, 51.84604f, 47.76973f, 45.59886f, 41.76114f, 37.38012f, 32.81517f, 28.80242f, 24.75176f, 18.51747f, //20
        14.21097f, 11.8817f, 8.192513f, 3.882403f,  -0.4808555f, -4.760947f,  -8.889845f,  -14.84284f, -19.4566f, -21.80591f, //30
        //31 start of second verse segment (-30)
        -25.37619f, -29.70992f, -34.031f, -37.90761f,  -42.45469f, -48.43943f, -52.52016f, -54.7162f, -58.79808f, -63.05347f, //40
        -67.41456f, -71.28312f, -76.08083f, -81.78916f, -86.34604f, -88.32552f, -92.60966f, -96.75107f, -101.066f, -105.1006f, //50
        -109.1984f,  -115.4061f, -120.0183f, -122.1534f, -126.4773f, -130.3214f, -135.0847f, -138.9155f, -142.8057f,  -148.9937f, //60
        //63 is start of third verse segment (-165)
        -153.55f, -156.0417f, -160.0149f, -164.1539f,  -168.4529f, -172.8066f, -177.0889f, -183.2981f, -187.4274f, -189.5552f, //70
        -193.4005f, -197.7395f, -202.0474f, -206.1104f, -210.4179f, -216.6542f,  -220.8046f, -222.9322f, -227.0443f, -231.1028f, //80
         -235.6473f, -239.7033f, -243.5733f, -249.8448f, -254.338f, -256.7381f, -260.8201f, -264.9527f, -269.4851f, -273.7743f, //90
         //96 is start of first chorus segment (-308)
        -277.8695f, -284.0983f, -288.9636f, -290.876f, -295.0498f, -298.5161f, -311.4875f, -328.7604f,  -345.1164f, -361.4208f, //100
        //103 is start of second chorus segment (-443)
        //109 is start of first outro segment (-569)
        -378.4388f, -395.4573f, -412.3076f, -446.138f, -462.941f, -479.7523f, -496.2909f, -513.3336f, -529.8619f, -572.2576f, //110
        -576.6733f,  -580.5591f, -584.8418f, -587.0385f, -591.0999f, -595.1585f, -597.3401f, -601.142f, -605.7703f,  -609.8522f, //120
         -613.8751f, -618.4138f, -620.6279f, -624.6816f, -629.0455f, -631.1353f, -635.2581f,  -639.6141f, -643.4359f, -647.7203f, //130
         //137 is start of second outro segment (-737)
        -652.1423f, -654.1753f, -658.5036f, -662.6164f, -665.0184f, -668.636f, -672.6959f, -740.1719f, -744.4438f, -748.7874f, //140
        -753.0626f, -754.9789f, -759.0886f, -763.2054f, -765.6022f, -769.6295f, -773.7516f, -777.7772f, -781.8854f, -786.4688f, //150
        -788.6249f, -792.7285f, -797.0027f, -799.1458f, -803.2703f, -807.5642f, -811.4258f, -815.4566f, -820.2857f, -822.4728f, //160
        -826.2689f, -830.1627f, -832.5598f, -836.3936f,  -841.3253f, -845,1475f, -849.4681f, -853.6711f, -855.7802f, -859.886f, //170
        -863.9691f, -866.3346f, -870.1935f, -874.5278f, //174
    };
    //4 corresponds to max height of red note, 3 is mid, 2 is min.
    List<float> yPositionOfNotes = new List<float>() {
        //0 is start of first verse segment
        0, 0, 0, 0, 0, 0, 0.5f, 0.6f, 0.7f, 0.7f, //10
        0.7f, 2.7f, 2.7f, 2.7f, 2.6f, 2.5f, 2.7f, 2.7f, 2.7f, 0.7f, //20
        0.7f, 0.6f, 0.5f, 0.6f, 0.7f, 0.7f, 0.7f, 4.7f, 4.7f, 4.6f, //30
        //31 start of second verse segment
        4.5f, 4.6f, 3, 3, 2, 2, 0, 0, 0.5f, 0.6f, //40
        0.7f, 0.7f, 0.7f, 4.7f, 4.7f, 4.7f, 4.6f, 4.5f, 3.7f, 3.7f, //50
        2.7f, 2.7f, 0.7f, 0.6f, 0.5f, 0.6f, 0.7f, 0.7f, 0.7f, 4.7f, //60
        //63 is start of third verse segment
        4.7f, 4.6f, 4.5f, 4.6f, 3, 3, 2, 0, 0, 0, //70
        0.5f, 0.6f, 0.7f, 0.7f, 0.7f, 4.7f, 4.7f, 4.7f, 4.6f, 4.5f, //80
        3.7f, 3.7f, 2.7f, 0.7f, 0.7f, 0.6f, 0.5f, 0.6f, 0.7f, 0.7f, //90
        //96 is start of first chorus segment
        0.7f, 4.7f, 4.7f, 4.7f, 4.7f, 0, 0, 0, 0, 0, //100
        //103 is start of second chorus segment
        //109 is start of first outro segment
        4, 0, 0, 0, 0, 0, 0, 0, 0, 0, //110
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //120
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //130
        //137 is start of second outro segment
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //140
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //150
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //160
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //170
        0, 0, 0, 0, //174
    };
    int listSize;

    private void Start()
    {
        listSize = xPositionOfNotes.Count;
        if (screenEdge == 0) screenEdge = -10f;
        if (groundNoteHeight == 0) groundNoteHeight = 4.4f;
        if (maxNoteHeight == 0) maxNoteHeight = 9f;
        if (midNoteHeight == 0) midNoteHeight = 7f;
        if (minNoteHeight == 0) minNoteHeight = 4.4f;
        nextNotePos.x = xPositionOfNotes[currentIndex];
        nextNotePos.y = groundNoteHeight;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentIndex < listSize)
        {
            if (currentIndex < 96)
            //if ((playerTransform.position.x + screenEdge) <= nextNotePos.x)
            {

                if (yPositionOfNotes[currentIndex] == 0f || yPositionOfNotes[currentIndex] == 2f || yPositionOfNotes[currentIndex] == 3f || yPositionOfNotes[currentIndex] == 4f) 
                {                    
                    Instantiate(noteV, nextNotePos, Quaternion.identity);
                } else if (yPositionOfNotes[currentIndex] == 0.5f || yPositionOfNotes[currentIndex] == 2.5f || yPositionOfNotes[currentIndex] == 3.5f || yPositionOfNotes[currentIndex] == 4.5f)
                {
                    Instantiate(noteC, nextNotePos, Quaternion.identity);
                }
                else if (yPositionOfNotes[currentIndex] == 0.6f || yPositionOfNotes[currentIndex] == 2.6f || yPositionOfNotes[currentIndex] == 3.6f || yPositionOfNotes[currentIndex] == 4.6f)
                {
                    Instantiate(noteX, nextNotePos, Quaternion.identity);
                }
                else if (yPositionOfNotes[currentIndex] == 0.7f || yPositionOfNotes[currentIndex] == 2.7f || yPositionOfNotes[currentIndex] == 3.7f || yPositionOfNotes[currentIndex] == 4.7f)
                {
                    Instantiate(noteZ, nextNotePos, Quaternion.identity);
                }
                currentIndex++;
                //Debug.Log(currentIndex + " is the current index and list size is " + listSize);
                if (currentIndex < listSize)
                {
                    nextNotePos.x = xPositionOfNotes[currentIndex];
                    if (yPositionOfNotes[currentIndex] < 1) //some values are 0.01f or 4.01f
                    {
                        nextNotePos.y = groundNoteHeight;
                    } else
                    {
                        if (Math.Floor(yPositionOfNotes[currentIndex]) == 2) nextNotePos.y = minNoteHeight;
                        if (Math.Floor(yPositionOfNotes[currentIndex]) == 3) nextNotePos.y = midNoteHeight;
                        if (Math.Floor(yPositionOfNotes[currentIndex]) == 4) nextNotePos.y = maxNoteHeight;
                    }
                    //Debug.Log("xPositionOfNotes[currentIndex] is " + xPositionOfNotes[currentIndex]);
                }
            }     
        }        

    }
    

}
