using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Interfaces;

public class Enemy : MonoBehaviour, IDamageable
{  
    
    public AudioSource HitSound;
    public int Hitpoints;
    public int MaxHitpoints = 5;
    public HealthBarBehaviour HealthBar;
    public int EnemyDamage = 2;
    public bool canDamage = true;
    private float coolDown = 1.25f;


    public void Start()
    {
        Hitpoints = MaxHitpoints;
        HealthBar.SetHealth(Hitpoints, MaxHitpoints);
    }

    public void TakeDamage(int damage)
    {   

        Hitpoints -= damage;
        HitSound.Play();
        HealthBar.SetHealth(Hitpoints, MaxHitpoints);

        if (Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    private IEnumerator OnCollisionStay2D(Collision2D other)
    {
        
        IDamageable structure = other.gameObject.GetComponent<IDamageable>();

        if (other.gameObject.CompareTag("Structure") && canDamage)
        {
            canDamage = false;
            
            structure.TakeDamage(EnemyDamage);
            
            yield return new WaitForSeconds(coolDown);

            canDamage = true;
            
        }
        
    }
}