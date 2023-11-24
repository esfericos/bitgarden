using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Core : Entity
{
    public static bool IsDead { set; get; }
    public override Price Price { get; set; }

    public override void Start()
    {
        Price = new Price();
        IsDead = false;
    }

    // No onDestroy, ou metodo semelhante,
    // nao esquecer de colocar IsDead = true
}
