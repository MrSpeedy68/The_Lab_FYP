using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunComponent : MonoBehaviour
{

    public float speed = 60f;
    public GameObject bullet;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public GameObject magazineSocket;

    private MagazineComponent _magazineComponent;
    public void Fire()
    {
        _magazineComponent = magazineSocket.GetComponentInChildren<MagazineComponent>();

        if (_magazineComponent != null)
        {
            Debug.Log(_magazineComponent.ammoCount);

            if (_magazineComponent.ammoCount > 0)
            {
                GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
                spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;
                audioSource.volume = 50f;
                audioSource.PlayOneShot(audioClip);
                Destroy(spawnedBullet, 5);
                _magazineComponent.ammoCount--;
            }
        }
        
        

    }
}