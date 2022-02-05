using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    public float _speed = 1f;
    public bool isWalking = false;
    
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (_rigidbody.velocity.magnitude < _speed)
        {
            if(isWalking)
            {
                _rigidbody.AddForce(0,0, 1 * Time.fixedDeltaTime * 1000f);
            }
        }
    }
}
