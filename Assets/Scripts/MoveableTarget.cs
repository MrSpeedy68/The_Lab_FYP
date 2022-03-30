using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableTarget : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    
    private Animator _animator;
    public bool isHit;
    private AnimatorStateInfo _animationState;
    
    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
       _animationState = _animator.GetCurrentAnimatorStateInfo(0);

       if (_animationState.IsName("MoveableShootingTargetReturn"))
       {
           ResetState();
       }
    }

    public void HitByRay()
    {
        if (audioSource && audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }

        isHit = true;
        _animator.SetBool("TargetHit", isHit);
    }

    private void ResetState()
    {
        isHit = false;
        _animator.SetBool("TargetHit", isHit);
    }
    
}
