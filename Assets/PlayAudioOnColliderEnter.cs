using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnColliderEnter : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private bool isPlayed;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !isPlayed)
        {
            if (audioSource && audioClip)
            {
                audioSource.PlayOneShot(audioClip);
                isPlayed = true;
            }
        }
    }
}
