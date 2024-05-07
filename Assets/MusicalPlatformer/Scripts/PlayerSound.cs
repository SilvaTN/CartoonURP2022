using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    //[SerializeField] AudioSource srcWrongSounds;
    //[SerializeField] AudioClip[] wrongSounds;
    [SerializeField] AudioSource wrongSound;
    [SerializeField] AudioSource srcGuitar;
    //[SerializeField] AudioClip sound1, sound2, sound3, sound4;
    private KeyCode correctKeyCode;
    private Collider noteTouched;

    // Start is called before the first frame update
    void Start()
    {
        correctKeyCode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (KeyCode.LeftArrow == correctKeyCode)
            {
                srcGuitar.mute = false;
                Destroy(noteTouched.gameObject);                
                Debug.Log("we are playing the CORRECT note yayayayay");
            } else
            {
                srcGuitar.mute = true;
                wrongSound.Play();
                //int soundIndex = Random.Range(0, 4); //which of the four wrongSounds to play
                //srcWrongSounds.PlayOneShot(wrongSounds[soundIndex]); //play Xth sound
                Debug.Log("we are playing the WRONG note nnnooooooo");
                if (noteTouched)
                {
                    Destroy(noteTouched.gameObject);
                }
            }
        } else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (KeyCode.DownArrow == correctKeyCode)
            {
                srcGuitar.mute = false;
                Destroy(noteTouched.gameObject);
                Debug.Log("we are playing the CORRECT note yayayayay");
            }
            else
            {
                srcGuitar.mute = true;
                wrongSound.Play();
                //int soundIndex = Random.Range(0, 4); //which of the four wrongSounds to play
                //srcWrongSounds.PlayOneShot(wrongSounds[soundIndex]); //play Xth sound
                Debug.Log("we are playing the WRONG note nnnooooooo");
                if (noteTouched)
                {
                    Destroy(noteTouched.gameObject);
                }
            }
        } else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (KeyCode.RightArrow == correctKeyCode)
            {
                srcGuitar.mute = false;
                Destroy(noteTouched.gameObject);
                Debug.Log("we are playing the CORRECT note yayayayay");
            }
            else
            {
                srcGuitar.mute = true;
                wrongSound.Play();
                //int soundIndex = Random.Range(0, 4); //which of the four wrongSounds to play
                //srcWrongSounds.PlayOneShot(wrongSounds[soundIndex]); //play Xth sound
                Debug.Log("we are playing the WRONG note nnnooooooo");
                if (noteTouched)
                {
                    Destroy(noteTouched.gameObject);
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        noteTouched = other;
        if (other.CompareTag("LeftNote"))
        {
            Debug.Log("Touching left uwuwuwuwuwu");
            correctKeyCode = KeyCode.LeftArrow;            
        }
        if (other.CompareTag("DownNote"))
        {
            Debug.Log("Touching down qqqqqqqq");
            correctKeyCode = KeyCode.DownArrow;
        }
        if (other.CompareTag("RightNote"))
        {
            Debug.Log("Touching right jjjjjjjj");
            correctKeyCode = KeyCode.RightArrow;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        srcGuitar.mute = true; //if you run past note and don't play it, mute the correct guitar track.
        correctKeyCode = 0;
    }


}
