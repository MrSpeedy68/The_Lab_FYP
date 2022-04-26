using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetActivatedDoor : MonoBehaviour
{
    [SerializeField] private List<MoveableTarget> _targets;
    private Animator _anim;
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTargets();
    }

    void CheckTargets()
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
                _anim.SetTrigger("openDoor");
            }
        }
    }
}
