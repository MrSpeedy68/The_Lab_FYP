using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiButton : ButtonPress
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private ParticleSystem particle;

    protected override void OnButtonActive()
    {
        if (audioSource && audioClip)
        {
            audioSource.PlayOneShot(audioClip);
            particle.Play();
        }
    }
}
