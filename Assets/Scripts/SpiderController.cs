using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class SpiderController : MonoBehaviour
{
    public float speed;
    public float angularSpeed;
    public bool isWalking = false;
    public GameObject sp;

    private Rigidbody _rigidbody;
    private Animator _anim;
    private AnimatorStateInfo _animationState;
    private Vector3 _randTarget;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        
        _anim.SetTrigger("Patrolling");
        _randTarget = transform.position;
    }

    void FixedUpdate()
    {


        _animationState = _anim.GetCurrentAnimatorStateInfo(0);

        if (_animationState.IsName("Patrolling"))
        {
            if (Vector3.Distance(transform.position, _randTarget) < 1.0f)
            {
                _randTarget = MoveToRandomWP();
            }
            // float targetDir = Vector3.Angle(_randTarget, transform.forward);
             // Debug.Log(targetDir);
            
             //Quaternion lookAtLoc = Quaternion.LookRotation(_randTarget,transform.up);
             //_rigidbody.MoveRotation(lookAtLoc);
             
             Debug.DrawLine(transform.position,_randTarget,Color.cyan);

            transform.position = Vector3.MoveTowards(transform.position, _randTarget, speed * Time.deltaTime);


        }
        
        // if (_rigidbody.velocity.magnitude < _speed)
        // {
        //     if(isWalking)
        //     {
        //         _rigidbody.AddForce(0,0, 1 * Time.fixedDeltaTime * 1000f);
        //     }
        // }
    }
    
    private Vector3 MoveToRandomWP()
    {
        Ray ray = new Ray();
        RaycastHit hit;
        ray.origin = transform.position;
        float distanceToObstacle = 0;
        float castingDistance = 20;
        do
        {
            float randomDirectionX = Random.Range(-1, 1);
            float randomDirectionZ = Random.Range(-1, 1);
            ray.direction = transform.forward * randomDirectionZ + transform.right * randomDirectionX;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, castingDistance))
            {
                distanceToObstacle = hit.distance;
            }
            else distanceToObstacle = castingDistance;
            
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            
            //Debug.Log(ray.origin + ray.direction * (distanceToObstacle - 1));
            return ray.origin + ray.direction * (distanceToObstacle - 1);
            
        } while (distanceToObstacle < 1.0f);
    }

    public int rayAmount = 6;

    // private void FindNewWP()
    // {
    //     Ray ray = new Ray();
    //     RaycastHit hit;
    //     ray.origin = transform.position;
    //     float castingDistance = 20;
    //     ray.direction = transform.forward;
    //     for (int i = 0; i < rayAmount; i++)
    //     {
    //         ray.direction += transform.right * 60;
    //         Physics.Raycast(ray.origin, ray.direction, out hit, castingDistance);
    //         
    //         Debug.DrawRay(ray.origin, ray.direction, Color.red);
    //     }
    // }
    //
    // private void Update()
    // {
    //     FindNewWP();
    // }
}
