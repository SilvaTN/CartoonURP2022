using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeJumpedOn : MonoBehaviour
{
    [SerializeField] Animator treeAnimator;
    private const string BOUNCE_ANIM = "TreeBounceAnimation";
    [SerializeField] ParticleSystem psPetals;

    private void OnTriggerEnter(Collider other)
    {
        treeAnimator.Play(BOUNCE_ANIM);
        psPetals.Play();
    }
}
