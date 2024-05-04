using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] AudioSource srcWrongSounds;
    [SerializeField] AudioClip[] wrongSounds;
    [SerializeField] AudioSource srcCorrectSound;
    //[SerializeField] AudioClip sound1, sound2, sound3, sound4;
    private bool isSoundProper; //only set true by MusicNote script
    private KeyCode correctKeyCode;
    private Collider noteTouched;

    // Start is called before the first frame update
    void Start()
    {
        isSoundProper = true;
        correctKeyCode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (KeyCode.LeftArrow == correctKeyCode)
            {
                srcCorrectSound.mute = false;
                Destroy(noteTouched.gameObject);                
                Debug.Log("we are playing the CORRECT note yayayayay");
            } else
            {
                srcCorrectSound.mute = true;
                int soundIndex = Random.Range(0, 4); //which of the four wrongSounds to play
                srcWrongSounds.PlayOneShot(wrongSounds[soundIndex]); //play Xth sound
                Debug.Log("we are playing the WRONG note nnnooooooo");
            }
        } else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (KeyCode.DownArrow == correctKeyCode)
            {
                srcCorrectSound.mute = false;
                Destroy(noteTouched.gameObject);
                Debug.Log("we are playing the CORRECT note yayayayay");
            }
            else
            {
                srcCorrectSound.mute = true;
                int soundIndex = Random.Range(0, 4); //which of the four wrongSounds to play
                srcWrongSounds.PlayOneShot(wrongSounds[soundIndex]); //play Xth sound
                Debug.Log("we are playing the WRONG note nnnooooooo");
            }
        } else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (KeyCode.RightArrow == correctKeyCode)
            {
                srcCorrectSound.mute = false;
                Destroy(noteTouched.gameObject);
                Debug.Log("we are playing the CORRECT note yayayayay");
            }
            else
            {
                srcCorrectSound.mute = true;
                int soundIndex = Random.Range(0, 4); //which of the four wrongSounds to play
                srcWrongSounds.PlayOneShot(wrongSounds[soundIndex]); //play Xth sound
                Debug.Log("we are playing the WRONG note nnnooooooo");
            }
        }

        /*
        if (isSoundProper)
        {
            //srcCorrectSound.mute = false;
            //Debug.Log("playing the proper sound yayayayayyaya");
        } else
        {
            srcCorrectSound.mute = true;
            //int soundIndex = Random.Range(0, 4); //which of the four wrongSounds to play
            //srcWrongSounds.PlayOneShot(wrongSounds[soundIndex]); //play Xth sound
            //srcWrongSounds.clip = sound1;
            //srcWrongSounds.Play();
            //srcWrongSounds.PlayOneShot(sound1);
        }*/
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
        srcCorrectSound.mute = true; //if you run past note and don't play it, mute the correct guitar track.
        correctKeyCode = 0;
    }


}
