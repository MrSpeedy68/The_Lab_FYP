using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEnabler : MonoBehaviour
{
    public float ragdollForce = 3000f;
    [SerializeField] private float maxHealth;
    public HealthBar healthBar;


    public float currHealth;
    private Rigidbody[] rigRigidbodies;
    // Start is called before the first frame update
    void Start()
    {
        rigRigidbodies = GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
        currHealth = maxHealth;
    }
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Weapon") && other.relativeVelocity.magnitude > 1f)
        {
            print("Collision Entered with Ragdoll");
            float damageDealt = other.relativeVelocity.magnitude * other.rigidbody.mass;
            TakeDamage(damageDealt);

            if (currHealth <= 0f)
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
                EnableRagdoll();

                Vector3 hitVector = (other.transform.position - transform.position).normalized;

                foreach(Rigidbody rb in rigRigidbodies)
                {
                    rb.AddForce(-hitVector * other.rigidbody.mass * ragdollForce);
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (currHealth > 0)
        {
            currHealth -= damage;
            healthBar.UpdateHealth(currHealth / maxHealth);
        }
        else EnableRagdoll();
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


}
