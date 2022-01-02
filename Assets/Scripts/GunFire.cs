using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    public float speed = 60f;
    public GameObject bullet;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void Fire()
    {
        GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;
        audioSource.PlayOneShot(audioClip);
        Destroy(spawnedBullet, 5);
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.rigidbody != null)
    //     {
    //         Vector3 hitVector = (other.transform.position - transform.position).normalized;
    //     
    //         other.rigidbody.AddForce(-hitVector * other.rigidbody.mass * 100f);
    //         
    //         Debug.Log("Hit Rigid body and added force");
    //     }
    // }
}
