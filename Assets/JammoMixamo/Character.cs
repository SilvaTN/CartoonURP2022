using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 720f;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("MagicHealJammo"))
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            characterController.Move(move * Time.deltaTime * speed);

            if (move != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
            // Avoid any reload.
        }

    }
}
