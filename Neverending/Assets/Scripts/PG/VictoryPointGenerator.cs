using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class VictoryPointGenerator : GameObjectGenerator
{
    // this generator generates everything in the last room (vectory point, boss, etc.)
    protected bool keyGenerated = false;
    protected GameObject portal = PGPararmeters.portal;

    public override void GenerateOneRoom(HashSet<Vector2Int> room)
    {
        List<Vector2Int> floors = new List<Vector2Int>();
        foreach (Vector2Int floor in room) {
            floors.Add(floor);
        }
        // instantiate portal at a random tile
        Vector2Int pos;
        do {
            pos = floors[Random.Range(0, room.Count - 1)];
        } 
        while (!SpawnPosCheck(pos, room));

        if (portal != null) {
            GameObject.Instantiate(portal, new Vector3(pos.x + spawnOffsetX, pos.y + spawnOffsetY, 0), Quaternion.identity);
        } else {
            Debug.Log("Portal is empty, spawn at " + new Vector3(pos.x, pos.y, 0));
        }
        
        // instantiate boss
        do {
            pos = floors[Random.Range(0, room.Count - 1)];
        } 
        while (!SpawnPosCheck(pos, room));

        // different boss by level
        for (int i = 0; i < 1 + (level / 3); i++) {
            GameObject obj = GameObject.Instantiate(
                PGPararmeters.typesOfBosses[Random.Range(0, PGPararmeters.typesOfBosses.Length)], 
                new Vector3(pos.x + spawnOffsetX, pos.y + spawnOffsetY, 0), 
                Quaternion.identity
            );
            // boss drops a key
            if (!keyGenerated) {
                obj.GetComponent<Enemy>().AddKey();
                keyGenerated = true;
            }
        }  
    }
}
