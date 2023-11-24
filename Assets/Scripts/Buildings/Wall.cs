using Interfaces;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class Wall : Entity, IDamageable
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

    public void TakeDamage(int damage)
    {
        Price = new Price(gold: 1);
        Hitpoints -= damage;
        HealthBar.SetHealth(Hitpoints, MaxHitpoints);

        if (Hitpoints <= 0)
        {
            Position position = new(
                x: (ushort)(gameObject.transform.position.x),
                y: (ushort)(gameObject.transform.position.y)
            );
            Graph grafo = GameObject.FindGameObjectWithTag("GraphHandle").GetComponent<Graph>();
            grafo.walls = grafo.walls.Where(ent => ent != position.ToId()).ToArray();
            Destroy(gameObject);
        }
    }
}