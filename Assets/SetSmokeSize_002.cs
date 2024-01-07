using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSmokeSize_002 : MonoBehaviour
{
    public ParticleSystem psLeft;
    public ParticleSystem psRight;
    float randomLeftSize;
    float randomRightSize;
    int zeroMeansLeftIsBigger;
    private float numberOfTimesPlayWasConfirmed = 0;
    private float START_OF_HIGH_RANGE = 540.0f;
    private float START_OF_LOW_RANGE = 440.0f;
    private float GREATEST_RANGE = 20.0f;

    // Update is called once per frame
    void Update()
    {
        if (psLeft.isPlaying == true)
        {
            numberOfTimesPlayWasConfirmed++;
        }

        //true when finished the animation loop
        if (psLeft.isPlaying == false && numberOfTimesPlayWasConfirmed > 1)
        {
            numberOfTimesPlayWasConfirmed = 0;
        }

        //true when particle system was just turned on
        if (numberOfTimesPlayWasConfirmed == 1)
        {
            var mainLeft = psLeft.main;
            var mainRight = psRight.main;
            randomLeftSize = Random.Range(1.0f, GREATEST_RANGE);
            randomRightSize = Random.Range(1.0f, GREATEST_RANGE);
            zeroMeansLeftIsBigger = Random.Range(0, 2);
            if (zeroMeansLeftIsBigger == 0)
            {
                mainLeft.startSize = START_OF_HIGH_RANGE + randomLeftSize;
                mainRight.startSize = START_OF_LOW_RANGE - randomRightSize;
            } else
            {
                mainLeft.startSize = START_OF_LOW_RANGE - randomLeftSize;
                mainRight.startSize = START_OF_HIGH_RANGE + randomRightSize;
            }            
        }

        //true when particle is playing AFTER initial turn on
        if (numberOfTimesPlayWasConfirmed > 1)
        {
            numberOfTimesPlayWasConfirmed = 2; //prevents overflow/wrap around
        }
    }
}
