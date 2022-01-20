using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagazineSpawner : MonoBehaviour
{

    private Vector3 magSpawn;
    public GameObject magazineObj;
    private XRGrabInteractable grabScript;
    
    public void Start()
    {
        grabScript = GetComponent<XRGrabInteractable>();
    }

    public void SpawnMag()
    {
        magSpawn = grabScript.interactorsSelecting[0].transform.position;
        
        Instantiate(magazineObj, magSpawn, Quaternion.identity);

        StopAllCoroutines();
        StartCoroutine(ReleaseSpawner());
    }

    IEnumerator ReleaseSpawner()
    {
        grabScript.enabled = false;
        
        yield return new WaitForSeconds(0.1f);

        grabScript.enabled = true;
    }
}
