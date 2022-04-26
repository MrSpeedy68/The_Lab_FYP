using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("isPressurePlateOn", false);
    }


    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("PressureObj"))
        {
            anim.SetBool("isPressurePlateOn", true);
        }
        else anim.SetBool("isPressurePlateOn", false);
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("PressureObj"))
        {
            anim.SetBool("isPressurePlateOn", false);
        }
        else anim.SetBool("isPressurePlateOn", false);
    }
}
