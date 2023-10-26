using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{   
    public AudioSource HitSound;
    public int Hitpoints;
    public int MaxHitpoints = 5;
    public HealthBarBehaviour HealthBar;
    public int EnemyDamage = 2;


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
    
    private void OnCollisionEnter2D(Collision2D other)
    {

        IDamageable structure = other.gameObject.GetComponent<IDamageable>();

        if (other.gameObject.CompareTag("Structure"))
        {
            structure.TakeDamage(EnemyDamage);
        }
        
    }
}