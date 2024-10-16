using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool moveToStartPosition = true;
    [SerializeField] GameManager gameManager;
    [SerializeField] Rigidbody rb;
    [SerializeField] float jumpForce;
    [SerializeField] float rainbowJumpForce;
    [SerializeField] float goldJumpForce;
    [SerializeField] float flowerJumpForce;
    [SerializeField] float treeJumpForce;
    [SerializeField] float runSpeed;
    [SerializeField] float customGravity;
    [SerializeField] bool isOnGround = true;
    [SerializeField] ParticleSystem jumpPoof;
    [SerializeField] ParticleSystem jumpLandPoof;
    [SerializeField] Animator GuitarAnimator;
    private Vector3 startPosition = new Vector3(136.91f, 3f, 104.42f);
    private const string JUMP_ANIM = "GuitarChar_Jump_LessFrames";
    private const string SPECIAL_JUMP_ANIM = "GuitarChar_SpecialJump";
    private string currentJumpAnim;
    float upwardThrustForce;
    private ConstantForce cForce;
    private Vector3 forceDirection;
    private bool isJumping;
    private bool isFalling;
    //private bool canShowJumpLandPoof = false;
    [SerializeField] float fallThreshold;
    private float previousHeight;
    private bool isSpecialJumping;
    

    public void upwardsThrust(bool isRainbow = false, bool isGold = false, bool isFlower = false, bool isTree = false)
    {
        isSpecialJumping = isRainbow || isGold || isFlower || isTree;
        //Debug.Log("isSpecialJumping value is " + isSpecialJumping);
        if (isRainbow)
        {
            upwardThrustForce = rainbowJumpForce;
            currentJumpAnim = SPECIAL_JUMP_ANIM;
        } else if (isGold)
        {
            upwardThrustForce = goldJumpForce;
            currentJumpAnim = SPECIAL_JUMP_ANIM;
        } else if (isFlower)
        {
            upwardThrustForce = flowerJumpForce;
            currentJumpAnim = SPECIAL_JUMP_ANIM;
        }  else if (isTree)
        {
            upwardThrustForce = treeJumpForce;
            currentJumpAnim = SPECIAL_JUMP_ANIM;
        }
        else
        {
            upwardThrustForce = jumpForce;
            currentJumpAnim = JUMP_ANIM;
        }
        //call JumpTrail() after 0.1 seconds of being in the air (maybe have a counter inside FixedUpdate)
        rb.velocity = new Vector3(rb.velocity.x, 0, 0); //zero out the vertical velocity in case the player is falling bc we always want the same jump height regardless.
        rb.AddForce(Vector3.up * upwardThrustForce, ForceMode.Impulse); //ForceMode.Impulse applies force immediately.
        isOnGround = false;
        GuitarAnimator.SetBool("isOnGround", isOnGround);
        isJumping = true;
        //Debug.Log("currentJumpAnim is " + currentJumpAnim);
        if (isTree)
        {
            //otherwise can't do consecutive special jumps bc did not interrupt transition out of the special jump animation I think;
            GuitarAnimator.Play(currentJumpAnim, 0, 0f);
        } else
        {
            GuitarAnimator.Play(currentJumpAnim);
        }
        
        jumpPoof.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Keep the start position the same in relation to the position of the notes.
        if (moveToStartPosition)
        {
            int songStartingTime = GetComponent<PlayerSound>().songStartTime;
            //Debug.Log("songStartTime inside PlayerMovement is " + songStartingTime);
            startPosition.x = startPosition.x - (runSpeed * songStartingTime);
            transform.position = startPosition; // sets char start position. 
        }
        
        isSpecialJumping = false;
        if (runSpeed == 0) runSpeed = 15f;      
        if (jumpForce == 0) jumpForce = 25f;
        if (rainbowJumpForce == 0) rainbowJumpForce = 15f;
        if (goldJumpForce == 0) goldJumpForce = 25f;
        if (flowerJumpForce == 0) flowerJumpForce = 25f;
        if (treeJumpForce == 0) treeJumpForce = 25f;
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
            if ((isFalling == false) &&(!isSpecialJumping))
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
            transform.position = new Vector3(transform.position.x, transform.position.y, 104.42f); //otherwise, char changes rotation/position slightly in rainbow path for some reason.
            transform.rotation = Quaternion.identity; //otherwise, char changes rotation/position slightly in rainbow path for some reason.

            isOnGround = true;
            //if (isSpecialJumping) transform.rotation = Quaternion.identity; //otherwise, it changes rotation slightly for some reason.
            isSpecialJumping = false;
            GuitarAnimator.SetBool("isOnGround", isOnGround);
            jumpLandPoof.Play();
            isJumping = false;
            isFalling = false;            
            //GuitarAnimator.Play("GuitarChar_SquashRunning");
            //GuitarAnimator.Play("GuitarChar_Running");
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameManager.EndGame();
        }
    }


}
