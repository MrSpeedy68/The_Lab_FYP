using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineComponent : MonoBehaviour
{

    //public List<GameObject> meshes;
    public int ammoCount = 20;

    public enum WeaponType
    {
        Rifle,
        Pistol
    };
    
    private void Start()
    {
        
    }
}
