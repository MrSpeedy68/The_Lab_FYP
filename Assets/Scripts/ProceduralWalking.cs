using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralWalking : MonoBehaviour
{
    public Transform frontR;
    public Transform frontL;
    public Transform backR;
    public Transform backL;

    public float footSpacing = 3f;
    public float stepDistance = 5f;

    private Vector3 currentPosition;
    private Vector3 newPosition = new Vector3(0,0,0);
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = currentPosition;
        float step = 100.0f * Time.deltaTime;

        Ray ray = new Ray(transform.position + (transform.right * footSpacing), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit info, 10))
        {
            if (Vector3.Distance(newPosition, info.point) > stepDistance)
            {
                newPosition = info.point;
                backL.position = Vector3.MoveTowards(backL.position, newPosition, step);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPosition, 0.5f);
    }
}
