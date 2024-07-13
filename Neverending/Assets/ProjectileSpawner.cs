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
        projectileSpawnPoint = GetComponent<Transform>();
        player = GetComponentInParent<Player>();
        playerData = player.playerData;
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
        Debug.Log("shoot");
        Quaternion newRotation = player.FacingDirection == 1 ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
        if (data.projectilePrefab != null) {
            GameObject newArrow = Instantiate(data.projectilePrefab, 
            projectileSpawnPoint.position, newRotation);
            Debug.Log(playerData.Damage);
            Debug.Log(currentAttackData.Damage);
            Debug.Log(currentAttackData.Range);
            newArrow.GetComponent<Projectile>().SetProjectileData(
                    new DataPackage(playerData.Damage
                    + currentAttackData.Damage, currentAttackData.Range));
        }
    }
}
