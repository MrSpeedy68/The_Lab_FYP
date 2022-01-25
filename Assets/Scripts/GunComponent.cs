using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunComponent : MonoBehaviour
{
    [SerializeField] private float fireRate = 12f; // How many rounds fired per second average fire rate = 700/min
    [SerializeField] private float bulletForce = 100f;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform ejectTransform;
    [SerializeField] private GameObject magazineSocket;
    [SerializeField] private GameObject casing;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gunFireAudio;
    [SerializeField] private AudioClip gunEmptyAudio;
    [SerializeField] private AudioClip gunMagInAudio;
    [SerializeField] private AudioClip gunMagOutAudio;

    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private ParticleSystem shootParticleSystem;
    [SerializeField] private ParticleSystem impactParticleSystem;

    public LayerMask _mask;
    private MagazineComponent _magazineComponent;
    private bool _isActive = false;
    private float _timeBeforeShooting;
    
    public void Fire()
    {
        _magazineComponent = magazineSocket.GetComponentInChildren<MagazineComponent>();
        if (_magazineComponent != null && _magazineComponent.currentAmmoCount > 0)
        {
            if (Physics.Raycast(bulletSpawnPoint.position, bulletSpawnPoint.forward, out RaycastHit hit, float.MaxValue, _mask))
            {
                TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, hit));
                CheckHit(hit);
                
                audioSource.volume = 50f;
                audioSource.PlayOneShot(gunFireAudio);
                _magazineComponent.RemoveBullet();

                shootParticleSystem.Play();
                EjectCasing();
            }
            else audioSource.PlayOneShot(gunEmptyAudio);
        }
        else audioSource.PlayOneShot(gunEmptyAudio);
    }

    IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
    {
        float time = 0f;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }
        Trail.transform.position = Hit.point;
        Instantiate(impactParticleSystem, Hit.point, Quaternion.LookRotation(Hit.normal));
        
        Destroy(Trail.gameObject, Trail.time);
    }

    private void CheckHit(RaycastHit hit)
    {
        if (hit.collider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Detected");
            RagdollEnabler ragdollEnabler = hit.collider.gameObject.GetComponent<RagdollEnabler>();
            ragdollEnabler.TakeDamage(10f);
        }


        if (hit.rigidbody)
        {
            hit.rigidbody.AddForce(hit.point.normalized * bulletForce);
        }
    }
    
    private void Start()
    {
        _timeBeforeShooting = 1 / fireRate;
    }

    private void Update()
    {
        if (_isActive)
        {
            if (_timeBeforeShooting <= 0f)
            {
                Fire();
                _timeBeforeShooting = 1 / fireRate;
            }
            else
            {
                _timeBeforeShooting -= Time.deltaTime;
            }
        }
    }

    private void EjectCasing()
    {
        var ejectedCasing = Instantiate(casing, ejectTransform.position, Quaternion.Euler(90f,UnityEngine.Random.Range(-45f, 45f),UnityEngine.Random.Range(-45f, 45f)));
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
