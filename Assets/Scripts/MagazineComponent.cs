using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MagazineComponent : MonoBehaviour
{
    public int ammoCount = 0;
    
    [SerializeField] private TMP_Text _ammoText;
    
    private int _maxAmmo = 20;

    private void Start()
    {
        ammoCount = _maxAmmo;

        _ammoText.text = ammoCount + "/" + _maxAmmo;
    }

    public void RemoveBullet()
    {
        ammoCount--;
        _ammoText.text = ammoCount + "/" + _maxAmmo;
    }
}
