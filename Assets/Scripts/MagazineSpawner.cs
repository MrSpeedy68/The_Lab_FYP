using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagazineSpawner : MonoBehaviour
{
    private Vector3 magSpawn;
    public GameObject[] magazineObj;
    public GameObject magStorage;
    private XRGrabInteractable grabScript;
    private StoreMagazine _storeMagazine;
    
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
        _storeMagazine = GameObject.Find("MagazineStorage").GetComponent<StoreMagazine>();
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
        if (objName.Contains("870_Shotgun"))
        {
            magType = MagazineType.SHOTGUN;
        }
        else if (objName.Contains("Pistol"))
        {
            magType = MagazineType.PISTOL;
        }
        else if (objName.Contains("M4A1 Rifle"))
        {
            magType = MagazineType.RIFLE;
        }
    }

    public void SpawnMag()
    {
        StartCoroutine(DelayStore());
        magSpawn = grabScript.interactorsSelecting[0].transform.position;
        int val = (int)magType;

        if (_player.IsAmmoAvailable(val))
        {
            var newMag = Instantiate(magazineObj[val], magSpawn, Quaternion.identity);
            var magComp = newMag.GetComponent<MagazineComponent>();
    
            magComp.currentAmmoCount = _player.RemoveAmmo(val, magComp._maxAmmo);
    
            StopAllCoroutines();
            StartCoroutine(ReleaseSpawner());
            StartCoroutine(DelayStore());
        }
        
    }

    IEnumerator ReleaseSpawner()
    {
        grabScript.enabled = false;
        
        yield return new WaitForSeconds(0.2f);

        grabScript.enabled = true;
    }

    IEnumerator DelayStore()
    {
        _storeMagazine.enabled = false;
        magStorage.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        _storeMagazine.enabled = true;
        magStorage.SetActive(true);
        
        yield return null;
    }
}
