using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class PGPararmeters
{
    // array of enemies
    // may consider split into separate lists for different levels
    public static GameObject[] typesOfEnemies = {
        Resources.Load("Prefabs/Enemy Bandit 1") as GameObject,
        Resources.Load("Prefabs/Blue Slime 1") as GameObject
    };
    
    // ...
    public static List<GameObject> itemList;

    // array of visualisers
    public static TileMapVisualiser[] visualisers = {
        Resources.Load("Visualisers/Visualiser1").GetComponent<TileMapVisualiser>(), 
        Resources.Load("Visualisers/Visualiser2").GetComponent<TileMapVisualiser>(), 
        Resources.Load("Visualisers/Visualiser3").GetComponent<TileMapVisualiser>(), 
        Resources.Load("Visualisers/Visualiser4").GetComponent<TileMapVisualiser>()
    };

    public static float spawnOffsetX = 0.5f;
    public static float spawnOffsetY = 0.1f;
    // note: need to put player prefab in Resources folder
    public static GameObject player = Resources.Load("Prefabs/Player") as GameObject;
    public static GameObject portal = Resources.Load("Prefabs/Simple Portal") as GameObject;
}
