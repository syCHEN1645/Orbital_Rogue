using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProjectileSpawnerData : ComponentData<AttackProjectileSpawner>
{
    [field: SerializeField] public Vector3 SpawnPositionOffset { get; private set; } 
    [field: SerializeField] public GameObject ProjectilePrefab { get; private set; } 

    public ProjectileSpawnerData()
    {
        ComponentDependency = typeof(ProjectileSpawner);
    }
}
