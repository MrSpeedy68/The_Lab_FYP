using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MeleeWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if(rb != null) rb.maxAngularVelocity = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
