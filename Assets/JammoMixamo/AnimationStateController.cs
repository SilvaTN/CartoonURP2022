using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    bool canStartAttack;
    string[] allAttacks = new string[] { "MagicHealJammo", "MagicHealJammo", "Standing2HMagicAttack04Jammo",
        "FrisbeeJammo", "SoccerHeaderJammo", "Standing2HMagicAttack01", "CastingSpell", "Standing2HMagicAttack05" };
    int currentAttackIndex;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        canStartAttack = false;
        currentAttackIndex = 0;
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
            canStartAttack = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            canStartAttack = true;
        }

        if (canStartAttack)
        {
            animator.SetBool("Is" + allAttacks[currentAttackIndex], true);
        }
        //bool isMmaKickTrue = animator.GetBool("IsMmaKick");

        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName(allAttacks[currentAttackIndex]))
        {
            animator.SetBool("Is" + allAttacks[currentAttackIndex], false);
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
