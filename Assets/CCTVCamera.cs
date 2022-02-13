using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVCamera : MonoBehaviour
{
    public GameObject cameraObj;
    public GameObject followObj;
    

    void FixedUpdate()
    {
        cameraObj.transform.LookAt(followObj.transform);
    }
}
