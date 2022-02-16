using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_FiringRange : ButtonPress
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private List<MoveAtoB> targets;
    
    protected override void OnButtonActive()
    {
        if (audioSource && audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }

        foreach (var t in targets)
        {
            t.isMoving = true;
            t.speed = Random.Range(1, 10);
        }
    }
}
