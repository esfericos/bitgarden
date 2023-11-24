using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
public class Core : Entity, IDamageable
{   
    [Header("References")]
    [SerializeField] private HealthBarBehaviour HealthBar;
    
    [Header("Attribute")]
    [SerializeField] private int Hitpoints;
    [SerializeField]private int MaxHitpoints = 30;
    public static bool IsDead { set; get; }
    public override Price Price { get; set; }

    public override void Start()
    {
        Price = new Price();
        IsDead = false;
        Hitpoints = MaxHitpoints;
        HealthBar.SetHealth(Hitpoints, MaxHitpoints);
    }

    public void TakeDamage(int damage)
    {
        Hitpoints -= damage;
        HealthBar.SetHealth(Hitpoints, MaxHitpoints);

        if (Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    
    // No onDestroy, ou metodo semelhante,
    // nao esquecer de colocar IsDead = true
}
