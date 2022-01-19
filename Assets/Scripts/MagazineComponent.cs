using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MagazineComponent : MonoBehaviour
{
    public int currentAmmoCount = 0;
    
    [SerializeField] private TMP_Text _ammoText;
    
    [SerializeField]private int _maxAmmo = 30;

    private void Start()
    {
        currentAmmoCount = _maxAmmo;

        _ammoText.text = currentAmmoCount + "/" + _maxAmmo;
    }

    public void RemoveBullet()
    {
        currentAmmoCount--;
        _ammoText.text = currentAmmoCount + "/" + _maxAmmo;
    }
}
