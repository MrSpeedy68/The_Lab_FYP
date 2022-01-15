using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunComponent : MonoBehaviour
{

    [SerializeField] private Transform magAttachPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Magazine"))
        {
            other.transform.position = magAttachPoint.position;
            other.attachedRigidbody.isKinematic = true;
            other.transform.parent = gameObject.transform;
        }
    }
}
