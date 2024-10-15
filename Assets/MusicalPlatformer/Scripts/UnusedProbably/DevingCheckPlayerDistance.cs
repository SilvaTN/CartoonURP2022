using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevingCheckPlayerDistance : MonoBehaviour
{
    private float initialPositionX = 136.91f;
    private float distanceTraveled;
    private const float measurementTime = 20f;

    private void Start()
    {
        // Start the distance check coroutine
        StartCoroutine(CheckDistance());
    }

    private IEnumerator CheckDistance()
    {
        // Wait for 20 seconds
        yield return new WaitForSeconds(measurementTime);

        // Calculate the distance traveled
        float finalPositionX = transform.position.x;
        distanceTraveled = initialPositionX - finalPositionX;

        // Calculate the distance traveled per second
        float distanceInOneSecond = distanceTraveled / measurementTime;

        // Print the result to the console
        Debug.Log("Distance traveled in one second: " + distanceInOneSecond);
    }
}
