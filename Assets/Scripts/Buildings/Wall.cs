using UnityEngine;
using UnityEditor;

public class Wall : Entity
{
    [Header("References")]
    [SerializeField] private HealthBarBehaviour HealthBar;
    
    [Header("Attributes")]
    [SerializeField] private int Hitpoints;
    [SerializeField] private int MaxHitpoints;
    public int Type = 0;
    
    public override Price Price { get; set; }
    public override void Start()
    {
        Price = new Price();
        Hitpoints = MaxHitpoints;
        HealthBar.SetHealth(Hitpoints, MaxHitpoints);   
    }
    
    public void TakeHit(int damage)
    {   
        Price = new Price(gold: 1);
        Hitpoints -= damage;
        HealthBar.SetHealth(Hitpoints, MaxHitpoints);

        if (Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}