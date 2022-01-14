using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorGrabbable : XRGrabInteractable
{

    public Transform handler;

    protected override void OnSelectExiting(XRBaseInteractor interactor)
    {
        base.OnSelectExiting(interactor);
        
        transform.position = handler.transform.position;
        transform.rotation = handler.transform.rotation;

        Rigidbody rbHandler = handler.GetComponent<Rigidbody>();
        rbHandler.velocity = Vector3.zero;
        rbHandler.angularVelocity = Vector3.zero;
    }

    private void Update()
    {
        if (Vector3.Distance(handler.position, transform.position) > 0.5f)
        {
            throwOnDetach = true;
        }
    }

}
