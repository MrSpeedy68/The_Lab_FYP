using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    public float attackDamage;

    private enum EnemyType
    {
        SPIDER,
        HUMANOID
    };

    [SerializeField]
    private EnemyType enemyType;

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        if(enemyType == EnemyType.SPIDER)
        {
            GetComponent<SpiderProceduralAnimation>().enabled = false;
            GetComponent<SpiderController>().enabled = false;
            var anim = GetComponent<Animator>();
            anim.SetBool("isDead", true);
            
            Destroy(gameObject,30f);
        }
        else if (enemyType == EnemyType.HUMANOID)
        {
            var re = GetComponent<RagdollEnabler>();
            re.EnableRagdoll();
            
            Destroy(gameObject,30f);
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<Player>();
            player.TakeDamage(attackDamage);
        }
    }
}
