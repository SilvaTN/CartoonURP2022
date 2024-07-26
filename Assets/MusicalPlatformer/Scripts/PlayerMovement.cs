using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool moveToStartPosition = true;
    [SerializeField] GameManager gameManager;
    [SerializeField] Rigidbody rb;
    [SerializeField] float jumpForce;
    [SerializeField] float rainbowJumpForce;
    [SerializeField] float runSpeed;
    [SerializeField] float customGravity;
    [SerializeField] bool isOnGround = true;
    [SerializeField] ParticleSystem jumpPoof;
    [SerializeField] ParticleSystem jumpLandPoof;
    [SerializeField] Animator GuitarAnimator;
    private const string JUMP_ANIM = "GuitarChar_Jump_LessFrames";
    private const string RAINBOW_JUMP_ANIM = "GuitarChar_RainbowJump";
    private string currentJumpAnim;
    float upwardThrustForce;
    private ConstantForce cForce;
    private Vector3 forceDirection;
    private bool isJumping;
    private bool isFalling;
    [SerializeField] float fallThreshold;
    private float previousHeight;

    public void upwardsThrust(bool isRainbow = false)
    {        
        if (isRainbow)
        {
            upwardThrustForce = rainbowJumpForce;
            currentJumpAnim = RAINBOW_JUMP_ANIM;
        } else
        {
            upwardThrustForce = jumpForce;
            currentJumpAnim = JUMP_ANIM;
        }                    
        //call JumpTrail() after 0.1 seconds of being in the air (maybe have a counter inside FixedUpdate)
        rb.AddForce(Vector3.up * upwardThrustForce, ForceMode.Impulse); //ForceMode.Impulse applies force immediately.
        isOnGround = false;
        GuitarAnimator.SetBool("isOnGround", isOnGround);
        isJumping = true;
        GuitarAnimator.Play(currentJumpAnim);
        jumpPoof.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Keep the start position the same in relation to the position of the notes.
        if (moveToStartPosition) transform.position = new Vector3(136.91f, 3f, 104.42f); // sets char start position.       
        
        if (runSpeed == 0) runSpeed = 15f;      
        if (jumpForce == 0) jumpForce = 25f;
        if (rainbowJumpForce == 0) rainbowJumpForce = 15f;
        if (customGravity == 0) customGravity = -50f;        
        cForce = GetComponent<ConstantForce>();
        cForce.force = new Vector3(0, customGravity, 0);
                
        if (fallThreshold == 0) fallThreshold = 0.01f;
        previousHeight = transform.position.y - (fallThreshold * 100);

    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * -runSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            upwardsThrust();
        }
        if (isJumping)
        {         
            float currentHeight = transform.position.y;
            if (isFalling == false)
            {
                if ((previousHeight - fallThreshold) > currentHeight)
                {
                    isFalling = true;
                    GuitarAnimator.Play("GuitarChar_FallJump_LessFrames");
                }
            }
            previousHeight = currentHeight;
        } else
        {
            previousHeight = transform.position.y - (fallThreshold * 100); //otherwise jump fall plays immediately after jump.
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
            GuitarAnimator.SetBool("isOnGround", isOnGround);
            isJumping = false;
            isFalling = false;
            jumpLandPoof.Play();
            //GuitarAnimator.Play("GuitarChar_SquashRunning");
            //GuitarAnimator.Play("GuitarChar_Running");
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameManager.EndGame();
        }
    }

}
