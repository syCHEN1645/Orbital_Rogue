using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : GameObjectGenerator
{
    protected bool keyGenerated = false;
    protected GameObject[] typesOfEnemies;
    // tile pos is the coordinates of bottom left corner of the tile, spawn locations need x, y offsets
    public EnemyGenerator(List<HashSet<Vector2Int>> roomsList, HashSet<Vector2Int> floorPositions) {
        // set parameters here
        this.roomsList = roomsList;
        this.floorPositions = floorPositions;
        // this.wallPositions = wallPositions;
        typesOfEnemies = PGPararmeters.typesOfEnemies;
    }

    public override void GenerateOneRoom(HashSet<Vector2Int> room)
    {
        foreach (var pos in room) {
            if (SpawnPosCheck(pos, room) && RandomBool(5)) {
                // 5% chance being true
                GameObject obj = GameObject.Instantiate(
                    typesOfEnemies[RandomInt(0, typesOfEnemies.Length - 1)], 
                    new Vector3(pos.x + spawnOffsetX, pos.y + spawnOffsetY, 0), 
                    Quaternion.identity);
                // pass key to the 1st enemy generated
                if (!keyGenerated) {
                    obj.GetComponent<Enemy>().AddKey();
                    keyGenerated = true;
                }
            }
        }
    }
}
