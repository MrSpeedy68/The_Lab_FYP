using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    [SerializeField] private float initializationDistance = 0.05f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private Vector3 _initialPosition;
    
    void Start()
    {
        _initialPosition = gameObject.transform.position;
    }
    
    void FixedUpdate()
    {
        if (gameObject.transform.position.y <= _initialPosition.y - initializationDistance)
        {
            Debug.Log("Button Pressed!!!!");
            //audioSource.PlayOneShot(audioClip);
        }
    }
}
