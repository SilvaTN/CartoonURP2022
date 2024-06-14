using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    //[SerializeField] AudioSource srcWrongSounds;
    //[SerializeField] AudioClip[] wrongSounds;
    [SerializeField] AudioSource wrongSound;
    [SerializeField] AudioSource srcGuitar;
    [SerializeField] ParticleSystem trailNote;
    [SerializeField] ParticleSystem noteDissipationCorrect;
    [SerializeField] ParticleSystem noteDissipationWrong;
    //[SerializeField] AudioClip sound1, sound2, sound3, sound4;
    private float charSpeed;
    private float lastPos;
    private int testCount = 0;
    private bool pastInitialCount = false;
    private int numberOfTimesSpeedCalculated = 0;
    private float totalSpeed = 0;
    private float minimumSpeed = 20;
    private float maxSpeed = -30;
    private KeyCode correctKeyCode;
    private Collider noteTouched;

    private void CorrectNotePressed()
    {
        srcGuitar.mute = false;
        noteDissipationCorrect.Play();
        Destroy(noteTouched.gameObject);
        trailNote.Play();
        correctKeyCode = 0; //Resets the correctKeyCode after you play correctly.
    }

    private void WrongNotePressed()
    {
        srcGuitar.mute = true;     
        wrongSound.Play();
        //int soundIndex = Random.Range(0, 4); //which of the four wrongSounds to play
        //srcWrongSounds.PlayOneShot(wrongSounds[soundIndex]); //play Xth sound
        if (noteTouched)
        {
            noteDissipationWrong.Play();
            Destroy(noteTouched.gameObject);
            correctKeyCode = 0; //Resets the correctKeyCode after playing incorrectly.
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        correctKeyCode = 0;
        lastPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
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
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (KeyCode.LeftArrow == correctKeyCode)
            {
                CorrectNotePressed();
            } else
            {
                WrongNotePressed();
            }
        } else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (KeyCode.DownArrow == correctKeyCode)
            {
                CorrectNotePressed();
            }
            else
            {
                WrongNotePressed();
            }
        } else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (KeyCode.RightArrow == correctKeyCode)
            {
                CorrectNotePressed();
            }
            else
            {
                WrongNotePressed();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        noteTouched = other;
        if (other.CompareTag("LeftNote"))
        {
            //Debug.Log("Touching left uwuwuwuwuwu");
            correctKeyCode = KeyCode.LeftArrow;            
        }
        if (other.CompareTag("DownNote"))
        {
            //Debug.Log("Touching down qqqqqqqq");
            correctKeyCode = KeyCode.DownArrow;
        }
        if (other.CompareTag("RightNote"))
        {
            //Debug.Log("Touching right jjjjjjjj");
            correctKeyCode = KeyCode.RightArrow;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //noteTouched = null;
        srcGuitar.mute = true; //if you run past note and don't play it, mute the correct guitar track.
        correctKeyCode = 0;
    }


}
