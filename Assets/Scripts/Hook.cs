using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private float hookForce = 25f;

    private Grapple grapple;
    private Rigidbody rigidbody;
    private LineRenderer lineRenderer;

    public void Initialize(Grapple grapple, Transform shootTransform)
    {
        transform.forward = shootTransform.forward;
        this.grapple = grapple;
        rigidbody = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        rigidbody.AddForce(transform.forward * hookForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] positions = new Vector3[]
        {
            transform.position,
            grapple.transform.position
        };
        
        lineRenderer.SetPositions(positions);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((LayerMask.GetMask("Grapple") & 1 << other.gameObject.layer) > 0)
        {
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;

            grapple.StartPull();
        }
    }
}
