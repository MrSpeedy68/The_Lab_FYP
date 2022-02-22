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
    
    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
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
    
}
