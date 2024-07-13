using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackDamage : AttackData
{
    [field: SerializeField] public float Amount { get; private set; }
}
