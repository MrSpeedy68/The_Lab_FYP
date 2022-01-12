using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    [SerializeField] private bool isGrappling = false;
    [SerializeField] private bool isShooting = false;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float maxGrappleDistance = 30f;
    [SerializeField] private float hookSpeed = 1f;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform playerBody;
    [SerializeField] private ContinuousMovement continuousMovement;

    private Vector3 _hookPoint; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrappling)
        {
            controller.enabled = false;
            continuousMovement.isGrounded = true;
            continuousMovement.isOffGround = true;
            continuousMovement.enabled = false;
            playerBody.position = Vector3.Lerp(playerBody.position, _hookPoint, hookSpeed * Time.deltaTime);
            if (Vector3.Distance(playerBody.position, _hookPoint) < 0.5f)
            {
                controller.enabled = true;
                isGrappling = false;
                continuousMovement.enabled = true;
                continuousMovement.isGrounded = true;
                continuousMovement.isOffGround = false;
                continuousMovement.ResetGravity();
            }
        }
    }

    private void LateUpdate()
    {
        if (lineRenderer.enabled)
        {
            lineRenderer.SetPosition(0, shootPoint.position);
            lineRenderer.SetPosition(1, _hookPoint);
        }
    }

    public void ShootHook()
    {
        if (isGrappling || isShooting) return;

        isShooting = true;
        RaycastHit hit;
        Ray ray = new Ray(shootPoint.position, shootPoint.forward);
        if (Physics.Raycast(ray, out hit, maxGrappleDistance, grappleLayer))
        {
            Debug.Log("Hit Grapple Layer");
            _hookPoint = hit.point;
            isGrappling = true;
            lineRenderer.enabled = true;
        }

        isShooting = false;
    }

    public void Released()
    {
        Debug.Log("Released");
        isGrappling = false;
        isShooting = false;
        lineRenderer.enabled = false;

        controller.enabled = true;
        continuousMovement.enabled = true;
        continuousMovement.isGrounded = true;
        continuousMovement.isOffGround = false;
        continuousMovement.ResetGravity();
    }
    
    
}
