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
        keyGenerated = false;
        foreach (var pos in room) {
            GameObject obj;
            // make sure has at least one enemy with a key
            // pass key to the 1st enemy generated
            if (!keyGenerated) {
                obj = GameObject.Instantiate(
                    typesOfEnemies[RandomInt(0, typesOfEnemies.Length - 1)], 
                    new Vector3(pos.x + spawnOffsetX, pos.y + spawnOffsetY, 0), 
                    Quaternion.identity
                );
                obj.GetComponent<Enemy>().AddKey();
                keyGenerated = true;
            } else if (SpawnPosCheck(pos, room) && RandomBool(4 + level)) {
                // (4 + level)% chance being true
                obj = GameObject.Instantiate(
                    typesOfEnemies[RandomInt(0, typesOfEnemies.Length - 1)], 
                    new Vector3(pos.x + spawnOffsetX, pos.y + spawnOffsetY, 0), 
                    Quaternion.identity
                );
            }
        }
    }
}
