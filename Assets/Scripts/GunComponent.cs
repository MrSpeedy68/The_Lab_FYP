  using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GunComponent : MonoBehaviour
{
    [Header("Weapon Attributes")]
    [Tooltip("If weapon isAutomatic then this will determine the amount of rounds fired per second else it will be how many bullets are spread")]
    [SerializeField] private float fireRate = 12f; // How many rounds fired per second average fire rate = 700/min
    [SerializeField] private float bulletForce = 100f;
    [SerializeField] private float bulletDamage;
    [SerializeField] private bool isAutomatic;
    [SerializeField] private bool isSpreadFire;
    [SerializeField] private float spreadAmount;
    [Tooltip("If you want to use a Magazine Component then this will be false otherwise specify how large the magazine storage is")]
    [SerializeField] private bool hasInternalStorage;
    
    [Header("Transforms And Additional Objects")]
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform ejectTransform;
    [SerializeField] private GameObject magazineSocket;
    [SerializeField] private GameObject casing;
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gunFireAudio;
    [SerializeField] private AudioClip gunEmptyAudio;
    [SerializeField] private AudioClip gunMagInAudio;
    [SerializeField] private AudioClip gunMagOutAudio;

    [Header("FX")]
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private ParticleSystem shootParticleSystem;
    [SerializeField] private ParticleSystem impactParticleSystem;

    public LayerMask _mask;
    private MagazineComponent _magazineComponent;
    private bool _isActive = false;
    private float _timeBeforeShooting;
    
    private void Fire()
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

    private void SpreadFire()
    {
        //_magazineComponent = magazineSocket.GetComponentInChildren<MagazineComponent>();
        if (_magazineComponent != null && _magazineComponent.currentAmmoCount > 0)
        {
            audioSource.volume = 50f;
            audioSource.PlayOneShot(gunFireAudio);
            _magazineComponent.RemoveBullet();

            shootParticleSystem.Play();
            EjectCasing();

            for (int i = 0; i < fireRate; i++)
            {
                float randomDirectionX = Random.Range(-spreadAmount, spreadAmount);
                float randomDirectionZ = Random.Range(-spreadAmount, spreadAmount);
                var direction = bulletSpawnPoint.forward + bulletSpawnPoint.up * randomDirectionZ + bulletSpawnPoint.right * randomDirectionX;
                
                if (Physics.Raycast(bulletSpawnPoint.position, direction, out RaycastHit hit, float.MaxValue, _mask))
                {
                    TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.identity);
                    
                    Debug.DrawRay(bulletSpawnPoint.position, direction,Color.red,5f);

                    StartCoroutine(SpawnTrail(trail, hit));
                    CheckHit(hit);
                }
            }
            
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

        Destroy(Trail.gameObject, Trail.time);
    }

    private void CheckHit(RaycastHit hit)
    {
        if (hit.collider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Detected");
            var enemy = hit.collider.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(bulletDamage);
            
            var ragdoll = hit.collider.gameObject.GetComponent<RagdollEnabler>();
            ragdoll.ApplyRagdollForce(hit.normal,1f);
        }
        
        else if (hit.rigidbody && !hit.collider.gameObject.CompareTag("Enemy"))
        {
            hit.rigidbody.AddForce(hit.point.normalized * bulletForce);
            SpawnImpactDecal(hit);
        }

        else if (hit.collider.gameObject.CompareTag("Target"))
        {
            Debug.Log("Hit Target Obj");
            hit.transform.SendMessage("HitByRay");
            SpawnImpactDecal(hit);
        }
        else
        {
            SpawnImpactDecal(hit);
        }

    }

    private void SpawnImpactDecal(RaycastHit hit)
    {
        var impactPointParticle = Instantiate(impactParticleSystem, hit.point, Quaternion.LookRotation(hit.normal));
        impactPointParticle.transform.parent = hit.transform; //Setting the impact particle as parent of the impact incase it has a rigitbody and moves so the hit moves with the moved rb.
    }
    
    private void Start()
    {
        _timeBeforeShooting = 0f;//_timeBeforeShooting = 1 / fireRate;

        if (hasInternalStorage)
        {
            _magazineComponent = GetComponent<MagazineComponent>();
        }
    }

    private void Update()
    {
        if (_isActive)
        {
            if (_timeBeforeShooting <= 0f && isAutomatic && !isSpreadFire)
            {
                Fire();
                _timeBeforeShooting = 1 / fireRate;
               
            }
            else if (_timeBeforeShooting <= 0f && !isAutomatic && !isSpreadFire)
            {
                Fire();
                _timeBeforeShooting = 1 / fireRate;
            }
            else if (_timeBeforeShooting <= 0f && !isAutomatic && isSpreadFire)
            {
                SpreadFire();
                _timeBeforeShooting = 1 / fireRate;
            }
            
            if (isAutomatic) _timeBeforeShooting -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.gameObject.CompareTag("Magazine") && hasInternalStorage)
        {
            if (!_magazineComponent.IsMaxAmmo())
            {
                _magazineComponent.AddBullet(1);
                Destroy(other.gameObject);
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

        if (!isAutomatic) _timeBeforeShooting = 0f;
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