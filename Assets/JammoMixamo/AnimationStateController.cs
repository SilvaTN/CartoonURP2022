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
    [SerializeField] ParticleSystem vfxHand;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        canStartAttack = false;
        canStartVfx = false;
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
            canStartVfx = true;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            canStartAttack = true;
        }

        if (canStartAttack)
        {
            animator.SetBool("Is" + allAttacks[currentAttackIndex], true);
            if (canStartVfx) vfxHand.Play();
        }

        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName(allAttacks[currentAttackIndex]))
        {
            animator.SetBool("Is" + allAttacks[currentAttackIndex], false);
            canStartVfx = false;
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
