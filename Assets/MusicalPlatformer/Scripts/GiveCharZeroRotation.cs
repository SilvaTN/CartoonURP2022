using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveCharZeroRotation : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, 104.42f);
            other.transform.rotation = Quaternion.identity; //otherwise, char changes rotation slightly for some reason.
            //Debug.Log("Staying, the position and rotation are " + other.transform.position + "and " + other.transform.rotation);
        }
            
    }

}