using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float jumpForce;
    [SerializeField] float runSpeed;
    [SerializeField] float customGravity;
    [SerializeField] bool isOnGround = true;
    private ConstantForce cForce;
    private Vector3 forceDirection;


    // Start is called before the first frame update
    void Start()
    {
        //Keep the start position the same in relation to the position of the notes.
        transform.position = new Vector3(136.91f, 2.764f, 104.42f); // sets char start position.

        if (runSpeed == 0)
        {
            runSpeed = 20f;
        }
        if (jumpForce == 0)
        {
            jumpForce = 20f;
        }
        if (customGravity == 0)
        {
            customGravity = -9.8f;
        }
        cForce = GetComponent<ConstantForce>();
        cForce.force = new Vector3(0, customGravity, 0);


    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * -runSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //ForceMode.Impulse applies force immediately.
            isOnGround = false;
        }
    }
    
    void FixUpdate()
    {
        rb.AddForce(Physics.gravity * customGravity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

}
