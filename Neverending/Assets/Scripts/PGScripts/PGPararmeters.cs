using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PGPararmeters
{
    // list of enemies
    // may consider split into separate lists for different levels
    public static GameObject[] enemyList = {Resources.Load("Enemy Light") as GameObject,
        Resources.Load("Blue Idle") as GameObject};
    
    // ...
    public static List<GameObject> itemList;
}
