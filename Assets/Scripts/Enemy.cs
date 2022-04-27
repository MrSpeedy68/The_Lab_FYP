using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    public float attackDamage;
    
    private HealthBar _healthBar;
    private float _maxHealth;

    private enum EnemyType
    {
        SPIDER,
        HUMANOID
    };

    [SerializeField]
    private EnemyType enemyType;

    private void Start()
    {
        _maxHealth = health;
        
        if (enemyType == EnemyType.HUMANOID)
        {
            _healthBar = GetComponentInChildren<HealthBar>();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        
        if (_healthBar) _healthBar.UpdateHealth(health / _maxHealth);

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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Weapon") && other.relativeVelocity.magnitude > 2f && health > 0f)
        {
            float damageDealt = other.relativeVelocity.magnitude * other.rigidbody.mass;
            print("Damage Dealt = " + damageDealt);
            health -= damageDealt;
            if (health <= 0f)
            {
                Die();
            }
        }
    }


    public void ApplyForcesFromGun()
    {
        
    }
    
    
    public void ApplyForcesFromMelee(float relativeVelocity, float mass, Vector3 hitPoint)
    {
        if (relativeVelocity > 1f)
        {
            if (health <= 0f)
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
                var ragdoll = GetComponent<RagdollEnabler>();
                ragdoll.EnableRagdoll();
                
                Vector3 hitVector = (hitPoint - transform.position).normalized;

                ragdoll.ApplyRagdollForce(hitVector, mass);
            }
        }
    }
    
    
}
