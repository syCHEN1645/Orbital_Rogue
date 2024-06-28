using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : GameObjectGenerator
{
    protected GameObject[] typesOfEnemies;
    // tile pos is the coordinates of bottom left corner of the tile, spawn locations need x, y offsets
    public EnemyGenerator(List<HashSet<Vector2Int>> roomsList, HashSet<Vector2Int> floorPositions) {
        // set parameters here
        this.roomsList = roomsList;
        this.floorPositions = floorPositions;
        this.typesOfEnemies = PGPararmeters.typesOfEnemies;
        // level = ??;
    }
    public override void GenerateOneRoom(HashSet<Vector2Int> room)
    {
        foreach (var pos in room) {
            if (RandomBool(5)) {
                // 5% chance being true
                GameObject.Instantiate(
                    typesOfEnemies[RandomInt(0, typesOfEnemies.Length - 1)], 
                    new Vector3(pos.x + spawnOffsetX, pos.y + spawnOffsetY, 0), 
                    Quaternion.identity);
            }
        }
    }
}
