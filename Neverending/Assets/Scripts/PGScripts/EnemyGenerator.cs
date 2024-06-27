using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : GameObjectGenerator
{
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
                    new Vector3(pos.x, pos.y, 0), 
                    Quaternion.identity);
            }
        }
    }
}
