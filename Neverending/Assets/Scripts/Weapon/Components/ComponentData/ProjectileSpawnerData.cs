using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProjectileSpawnerData : ComponentData<AttackProjectileSpawner>
{
    [field: SerializeField] public GameObject projectilePrefab { get; private set; } 

    public ProjectileSpawnerData()
    {
        ComponentDependency = typeof(ProjectileSpawner);
    }
}
