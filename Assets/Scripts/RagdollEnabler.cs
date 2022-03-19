using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEnabler : MonoBehaviour
{
    public float ragdollForce = 3000f;
    private Rigidbody[] rigRigidbodies;
    
    // Start is called before the first frame update
    void Start()
    {
        rigRigidbodies = GetComponentsInChildren<Rigidbody>();
        //DisableRagdoll();
    }

    public void DisableRagdoll()
    {
        foreach(Rigidbody rb in rigRigidbodies)
        {
            rb.isKinematic = true;
        }
    }
    
    public void EnableRagdoll()
    {
        foreach(Rigidbody rb in rigRigidbodies)
        {
            rb.isKinematic = false;
        }
    }

    public void ApplyRagdollForce(Vector3 hitVector, float mass)
    {
        foreach(Rigidbody rb in rigRigidbodies)
        {
            rb.AddForce(-hitVector * mass * ragdollForce);
        }
    }
}
