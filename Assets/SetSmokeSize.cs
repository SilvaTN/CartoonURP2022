using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSmokeSize : MonoBehaviour
{
    private ParticleSystem ps;
    float randomSize;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        //Debug.Log("particlce system isPlaying value is: " + ps.isPlaying);
        //main.startSize = randomSize;
    }

    // Update is called once per frame
    void Update()
    {
        randomSize = Random.Range(100.0f, 1000.0f);
        if (ps.isPlaying == false)
        {
            Debug.Log("Stopped playing");
        }
    }
}
