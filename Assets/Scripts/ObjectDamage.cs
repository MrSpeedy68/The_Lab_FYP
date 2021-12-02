using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDamage : MonoBehaviour
{
    public float objectHealth = 100f;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Weapon") && other.relativeVelocity.magnitude > 2f && objectHealth > 0f)
        {
            float damageDealt = other.relativeVelocity.magnitude * other.rigidbody.mass;
            print("Damage Dealt = " + damageDealt);
            objectHealth -= damageDealt;
            if (objectHealth <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
