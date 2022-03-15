using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private Vector3 respawnLoc;
    
    
    // Start is called before the first frame update
    void Start()
    {
        respawnLoc = transform.position;
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
