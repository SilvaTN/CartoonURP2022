using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovementScript;
    //[SerializeField] AudioSource srcWrongSounds;
    //[SerializeField] AudioClip[] wrongSounds;
    [SerializeField] AudioSource wrongSound;
    [SerializeField] AudioSource srcGuitar;
    [SerializeField] ParticleSystem noteDissipationCorrect;
    [SerializeField] ParticleSystem rainbowPathPS;
    [SerializeField] GameObject rainbowPathCollider;
    [SerializeField] ParticleSystem rainbowJumpSwirl;
    [SerializeField] ParticleSystem glowInsideGuitar;
    [SerializeField] ParticleSystem noteDissipationWrong;
    [SerializeField] Material guitarBodyMaterial;
    [SerializeField] Animator eyeAnimator;
    [SerializeField] float rotationSpeed;
    [SerializeField] float noteSizeScaleFactor = 1.4f;
    private bool isRainbow;

    private Vector3 noteSizeOriginalScale;
    //[SerializeField] AudioClip sound1, sound2, sound3, sound4;    
    private KeyCode correctKeyCode;
    //private KeyCode specialKeyCodeIsPrevCorrect; //special note
    private Collider noteTouched;

    private void CorrectNotePressed()
    {
        srcGuitar.mute = false;
        noteDissipationCorrect.Play();
        if (isRainbow)
        {
            playerMovementScript.upwardsThrust(isRainbow);
            rainbowJumpSwirl.Play();
            rainbowPathPS.Play();
            rainbowPathCollider.SetActive(true);
        } else
        {
            glowInsideGuitar.Play();
        }
        Destroy(noteTouched.gameObject);
        isRainbow = false;
        //specialKeyCodeIsPrevCorrect = correctKeyCode;
        correctKeyCode = 0; //Resets the correctKeyCode after you play correctly.
    }

    private void WrongNotePressed()
    {
        srcGuitar.mute = true;     
        wrongSound.Play();
        noteDissipationWrong.Play();
        eyeAnimator.Play("EyelidAnimation");
        if (guitarBodyMaterial != null && guitarBodyMaterial.HasProperty("_LerpRegularVsPolka"))
        {            
            guitarBodyMaterial.SetFloat("_LerpRegularVsPolka", 0.5f);
        }
        else
        {
            Debug.LogError("Material is not assigned or does not have the property '_LerpRegularVsPolka'.");
        }
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {        
        //Wait for X seconds
        yield return new WaitForSeconds(0.2f);        
        guitarBodyMaterial.SetFloat("_LerpRegularVsPolka", 1f);
    }

    private bool NotePressedAction(KeyCode keyPressed)
    {
        if (Input.GetKeyDown(keyPressed))
        {
            if ((keyPressed == correctKeyCode) || (isRainbow))
            {
                CorrectNotePressed();
            }
            else
            {
                WrongNotePressed();
            }
            return true;
        } else
        {
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isRainbow = false;
        if (rotationSpeed == 0)
        {
            rotationSpeed = 900f;
        }
        correctKeyCode = 0;        
    }

    // Update is called once per frame
    void Update()
    {
        //maybe replace these with switch statements bc it looks more clean for when you dont wanna do nothing inside.
        if (NotePressedAction(KeyCode.Period))
        {
            //do nothing
        } else if (NotePressedAction(KeyCode.Comma))
        {
            //do nothing
        }
        else if (NotePressedAction(KeyCode.M))
        {
            //do nothing
        }
        else if (NotePressedAction(KeyCode.N))
        {
            //do nothing
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        noteSizeOriginalScale = other.transform.localScale;
        noteTouched = other;
        other.transform.localScale *= noteSizeScaleFactor;

        if (other.CompareTag("NoteV"))
        {
            //Debug.Log("Touching left uwuwuwuwuwu");
            correctKeyCode = KeyCode.Period;
            isRainbow = false;
        }
        if (other.CompareTag("NoteC"))
        {
            //Debug.Log("Touching down qqqqqqqq");
            correctKeyCode = KeyCode.Comma;
            isRainbow = false;
        }
        if (other.CompareTag("NoteX"))
        {
            //Debug.Log("Touching right jjjjjjjj");
            correctKeyCode = KeyCode.M;
            isRainbow = false;
        }
        if (other.CompareTag("NoteZ"))
        {
            //Debug.Log("Touching right jjjjjjjj");
            correctKeyCode = KeyCode.N;
            isRainbow = false;
        }
        if (other.CompareTag("NoteSpecial"))
        {
            //Debug.Log("Touching right jjjjjjjj");
            //correctKeyCode = specialKeyCodeIsPrevCorrect;
            isRainbow = true;
        }


        //other.transform.localScale *= 0.8f;
        //Renderer renderer = other.GetComponent<Renderer>();
        //if (renderer != null)
        //{

        // Find the property ID of SpinAmount
        //int spinAmountPropertyID = Shader.PropertyToID("_RotationSpeed");

        // Set the SpinAmount property of the material to 360
        //renderer.material.SetFloat(spinAmountPropertyID, 660f);
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        //noteTouched = null;
        other.transform.localScale = noteSizeOriginalScale;
        srcGuitar.mute = true; //if you run past note and don't play it, mute the correct guitar track.
        //specialKeyCodeIsPrevCorrect = correctKeyCode;
        correctKeyCode = 0;
    }


}



//This below calculates speed of char.
/* put this in the declarations at the top
    private float charSpeed;
    private float lastPos;
    private int testCount = 0;
    private bool pastInitialCount = false;
    private int numberOfTimesSpeedCalculated = 0;
    private float totalSpeed = 0;
    private float minimumSpeed = 20;
    private float maxSpeed = -30;
    /*

// put this in Start()
    lastPos = transform.position.x;

/* put this inside Update()
        testCount++;
        charSpeed = (transform.position.x - lastPos) / Time.deltaTime;
        lastPos = transform.position.x;
        numberOfTimesSpeedCalculated++;
        totalSpeed += charSpeed;
        if (pastInitialCount && (charSpeed < minimumSpeed))
        {
            minimumSpeed = charSpeed;
            Debug.Log("minimum speed changed to: " + minimumSpeed);
        }
        if (pastInitialCount && (charSpeed > maxSpeed))
        {
            maxSpeed = charSpeed;
            Debug.Log("max speed changed to: " + maxSpeed);
        }
        if (testCount == 30)
        {            
            Debug.Log("The currect speed is: " + charSpeed + " and average speed is: " + 
                totalSpeed/numberOfTimesSpeedCalculated);
            testCount = 0;
            pastInitialCount = true;
        }*/
