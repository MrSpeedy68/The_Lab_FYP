using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAtoB : MonoBehaviour
{
    [SerializeField] private Transform A;
    [SerializeField] private Transform B;
    [SerializeField] private bool hitA;
    
    public bool isMoving;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (transform.position == A.position)
            {
                hitA = true; 
            }
            else if (transform.position == B.position)
            {
                hitA = false;
            }
        
            if (hitA)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, B.position, step);
            }
            else
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, A.position, step);
            }
        }
        else ReturnToPosition();
    }

    void ReturnToPosition()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, A.position, step);
    }
}
