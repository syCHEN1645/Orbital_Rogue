using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class PGPararmeters
{
    // list of enemies
    // may consider split into separate lists for different levels
    public static GameObject[] typesOfEnemies = {
        Resources.Load("Prefabs/Enemy Bandit 1") as GameObject,
        Resources.Load("Prefabs/Blue Slime 1") as GameObject
    };

    // list of bosses
    public static GameObject[] typesOfBosses = {
        Resources.Load("Prefabs/Enemy Boss 1") as GameObject,
        Resources.Load("Prefabs/Evil Wizard") as GameObject
    };
    
    // list of rewards
    public static GameObject[] itemList = {
        // Resources.Load("Prefabs/Buffs/AttackBoost1") as GameObject, 
        Resources.Load("Prefabs/Buffs/IncreaseDefense1") as GameObject, 
        Resources.Load("Prefabs/Buffs/HealthRecovery1") as GameObject,
        Resources.Load("Prefabs/Buffs/AttackBoost1") as GameObject
    };

    public static GameObject portalKey = Resources.Load("Keys/Key 1") as GameObject;

    public static float spawnOffsetX = 0.5f;
    public static float spawnOffsetY = 0.1f;
    // note: need to put player prefab in Resources folder
    public static GameObject player = Resources.Load("Prefabs/Player") as GameObject;
    public static GameObject portal = Resources.Load("Prefabs/Simple Portal") as GameObject;
}
