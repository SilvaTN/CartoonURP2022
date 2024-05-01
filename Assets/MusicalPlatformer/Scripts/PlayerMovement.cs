using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float runSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (runSpeed <= 0)
        {
            runSpeed = 200f;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.AddForce(-runSpeed, 0f, 0f * Time.deltaTime);

        //makes char jump but need to fix code
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0f, 200f, 0f);
        }
    }


}
