using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{   
    public AudioSource HitSound;
    public int Hitpoints;
    public int MaxHitpoints = 5;
    public HealthBarBehaviour HealthBar;
    

    public void Start()
    {
        Hitpoints = MaxHitpoints;
        HealthBar.SetHealth(Hitpoints, MaxHitpoints);
    }

    public void TakeHit(int damage)
    {   
        
        Hitpoints -= damage;
        HitSound.Play();
        HealthBar.SetHealth(Hitpoints, MaxHitpoints);
        
        if (Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }  
}
