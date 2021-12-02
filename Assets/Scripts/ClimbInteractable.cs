using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractable : XRBaseInteractable
{
    private ContinuousMovement _continuousMovement;
    
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);

        if(interactor is XRDirectInteractor)
        {
            ClimbingScript.climbingHand = interactor.GetComponent<XRController>();

            _continuousMovement = interactor.GetComponentInParent<ContinuousMovement>();

            _continuousMovement.isGrounded = true;
        }    
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);

        if(interactor is XRDirectInteractor)
        {
            if(ClimbingScript.climbingHand && ClimbingScript.climbingHand.name == interactor.name)
            {
                ClimbingScript.climbingHand = null;
            }
        }
    }
    
}
