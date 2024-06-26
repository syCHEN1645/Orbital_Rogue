using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PGPararmeters
{
    // list of enemies
    // may consider split into separate lists for different levels
    public static List<GameObject> enemyList;
    
    // ...
    public static List<GameObject> itemList;

    public static GameObject enemyLight = (GameObject)Resources.Load("Assets/Prefabs/new Prefabs/Enemy Light.prefab");
}
