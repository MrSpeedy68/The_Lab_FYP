using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineSpawner : MonoBehaviour
{
    private MagazineComponent _magazineComponent;

    public GameObject magazineObj;

    private MagazineComponent mag;
    public void Start()
    { 

    }

    public void SpawnMag()
    {
        
    }

    private void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnMag();
        }
    }
}
