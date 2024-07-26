using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerGenerator : GameObjectGenerator
{
    public GameObject player = PGPararmeters.player;


    public override void GenerateOneRoom(HashSet<Vector2Int> room)
    {
        // player is spawned in this room
        // this room has no enemy
        // this room may have some interactables

        List<Vector2Int> floors = new List<Vector2Int>();
        foreach (Vector2Int floor in room) {
            floors.Add(floor);
        }
        // instantiate player at a random tile
        Vector2Int spawnPos;
        do {
            spawnPos = floors[Random.Range(0, room.Count - 1)];
        } while (!SpawnPosCheck(spawnPos, room));

        if (player != null) {
            GameObject.Instantiate(player, new Vector3(spawnPos.x + spawnOffsetX, spawnPos.y + spawnOffsetY, 0), Quaternion.identity);
        } else {
            Debug.Log("Player is empty, spawn at " + new Vector3(spawnPos.x, spawnPos.y, 0));
        }
        
        for (int i = 0; i < PGPararmeters.weaponList.Length; i++) {
            do {
                spawnPos = floors[Random.Range(0, floors.Count - 1)];
            } while (!SpawnPosCheck(spawnPos, room));
            if (PGPararmeters.weaponList[i] != null) {
                GameObject.Instantiate(
                    PGPararmeters.weaponList[i],
                    new Vector3(spawnPos.x, spawnPos.y, 0), 
                    Quaternion.identity
                );
            } else {
                Debug.Log("weapon " + i + " is null");
            }
        }
    }
}
