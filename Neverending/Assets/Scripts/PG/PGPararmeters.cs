using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class PGPararmeters
{
    // list of enemies
    // may consider split into separate lists for different levels
    public static GameObject[] typesOfEnemies = {
        Resources.Load("Prefabs/Heavy Bandit") as GameObject,
        Resources.Load("Prefabs/Light Bandit") as GameObject,
        Resources.Load("Prefabs/Blue Slime 1") as GameObject
    };

    // list of bosses
    public static GameObject[] typesOfBosses = {
        Resources.Load("Prefabs/Bringer Of Death") as GameObject,
        Resources.Load("Prefabs/Evil Wizard 1") as GameObject, 
        Resources.Load("Prefabs/Evil Wizard 2") as GameObject,
    };
    
    // list of rewards
    public static GameObject[] itemList = {
        // Resources.Load("Prefabs/Buffs/AttackBoost1") as GameObject, 
        Resources.Load("Prefabs/Buffs/IncreaseDefense1") as GameObject, 
        Resources.Load("Prefabs/Buffs/IncreaseDefense2") as GameObject, 
        Resources.Load("Prefabs/Buffs/HealthRecovery1") as GameObject,
        Resources.Load("Prefabs/Buffs/HealthRecovery2") as GameObject,
        Resources.Load("Prefabs/Buffs/AttackBoost1") as GameObject,
        Resources.Load("Prefabs/Buffs/IncreaseAttack1") as GameObject,
        Resources.Load("Prefabs/Buffs/IncreaseMaxHealth1") as GameObject,
        Resources.Load("Prefabs/Buffs/DefenseBoost1") as GameObject
    };

    // list of weapons
    public static GameObject[] weaponList = {
        Resources.Load("Prefabs/WeaponPickup/WeaponPickup_Bow1") as GameObject, 
        Resources.Load("Prefabs/WeaponPickup/WeaponPickup_Sword1") as GameObject,
        Resources.Load("Prefabs/WeaponPickup/WeaponPickup_Sword2") as GameObject,
        Resources.Load("Prefabs/WeaponPickup/WeaponPickup_Tome1") as GameObject
    };



    public static GameObject portalKey = Resources.Load("Prefabs/Keys/Key 1") as GameObject;

    // list of visualisers
    public static TileMapVisualiser[] visualisers = {
        Resources.Load("Visualisers/Visualiser1").GetComponent<TileMapVisualiser>() as TileMapVisualiser, 
        Resources.Load("Visualisers/Visualiser2").GetComponent<TileMapVisualiser>() as TileMapVisualiser, 
        Resources.Load("Visualisers/Visualiser3").GetComponent<TileMapVisualiser>() as TileMapVisualiser, 
        Resources.Load("Visualisers/Visualiser4").GetComponent<TileMapVisualiser>() as TileMapVisualiser
    };

    public static float spawnOffsetX = 0.5f;
    public static float spawnOffsetY = 0.1f;
    // note: need to put player prefab in Resources folder
    public static GameObject player = Resources.Load("Prefabs/Player") as GameObject;
    public static GameObject portal = Resources.Load("Prefabs/Simple Portal") as GameObject;
}
