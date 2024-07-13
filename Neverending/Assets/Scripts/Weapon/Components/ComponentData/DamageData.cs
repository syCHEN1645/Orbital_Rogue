using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageData : ComponentData<AttackDamage>
//melee damage data
{
    public DamageData()
    {
        ComponentDependency = typeof(Damage);
    }
}
