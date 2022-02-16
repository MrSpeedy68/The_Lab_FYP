using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableTarget : MonoBehaviour
{
    private Animator _animator;
    
    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
    }

    public void HitByRay()
    {
        _animator.SetTrigger("TargetHit");
    }
    
}
