using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveCharPositionPressed : MonoBehaviour
{
    private int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            //Debug.Log("('LeftNote', " + currentIndex + ", " + transform.position.x + ")");
            Debug.Log(transform.position.x);
            currentIndex++;
        }

    }

}
