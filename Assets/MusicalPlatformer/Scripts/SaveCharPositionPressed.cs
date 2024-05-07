using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            currentIndex++;
            Debug.Log(currentIndex + " --> " + transform.position.x);
        }

    }

}
