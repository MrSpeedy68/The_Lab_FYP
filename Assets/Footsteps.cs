using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private CharacterController _characterController;
    private AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_characterController.isGrounded == true && _characterController.velocity.magnitude > 2f &&
            _audio.isPlaying == false)
        {
            _audio.volume = Random.Range(0.6f, 1f);
            _audio.pitch = Random.Range(0.8f, 1.1f);
            _audio.Play();
        } 
    }
}
