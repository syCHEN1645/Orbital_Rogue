using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : WeaponComponent<ProjectileSpawnerData, AttackProjectileSpawner>
{
    private Camera mainCam;
    private Vector3 mousePos;
    protected Player player;
    protected PlayerData playerData;
    private Transform projectileSpawnPoint;

    protected override void Awake()
    {
        base.Awake();
        mainCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        projectileSpawnPoint = player.GetComponent<Transform>();
        playerData = player.GetComponent<PlayerData>();
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 rotation = mousePos - transform.position;
        //float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
    }

    protected override void Start()
    {
        base.Start();
        if (eventHandler != null) {
            eventHandler.OnProjectileSpawn += HandleProjectileSpawn;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (eventHandler != null) {
            eventHandler.OnProjectileSpawn -= HandleProjectileSpawn;
        }
    }

    private void HandleProjectileSpawn()
    {
        Vector3 direction = (mousePos - projectileSpawnPoint.position).normalized;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        Debug.Log("rotZbefore: " + rotZ);
        if (rotZ <= 90f && rotZ >= -90f) { // Facing right
            rotZ = Mathf.Clamp(rotZ, -45f, 45f);
        } else if (rotZ < 180f && rotZ > 90f) { // Facing left
            rotZ = Mathf.Clamp(rotZ, 135f, 180f);
        } else {
            rotZ = Mathf.Clamp(rotZ, -180f, -135f);
        }
        // If the angle is between -135 and 135, force it to the closest edge
        //rotZ = rotZ > 0 ? 135f : -135f;
        Debug.Log("rotZ: " + rotZ);
        Quaternion newRotation = Quaternion.Euler(0, 0, rotZ);
        Debug.Log(newRotation);
        if (data.ProjectilePrefab != null)
        {   
            GameObject newArrow = Instantiate(data.ProjectilePrefab, projectileSpawnPoint.position + data.SpawnPositionOffset, newRotation);
            newArrow.GetComponent<Projectile>().SetProjectileData(
                new DataPackage(playerData.Attack + currentAttackData.Damage, currentAttackData.Range));
        }
    }
}