using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PGPararmeters
{
    // list of enemies
    // may consider split into separate lists for different levels
    public static GameObject[] typesOfEnemies = {Resources.Load("Enemy Bandit 1") as GameObject,
        Resources.Load("Blue Slime 1") as GameObject};
    
    // ...
    public static List<GameObject> itemList;

    public static float spawnOffsetX = 0.5f;
    public static float spawnOffsetY = 0.1f;
    // note: need to put player prefab in Resources folder
    public static GameObject player = Resources.Load("Test Player") as GameObject;
    public static GameObject portal = Resources.Load("Simple Portal") as GameObject;
}
