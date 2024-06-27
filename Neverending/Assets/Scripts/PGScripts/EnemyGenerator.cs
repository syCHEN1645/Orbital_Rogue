using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : GameObjectGenerator
{
    // tile pos is the coordinates of bottom left corner of the tile, spawn locations need x, y offsets
    private float spawnOffsetX = PGPararmeters.spawnOffsetX;
    private float spawnOffsetY = PGPararmeters.spawnOffsetY;
    public EnemyGenerator(List<HashSet<Vector2Int>> roomsList, HashSet<Vector2Int> floorPositions) {
        // set parameters here
        this.roomsList = roomsList;
        this.floorPositions = floorPositions;
        this.objectList = PGPararmeters.enemyList;
        // level = ??;
    }
    public override void GenerateOneRoom(HashSet<Vector2Int> room)
    {
        foreach (var pos in room) {
            if (RandomBool(5)) {
                GameObject.Instantiate(
                    objectList[RandomInt(0, objectList.Length - 1)], 
                    new Vector3(pos.x + spawnOffsetX, pos.y + spawnOffsetY, 0), 
                    Quaternion.identity);
            }
        }
    }
}
