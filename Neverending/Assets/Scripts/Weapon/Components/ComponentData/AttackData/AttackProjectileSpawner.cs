using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackProjectileSpawner : AttackData
{
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float Range { get; private set; }
}
