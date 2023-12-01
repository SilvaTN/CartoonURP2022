using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    bool canStartAttack;
    bool canStartVfx;
    string[] allAttacks = new string[] { "MagicHealJammo", "MagicHealJammo", "Standing2HMagicAttack04Jammo",
        "FrisbeeJammo", "SoccerHeaderJammo", "Standing2HMagicAttack01", "CastingSpell", "Standing2HMagicAttack05" };
    int currentAttackIndex;
    [SerializeField] ParticleSystem vfxCurrentAttack; //TURN INTO ARRAY LIKE allAttacks
    //bool wasFirstSpacePressRegistered;
    //bool vfxIsLocked;
    //bool isAlreadyPlayingVfx;
    int idleNumberOfTimes;
    int attackNumberOfTimes;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        canStartAttack = false;
        canStartVfx = false;
        currentAttackIndex = 0;
        //vfxIsLocked = true;
        //isAlreadyPlayingVfx = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool("IsRunning");
        bool forwardPressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");

        if (Input.GetKeyDown(KeyCode.Alpha0)) currentAttackIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentAttackIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentAttackIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentAttackIndex = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4)) currentAttackIndex = 4;
        if (Input.GetKeyDown(KeyCode.Alpha5)) currentAttackIndex = 5;
        if (Input.GetKeyDown(KeyCode.Alpha6)) currentAttackIndex = 6;
        if (Input.GetKeyDown(KeyCode.Alpha7)) currentAttackIndex = 7;

        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("IdleJammo"))
        {
            idleNumberOfTimes++;
            attackNumberOfTimes = 0;
            canStartAttack = false;
            //isAlreadyPlayingVfx = false;
            if (idleNumberOfTimes == 1)
            {
                Debug.Log("First time entering Idle after atkAnim");
            } else
            {
                idleNumberOfTimes = 2; //make sure it doesn't loop around to 1
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            //if (vfxIsLocked == false && isAlreadyPlayingVfx == false)
            //{
            //    vfxCurrentAttack.Play();
            //}
            //isAlreadyPlayingVfx = true;
            //vfxIsLocked = true;
            canStartAttack = true;
        }

        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    vfxIsLocked = false;
        //}

        if (canStartAttack)
        {
            animator.SetBool("Is" + allAttacks[currentAttackIndex], true);            
        }

        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName(allAttacks[currentAttackIndex]))
        {            
            attackNumberOfTimes++;
            idleNumberOfTimes = 0;
            //isAlreadyPlayingVfx = true;
            animator.SetBool("Is" + allAttacks[currentAttackIndex], false);
            if (attackNumberOfTimes == 1)
            {
                vfxCurrentAttack.Play();
                Debug.Log("First time entering Attack after idlAnim");
            }
            else
            {
                attackNumberOfTimes = 2; //make sure it doesn't loop around to 1
            }
        }


        if (!isRunning && forwardPressed)
        {
            animator.SetBool("IsRunning", true);
        }

        if (isRunning && !forwardPressed)
        {
            animator.SetBool("IsRunning", false);
        }

    }
}
