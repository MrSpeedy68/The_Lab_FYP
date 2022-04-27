using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoGraphic : MonoBehaviour
{
    public TMP_Text pistol;
    public TMP_Text rifle;
    public TMP_Text shotgun;
    void Start()
    {
        pistol.text = PlayerData.pistolAmmo.ToString();
        rifle.text = PlayerData.rifleAmmo.ToString();
        shotgun.text = PlayerData.shotgunAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        pistol.text = PlayerData.pistolAmmo.ToString();
        rifle.text = PlayerData.rifleAmmo.ToString();
        shotgun.text = PlayerData.shotgunAmmo.ToString();
    }
}
