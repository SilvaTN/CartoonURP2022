using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TrailMusicSheet trailScript;
    [SerializeField] Rigidbody rb;
    [SerializeField] float jumpForce;
    [SerializeField] float runSpeed;
    [SerializeField] float customGravity;
    [SerializeField] bool isOnGround = true;
    [SerializeField] Animator GuitarAnimator;
    private ConstantForce cForce;
    private Vector3 forceDirection;

    // Start is called before the first frame update
    void Start()
    {
        //Keep the start position the same in relation to the position of the notes.
        transform.position = new Vector3(136.91f, 3f, 104.42f); // sets char start position.

        if (runSpeed == 0)
        {
            runSpeed = 15f;
        }
        if (jumpForce == 0)
        {
            jumpForce = 25f;
        }
        if (customGravity == 0)
        {
            customGravity = -50f;
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
            //call JumpTrail() after 0.1 seconds of being in the air (maybe have a counter inside FixedUpdate)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //ForceMode.Impulse applies force immediately.
            isOnGround = false;
            GuitarAnimator.Play("GuitarChar_Jump");
        }
    }
    
    void FixedUpdate()
    {
        //rb.AddForce(Physics.gravity * customGravity);        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            GuitarAnimator.Play("GuitarChar_Running");
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameManager.EndGame();
        }
    }

}
