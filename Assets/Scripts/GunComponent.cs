using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunComponent : MonoBehaviour
{

    [SerializeField] private float bulletSpeed = 60f;
    [SerializeField] private float fireRate = 12f; // How many rounds fired per second average fire rate = 700/min
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform barrel;
    [SerializeField] private AudioSource audioSource;
    
    [SerializeField] private AudioClip gunFireAudio;
    [SerializeField] private AudioClip gunEmptyAudio;
    [SerializeField] private AudioClip gunMagInAudio;
    [SerializeField] private AudioClip gunMagOutAudio;
    
    [SerializeField] private GameObject magazineSocket;
    [SerializeField] private GameObject casing;
    [SerializeField] private Transform ejectTransform;

    private MagazineComponent _magazineComponent;
    private bool _isActive = false;
    private float timeBeforeShooting;
    
    public void Fire()
    {
        _magazineComponent = magazineSocket.GetComponentInChildren<MagazineComponent>();

        if (_magazineComponent != null)
        {
            Debug.Log(_magazineComponent.currentAmmoCount);

            if (_magazineComponent.currentAmmoCount > 0)
            {
                GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
                spawnedBullet.GetComponent<Rigidbody>().velocity = bulletSpeed * barrel.forward;
                audioSource.volume = 50f;
                audioSource.PlayOneShot(gunFireAudio);
                Destroy(spawnedBullet, 5);
                _magazineComponent.RemoveBullet();
                EjectCasing();
            }
            else audioSource.PlayOneShot(gunEmptyAudio);
        }
        else audioSource.PlayOneShot(gunEmptyAudio);
    }



    private void Start()
    {
        timeBeforeShooting = 1 / fireRate;
    }

    private void Update()
    {
        if (_isActive)
        {
            if (timeBeforeShooting <= 0f)
            {
                Fire();
                timeBeforeShooting = 1 / fireRate;
            }
            else
            {
                timeBeforeShooting -= Time.deltaTime;
            }
        }
    }

    private void EjectCasing()
    {
        var ejectedCasing = Instantiate(casing, ejectTransform.position, Quaternion.Euler(90f,0f,0f));
        var rb = ejectedCasing.GetComponent<Rigidbody>();
        rb.AddForce(ejectTransform.right.normalized * 300);
        Destroy(ejectedCasing, 3f);
    }

    public void WeaponState()
    {
        _isActive = !_isActive;
    }

    public void ReleaseWeapon()
    {
        _isActive = false;
    }

    public void PlayMagIn()
    {
        audioSource.PlayOneShot(gunMagInAudio);
    }
    
    public void PlayMagOut()
    {
        audioSource.PlayOneShot(gunMagOutAudio);
    }
}
