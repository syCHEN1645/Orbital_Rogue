using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : WeaponComponent<ProjectileSpawnerData, AttackProjectileSpawner>
{
    protected Player player;
    protected PlayerData playerData;
    private Transform projectileSpawnPoint;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        projectileSpawnPoint = player.GetComponent<Transform>();
        playerData = player.GetComponent<PlayerData>();
    }

    protected override void Start()
    {
        base.Start();
        eventHandler.OnProjectileSpawn += HandleProjectileSpawn;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        eventHandler.OnProjectileSpawn -= HandleProjectileSpawn;
    }

    private void HandleProjectileSpawn()
    {
        Quaternion newRotation = player.FacingDirection == 1 ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
        if (data.projectilePrefab != null) {
            GameObject newArrow = Instantiate(data.projectilePrefab, 
            projectileSpawnPoint.position, newRotation);
            newArrow.GetComponent<Projectile>().SetProjectileData(
                    new DataPackage(playerData.Damage
                    + currentAttackData.Damage, currentAttackData.Range));
        }
    }
}
