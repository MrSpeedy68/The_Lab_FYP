using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MagazineComponent : MonoBehaviour
{
    public int currentAmmoCount = 0;
    
    [SerializeField]private TMP_Text _ammoText;
    
    public int _maxAmmo;

    private void Start()
    {
        if (_ammoText) _ammoText.text = currentAmmoCount + "/" + _maxAmmo;
    }

    public void RemoveBullet()
    {
        currentAmmoCount--;
        if (_ammoText) _ammoText.text = currentAmmoCount + "/" + _maxAmmo;
    }
    
    public void AddBullet(int amount)
    {
        if (currentAmmoCount < _maxAmmo)
        {
            currentAmmoCount += amount;
            if (_ammoText) _ammoText.text = currentAmmoCount + "/" + _maxAmmo;
        }
    }
}
