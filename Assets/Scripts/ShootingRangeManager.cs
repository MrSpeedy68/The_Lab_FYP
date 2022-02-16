using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRangeManager : MonoBehaviour
{
    [SerializeField] private List<MoveableTarget> _targets;

    private MoveAtoB _moveAtoB;
    private void Start()
    {
        _moveAtoB = _targets[0].GetComponentInParent<MoveAtoB>();
    }

    void Update()
    {
        if (_moveAtoB.isMoving)
        {
            int i = 0;
        
            foreach (var t in _targets)
            {
                if (t.isHit)
                {
                    i++;
                }

                if (i == _targets.Count)
                {
                    foreach (var t1 in _targets)
                    {
                        var move = t1.GetComponentInParent<MoveAtoB>();
                        move.isMoving = false;
                        move.speed = 7.0f;
                        var animator = t1.GetComponentInParent<Animator>();
                        animator.SetTrigger("ReturnTarget");
                        t1.isHit = false;
                    }
                }
            }
        }
    }
}
