using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPointGenerator : GameObjectGenerator
{
    // this generator generates everything in the last room (vectory point, boss, etc.)
    
    protected GameObject portal = PGPararmeters.portal;

    public override void GenerateOneRoom(HashSet<Vector2Int> room)
    {
        List<Vector2Int> floors = new List<Vector2Int>();
        foreach (Vector2Int floor in room) {
            floors.Add(floor);
        }
        // instantiate player at a random tile
        Vector2Int portalPos = floors[Random.Range(0, room.Count - 1)];
        if (portal != null) {
            GameObject.Instantiate(portal, new Vector3(portalPos.x + spawnOffsetX, portalPos.y + spawnOffsetY, 0), Quaternion.identity);
        } else {
            Debug.Log("Portal is empty, spawn at " + new Vector3(portalPos.x, portalPos.y, 0));
        }
    }
}
