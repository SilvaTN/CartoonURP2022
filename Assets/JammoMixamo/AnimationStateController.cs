using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    bool canStartAttack;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        canStartAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool("IsRunning");
        bool forwardPressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");

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
            animator.SetBool("IsMagicHeal", true);
        }
        //bool isMmaKickTrue = animator.GetBool("IsMmaKick");

        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("MagicHealJammo"))
        {
            animator.SetBool("IsMagicHeal", false);
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
