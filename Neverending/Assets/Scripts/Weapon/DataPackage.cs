using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPackage
{
    public float AttackDamage { get; private set; }
    public float Range { get; private set; }

    public DataPackage(float attackDamage, float range)
    {
        this.AttackDamage = attackDamage;
        this.Range = range;
    }
}
