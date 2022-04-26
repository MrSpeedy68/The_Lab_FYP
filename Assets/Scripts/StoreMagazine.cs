using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StoreMagazine : MonoBehaviour
{
    [SerializeField]
    private XRDirectInteractor[] hands;
    private Player _player;

    private bool _isMagazine = false;
    private int _ammoInMag = 0;
    private string _magName;
    private GameObject _currentMag;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Magazine"))
        {
            _currentMag = other.gameObject;
            _isMagazine = true;
            _magName = other.name;
            _ammoInMag = other.gameObject.GetComponent<MagazineComponent>().currentAmmoCount;

                     
            for (int i = 0; i < hands.Length; i++)
            {
                if (hands[i].isSelectActive)
                {
                    if (hands[i].firstInteractableSelected != null)
                    {
                        string interactableName = hands[i].firstInteractableSelected.ToString();
                        string newStr = interactableName.Replace(" (UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable)", "");
                        // if (newStr.Any(char.IsDigit))
                        // {
                        //     var result = Regex.Match(newStr, @"\d+").Value;
                        //     string newStr2 = newStr.Replace("(" + result + ")","");
                        //     Debug.Log(newStr2 + " ====================");
                        // }
                         

                        if (newStr == _magName) break;
                        else if (i > 0)
                        {
                            AddAmmo();
                        }
                    }
                }
                if (i > 0)
                {
                    AddAmmo();
                }
            }
        }
        else _isMagazine = false;
    }
    
    private void OnTriggerExit(Collider other)
    {
        _isMagazine = false;
    }
    
    public void AddAmmo()
    {
        if (_isMagazine)
        {
            if (_currentMag)
            {
                
                int val = CheckGunType(_magName);
                _player.AddAmmo(val, _ammoInMag);
                
                Destroy(_currentMag);
            }
        }
    }
    
    public int CheckGunType(string objName)
    {
        if (objName.Contains("Shotgun Ammo"))
        {
            return 2;
        }
        if (objName.Contains("Pistol Magazine"))
        {
            return 1;
        }
        if (objName.Contains("M4_Magazine"))
        {
            return 0;
        }

        return 0;
    }

    
}
