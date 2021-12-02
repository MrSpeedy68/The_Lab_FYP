using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEnabler : MonoBehaviour
{
    private Rigidbody[] rigRigidbodies;
    // Start is called before the first frame update
    void Start()
    {
        rigRigidbodies = GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
    }
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Weapon") && other.relativeVelocity.magnitude > 2f)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            print("Collision Entered with Ragdoll");
            EnableRagdoll();

            Vector3 hitVector = (other.transform.position - transform.position).normalized;

            foreach(Rigidbody rb in rigRigidbodies)
            {
                rb.AddForce(hitVector * other.rigidbody.mass * 4000);
            }
        }
    }

    private void DisableRagdoll()
    {
        foreach(Rigidbody rb in rigRigidbodies)
        {
            rb.isKinematic = true;
        }
    }
    
    private void EnableRagdoll()
    {
        foreach(Rigidbody rb in rigRigidbodies)
        {
            rb.isKinematic = false;
        }
    }

    IEnumerator enableAfter()
    {
        EnableRagdoll();

        yield return null;
    }
}
