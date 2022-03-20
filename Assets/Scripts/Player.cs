using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private Vector3 respawnLoc;

    private MagazineSpawner _magazineSpawner;

    public int rifleAmmo;
    public int pistolAmmo;
    public int shotgunAmmo;

    // Start is called before the first frame update
    void Start()
    {
        _magazineSpawner = GetComponentInChildren<MagazineSpawner>();
        respawnLoc = transform.position;
    }

    public void AddAmmo(int type, int amount)
    {
        switch (type)
        {
            case 0:
                rifleAmmo += amount;
                break;
            case 1:
                pistolAmmo += amount;
                break;
            case 2:
                shotgunAmmo += amount;
                break;
            default:
                rifleAmmo += amount;
                break;
        }
    }

    public bool IsAmmoAvailable(int type)
    {
        bool isAmmo = false;
        switch (type)
        {
            case 0:
                isAmmo = rifleAmmo > 0;
                break;
            case 1:
                isAmmo = pistolAmmo > 0;
                break;
            case 2:
                isAmmo = shotgunAmmo > 0;
                break;
        }

        return isAmmo;
    }

    public int RemoveAmmo(int type, int magAmount)
    {
        int returnAmount = 0;
        switch (type)
        {
            case 0:
                if (rifleAmmo >= magAmount)
                {
                    rifleAmmo -= magAmount;
                    returnAmount = magAmount;
                }
                else
                {
                    returnAmount = rifleAmmo;
                    rifleAmmo -= rifleAmmo;
                }
                break;
            
            case 1:
                if (pistolAmmo >= magAmount)
                {
                    pistolAmmo -= magAmount;
                    returnAmount = magAmount;
                }
                else
                {
                    returnAmount = pistolAmmo;
                    pistolAmmo -= pistolAmmo;
                }
                break;
            
            case 2:
                if (shotgunAmmo >= magAmount)
                {
                    shotgunAmmo -= magAmount;
                    returnAmount = magAmount;
                }
                else
                {
                    returnAmount = shotgunAmmo;
                    shotgunAmmo -= shotgunAmmo;
                }
                break;
            default:
                rifleAmmo -= magAmount;
                break;
        }

        return returnAmount;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0f)
        {
            Die();
        }
    }

    public void SetRespawnPos(Vector3 newPos)
    {
        respawnLoc = newPos;
    }

    private void Die()
    {
        health = 100f;
        transform.position = respawnLoc;
    }
}
