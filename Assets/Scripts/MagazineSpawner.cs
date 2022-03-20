using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagazineSpawner : MonoBehaviour
{
    private Vector3 magSpawn;
    public GameObject[] magazineObj;
    private XRGrabInteractable grabScript;
    
    [SerializeField]
    private XRDirectInteractor[] hands;

    private Player _player;
    
    public enum MagazineType
    {
        RIFLE = 0,
        PISTOL = 1,
        SHOTGUN = 2
    };
    
    public MagazineType magType;
    
    public void Start()
    {
        _player = GetComponentInParent<Player>();
        grabScript = GetComponent<XRGrabInteractable>();
    }

    private void FixedUpdate()
    {
        CheckObjectsInHands();
    }


    private void CheckObjectsInHands()
    {
        foreach (var hand in hands)
        {
            if (hand.isSelectActive)
            {
                if (hand.firstInteractableSelected != null)
                {
                    string interactableName = hand.firstInteractableSelected.ToString();
                    CheckGunType(interactableName.Replace(" (UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable)", ""));
                }
            }
        }
    }

    public void CheckGunType(string objName)
    {
        if (objName == "Shotgun")
        {
            magType = MagazineType.SHOTGUN;
        }
        else if (objName == "Pistol")
        {
            magType = MagazineType.PISTOL;
        }
        else magType = MagazineType.RIFLE;
    }

    public void SpawnMag()
    {
        magSpawn = grabScript.interactorsSelecting[0].transform.position;

        int val = (int)magType;

        Instantiate(magazineObj[val], magSpawn, Quaternion.identity);

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
