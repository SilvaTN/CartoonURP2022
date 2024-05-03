using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManaging : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Testinggggg ffs");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            AudioListener.volume = 0;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            AudioListener.volume = 1;
        }
    }
}
