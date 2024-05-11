using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailMusicSheet : MonoBehaviour
{
    [SerializeField] TrailRenderer trail;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //gets called by PlayerMovement script
    public void JumpTrail()
    {
        Debug.Log("TrailMusicSheet: JumpTrail() hurrayyyyy");
    }

    public void GlowTrail()
    {
        Debug.Log("TrailMusicSheet: GlowTrail() oooooooooo");
    }

    public void WrongTrail()
    {
        Debug.Log("TrailMusicSheet: WrongTrail() wopwopwopwopwwopwop");
    }
}
