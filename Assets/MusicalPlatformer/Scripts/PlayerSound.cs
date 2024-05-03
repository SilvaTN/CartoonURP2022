using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] AudioSource src;
    //[SerializeField] AudioClip sound1, sound2, sound3, sound4;
    [SerializeField] AudioClip[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            //src.clip = sound1;
            //src.Play();
            //src.PlayOneShot(sound1);
            int soundIndex = Random.Range(0, 4); //which of the four sounds to play
            src.PlayOneShot(sounds[soundIndex]); //play Xth sound
        }
    }
}
