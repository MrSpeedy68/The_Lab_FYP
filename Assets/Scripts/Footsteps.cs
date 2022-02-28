using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private CharacterController _characterController;
    private AudioSource _audio;
    private ContinuousMovement _continuousMovement;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _audio = GetComponent<AudioSource>();
        _continuousMovement = GetComponent<ContinuousMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_continuousMovement.isGrounded && _characterController.velocity.magnitude > 2f &&
            _audio.isPlaying == false)
        {
            _audio.volume = Random.Range(0.1f, 0.3f);
            _audio.pitch = Random.Range(0.8f, 1f);
            _audio.Play();
        } 
    }
}
