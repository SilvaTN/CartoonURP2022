using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovementScript;
    [SerializeField] UpdatingUI updatingUIScript;
    //[SerializeField] AudioSource srcWrongSounds;
    //[SerializeField] AudioClip[] wrongSounds;
    [SerializeField] AudioSource wrongSound;
    [SerializeField] AudioSource goldNoteSound;
    [SerializeField] AudioSource srcGuitar;
    [SerializeField] AudioSource srcGuitarHarmony;
    [SerializeField] ParticleSystem noteDissipationCorrect;
    [SerializeField] ParticleSystem noteDissipationGold;
    [SerializeField] float timeBeforeUpdatingSpecialUI = 0.2f;
    [SerializeField] ParticleSystem rainbowPathPS;
    [SerializeField] ParticleSystem sparklesFromRainbowNote;
    [SerializeField] GameObject NoteGold;
    [SerializeField] GameObject NoteGoldNormal;
    [SerializeField] GameObject rainbowPathCollider;
    [SerializeField] ParticleSystem specialJumpSwirl;
    [SerializeField] ParticleSystem glowInsideGuitar;
    [SerializeField] ParticleSystem noteDissipationWrong;
    [SerializeField] Material guitarBodyMaterial;
    [SerializeField] [Range(0f, 1f)]  float wrongPatternSoftness;
    [SerializeField] Animator eyeAnimator;
    [SerializeField] float rotationSpeed;
    [SerializeField] float noteSizeScaleFactor = 1.4f;
    [SerializeField] private float correctNoteShrinkSpeed = 500f;
    [SerializeField] private float correctNoteMinSize = 20f;
    private bool isInsideRainbowPathTrigger;
    private bool isRainbow;
    private int numofRainbowNotesCorrect;
    private bool isGold;
    

    private Vector3 noteSizeOriginalScale;
    //[SerializeField] AudioClip sound1, sound2, sound3, sound4;    
    private KeyCode correctKeyCode;
    //private KeyCode specialKeyCodeIsPrevCorrect; //special note
    private Collider noteTouched;

    private void CorrectNotePressed()
    {
        srcGuitar.mute = false;
        updatingUIScript.IncreaseLives();

        if (isRainbow)
        {
            srcGuitarHarmony.mute = false;
            noteDissipationCorrect.Play();
            playerMovementScript.upwardsThrust(isRainbow, isGold);
            specialJumpSwirl.Play();
            sparklesFromRainbowNote.Play();
            rainbowPathPS.Play();
            rainbowPathCollider.SetActive(true);
        } else if (isGold)
        {
            srcGuitarHarmony.mute = false;
            goldNoteSound.Play();
            noteDissipationGold.Play();
            playerMovementScript.upwardsThrust(isRainbow, isGold);
            specialJumpSwirl.Play();
            StartCoroutine(waitThenShowSpecialNoteUI());
        } else
        {
            noteDissipationCorrect.Play();
            glowInsideGuitar.Play();
            if (isInsideRainbowPathTrigger)
            {
                numofRainbowNotesCorrect++;
                if (numofRainbowNotesCorrect == 4)
                {
                    NoteGold.SetActive(true);
                    NoteGoldNormal.SetActive(false);
                }
            }
        }
        //Destroy(noteTouched.gameObject);
        noteTouched.enabled = false; //disable just the collider, while still keeping the note object.
        StartCoroutine(ScaleDownAndDestroy(noteTouched.gameObject));
        isRainbow = false;
        isGold = false;
        //specialKeyCodeIsPrevCorrect = correctKeyCode;
        correctKeyCode = 0; //Resets the correctKeyCode after you play correctly.
    }

    private void WrongNotePressed()
    {
        srcGuitar.mute = true;
        updatingUIScript.DecreaseLives();
        if (isRainbow || isGold)
        {
            srcGuitarHarmony.mute = true;            
        }
        wrongSound.Play();
        noteDissipationWrong.Play();
        eyeAnimator.Play("EyelidAnimation");
        if (guitarBodyMaterial != null && guitarBodyMaterial.HasProperty("_LerpRegularVsPolka"))
        {            
            guitarBodyMaterial.SetFloat("_LerpRegularVsPolka", wrongPatternSoftness);
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

    private IEnumerator ScaleDownAndDestroy(GameObject correctNote)
    {
        // Continuously scale down the object until it reaches correctNoteMinSize
        while (correctNote.transform.localScale.z > correctNoteMinSize)
        {
            correctNote.transform.localScale -= Vector3.one * correctNoteShrinkSpeed * Time.deltaTime;
            yield return null;
        }
        Debug.Log("destroying " + correctNote.tag);
        Destroy(correctNote);
    }

    private IEnumerator waitThenShowSpecialNoteUI()
    {
        //Wait for X seconds
        yield return new WaitForSeconds(timeBeforeUpdatingSpecialUI);
        updatingUIScript.specialNoteCollectedUI();
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
        isGold = false;
        isInsideRainbowPathTrigger = false;
        numofRainbowNotesCorrect = 0;
        if (rotationSpeed == 0)
        {
            rotationSpeed = 900f;
        }
        correctKeyCode = 0;        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.H)) //for testing
        //{
        //    goldNoteSound.Play();
        //    noteDissipationGold.Play();
        //}
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
        if (other.CompareTag("RainbowPathTrigger"))
        {
            isInsideRainbowPathTrigger = true;
        }
        else
        {
            noteSizeOriginalScale = other.transform.localScale;
            noteTouched = other;
            other.transform.localScale *= noteSizeScaleFactor;

            if (other.CompareTag("NoteV"))
            {
                correctKeyCode = KeyCode.Period;
                isRainbow = false;
                isGold = false;
            }
            else if (other.CompareTag("NoteC"))
            {
                correctKeyCode = KeyCode.Comma;
                isRainbow = false;
                isGold = false;
            }
            else if (other.CompareTag("NoteX"))
            {
                correctKeyCode = KeyCode.M;
                isRainbow = false;
                isGold = false;
            }
            else if (other.CompareTag("NoteZ"))
            {
                correctKeyCode = KeyCode.N;
                isRainbow = false;
                isGold = false;
            }
            else if (other.CompareTag("NoteSpecial"))
            {
                //correctKeyCode = specialKeyCodeIsPrevCorrect;
                isRainbow = true;
                isGold = false;
            }
            else if (other.CompareTag("NoteGold"))
            {
                correctKeyCode = KeyCode.Comma; //same as NoteX tag
                isRainbow = false;
                isGold = true;
            }
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
        if (!other.CompareTag("RainbowPathTrigger")) //otherwise it automatically mutes when you leave rainbow.
        {
            correctKeyCode = 0; // do not reset to 0 just bc you stopped touching rainbow.
            srcGuitar.mute = true; //if you run past note and don't play it, mute the correct guitar track.
            srcGuitarHarmony.mute = true;
            updatingUIScript.DecreaseLives();
        }
            
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
