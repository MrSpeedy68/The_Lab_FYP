using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    [SerializeField] private float initializationDistance = 0.05f;
    
    private bool _isPressed = false;
    private Vector3 _initialPosition;
    
    void Start()
    {
        _initialPosition = gameObject.transform.position;
    }
    
    void FixedUpdate()
    {
        if (gameObject.transform.position.y <= _initialPosition.y - initializationDistance)
        {
            if (!_isPressed)
            {
                _isPressed = !_isPressed;
                
                OnButtonActive();

                StartCoroutine("PressCooldown");
            }

        }
    }

    IEnumerator PressCooldown()
    {
        yield return new WaitForSeconds(1f);
        
        _isPressed = !_isPressed;
    }

    protected virtual void OnButtonActive()
    {
        
    }
}
