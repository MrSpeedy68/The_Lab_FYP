using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector3 respawnLoc;

    private MagazineSpawner _magazineSpawner;
    
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
                PlayerData.rifleAmmo += amount;
                break;
            case 1:
                PlayerData.pistolAmmo += amount;
                break;
            case 2:
                PlayerData.shotgunAmmo += amount;
                break;
            default:
                PlayerData.rifleAmmo += amount;
                break;
        }
    }

    public bool IsAmmoAvailable(int type)
    {
        bool isAmmo = false;
        switch (type)
        {
            case 0:
                isAmmo = PlayerData.rifleAmmo > 0;
                break;
            case 1:
                isAmmo = PlayerData.pistolAmmo > 0;
                break;
            case 2:
                isAmmo = PlayerData.shotgunAmmo > 0;
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
                if (PlayerData.rifleAmmo >= magAmount)
                {
                    PlayerData.rifleAmmo -= magAmount;
                    returnAmount = magAmount;
                }
                else
                {
                    returnAmount = PlayerData.rifleAmmo;
                    PlayerData.rifleAmmo -= PlayerData.rifleAmmo;
                }
                break;
            
            case 1:
                if (PlayerData.pistolAmmo >= magAmount)
                {
                    PlayerData.pistolAmmo -= magAmount;
                    returnAmount = magAmount;
                }
                else
                {
                    returnAmount = PlayerData.pistolAmmo;
                    PlayerData.pistolAmmo -= PlayerData.pistolAmmo;
                }
                break;
            
            case 2:
                if (PlayerData.shotgunAmmo >= magAmount)
                {
                    PlayerData.shotgunAmmo -= magAmount;
                    returnAmount = magAmount;
                }
                else
                {
                    returnAmount = PlayerData.shotgunAmmo;
                    PlayerData.shotgunAmmo -= PlayerData.shotgunAmmo;
                }
                break;
            default:
                PlayerData.rifleAmmo -= magAmount;
                break;
        }

        return returnAmount;
    }

    public void TakeDamage(float damageAmount)
    {
        PlayerData.health -= damageAmount;

        if (PlayerData.health <= 0f)
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
        PlayerData.health = 100f;
        transform.position = respawnLoc;
    }
}
