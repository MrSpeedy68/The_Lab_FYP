using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class SpiderController : MonoBehaviour
{
    public float speed;
    public bool isWalking = false;
    public int rayAmount;
    public float castingDistance;

    private Rigidbody _rigidbody;
    private Animator _anim;
    private AnimatorStateInfo _animationState;
    private Vector3 _randTarget;
    [SerializeField] private GameObject _player;
    
    
    [Header("Detection Settings")]
    public float hearingDistance = 10f;
    public float sightDistance = 30f;

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
        
        Sight();
        Listen();
        
        

        if (_animationState.IsName("Patrolling"))
        {
             if (Vector3.Distance(transform.position, _randTarget) < 0.5f)
             {
                 _randTarget = MoveToRandomWP();
             }
             //float targetDir = Vector3.Angle(_randTarget, transform.forward);
             //Debug.Log(targetDir);
            
             //Quaternion lookAtLoc = Quaternion.LookRotation(_randTarget,transform.up);
             //_rigidbody.MoveRotation(lookAtLoc);
             
             
            Debug.DrawLine(transform.position,_randTarget,Color.cyan);

            transform.position = Vector3.MoveTowards(transform.position, _randTarget, speed * Time.deltaTime);
        }

        if (_animationState.IsName("FollowPlayer"))
        {
            _randTarget = _player.transform.position;

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
        float distanceToObstacle = 0;
        List<Vector3> returnDir = new List<Vector3>();
        Ray returnedRay = new Ray();
        Random.InitState(Mathf.CeilToInt(Time.time));
        
        for (int i=0; i<4; i++)
        {
            
            RaycastHit hit;
            Ray ray = new Ray();

            switch (i)
            {
                case 0:
                    ray.direction = Vector3.forward;
                    break;
                case 1:
                    ray.direction = Vector3.back;
                    break;
                case 2:
                    ray.direction = Vector3.left;
                    break;
                case 3:
                    ray.direction = Vector3.right;
                    break;
            }
            
            ray.origin = transform.position;
            
            Debug.DrawRay(ray.origin,ray.direction,Color.red,30f);
            
            if (Physics.Raycast (ray, out hit)) {

                Debug.Log(i + " " + hit.distance);
                
                if (hit.distance > 1.5f)
                {
                    distanceToObstacle = hit.distance;
                    returnDir.Add(ray.origin + ray.direction * (distanceToObstacle - 1)); 
                }
            }
        }
        
        Debug.Log(returnDir.Count);

        Vector3 returnedVector = returnDir[Random.Range(0,returnDir.Count)];
        return returnedVector;
    }
    
    private void Sight()
    {
        Ray ray = new Ray();
        RaycastHit hit;
        ray.origin = transform.position + Vector3.up;
        string objectInSight;

        ray.direction = transform.forward * sightDistance;
        Debug.DrawRay(ray.origin, ray.direction * sightDistance, Color.red);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, sightDistance))
        {
            objectInSight = hit.collider.gameObject.name;
            if (objectInSight == "XR Rig")
            {
                _anim.SetBool("canSeePlayer",true);
            }
            else
            {
                _anim.SetBool("canSeePlayer",false);
            }
        }
        else _anim.SetBool("canSeePlayer",false);
    }

    private void Listen()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance <= hearingDistance)
        {
            _anim.SetBool("canHearPlayer",true);
        }
        else _anim.SetBool("canHearPlayer",false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,hearingDistance/2);
    }
    
    // private Vector3 MoveToRandomWP()
    // {
    //     float distanceToObstacle = 0;
    //     Vector3 returnDir = new Vector3(0,0,0);
    //     Ray returnedRay = new Ray();
    //     
    //     float angle = 0;
    //     for (int i=0; i<rayAmount; i++)
    //     {
    //
    //         float deg = angle * Mathf.Rad2Deg;
    //         
    //         float x = 100 * Mathf.Cos (deg);
    //         float z = 100 * Mathf.Sin (deg);
    //         angle += 2 * i * Mathf.PI / rayAmount;
    //
    //         RaycastHit hit;
    //         Ray ray = new Ray();
    //
    //         ray.direction = new Vector3 (transform.position.x + x, transform.position.y + 0.2f, transform.position.z + z);
    //         ray.origin = transform.position;
    //         
    //         Debug.DrawRay(ray.origin,ray.direction,Color.red,30f);
    //         
    //         if (Physics.Raycast (ray, out hit,castingDistance)) {
    //
    //             Debug.Log(i + " " + hit.distance);
    //             if (hit.distance > distanceToObstacle)
    //             {
    //                 distanceToObstacle = hit.distance;
    //                 returnDir = hit.point;
    //             }
    //         }
    //     }
    //
    //     //Vector3 newDir = new Vector3(returnDir.x, returnDir.y, returnDir.z);
    //     Debug.Log("Distance Returned" + distanceToObstacle);
    //     return returnDir;
    // }
    
    
    

    // private Vector3 MoveToRandomWP()
    // {
    //     float distanceToObstacle = 0;
    //     Vector3 returnDir = new Vector3(0,0,0);
    //     
    //     float angle = 0;
    //     for (int i=0; i<rayAmount; i++) {
    //         float x = Mathf.Cos (angle);
    //         float z = Mathf.Sin (angle);
    //         angle += 2 * Mathf.PI / rayAmount;
    //
    //         Vector3 dir = new Vector3 (transform.position.x + x*100, transform.position.y + 0.2f, transform.position.z + z*100);
    //         RaycastHit hit;
    //         Debug.DrawRay(transform.position + Vector3.up / 5, dir, Color.red,30f);
    //         
    //         if (Physics.Raycast (transform.position + Vector3.up / 5, dir, out hit, castingDistance)) {
    //
    //             Debug.Log(i + " " + hit.distance);
    //             if (hit.distance > distanceToObstacle)
    //             {
    //                 distanceToObstacle = hit.distance;
    //                 returnDir = hit.direction;
    //             }
    //         }
    //     }
    //
    //     //Vector3 newDir = new Vector3(returnDir.x, returnDir.y, returnDir.z);
    //
    //     return returnDir - (Vector3.forward);
    // }
    
    // private Vector3 MoveToRandomWP()
    // {
    //     Ray ray = new Ray();
    //     RaycastHit hit;
    //     ray.origin = transform.position;
    //     float distanceToObstacle = 0;
    //     float castingDistance = 20;
    //     do
    //     {
    //         float randomDirectionX = Random.Range(-1, 1);
    //         float randomDirectionZ = Random.Range(-1, 1);
    //         ray.direction = transform.forward * randomDirectionZ + transform.right * randomDirectionX;
    //         if (Physics.Raycast(ray.origin, ray.direction, out hit, castingDistance))
    //         {
    //             distanceToObstacle = hit.distance;
    //         }
    //         else distanceToObstacle = castingDistance;
    //         
    //         Debug.DrawRay(ray.origin, ray.direction, Color.red);
    //         
    //         //Debug.Log(ray.origin + ray.direction * (distanceToObstacle - 1));
    //         return ray.origin + ray.direction * (distanceToObstacle - 1);
    //         
    //     } while (distanceToObstacle < 1.0f);
    // }


}
