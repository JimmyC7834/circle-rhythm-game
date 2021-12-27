using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class JudgementIconAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private InputReader input;

    private void Awake()
    {
        input.dotHitEvent += PlayHitAnimation;
        input.circleHitEvent += PlayHitAnimation;
    }

    private void PlayHitAnimation()
    {
        animator.Play("HitAnim");
    }
}
