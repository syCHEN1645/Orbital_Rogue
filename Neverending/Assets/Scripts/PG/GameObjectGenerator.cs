using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameObjectGenerator
{
    protected List<HashSet<Vector2Int>> roomsList;
    protected HashSet<Vector2Int> floorPositions;
    protected HashSet<Vector2Int> wallPositions;
    protected float spawnOffsetX = PGPararmeters.spawnOffsetX;
    protected float spawnOffsetY = PGPararmeters.spawnOffsetY;
    protected int level;

    public virtual void GenerateOneRoom(HashSet<Vector2Int> room) {

    }
    protected bool RandomBool(int probability) {
        // randomly generate a bool
        int x = Random.Range(0, 100);
        if (x <= probability - 1) {
            return true;
        }
        return false;
    }

    protected int RandomInt(int min, int max) {
        // randomly generate an int
        return Random.Range(min, max + 1);
    }    
    
    protected bool SpawnPosCheck(Vector2Int pos, HashSet<Vector2Int> room) {
        // if 4 neighbouring tiles are all floor, then most likely not going to stuck in wall
        // can refine this condition
        return room.Contains(pos + Vector2Int.up) && room.Contains(pos + Vector2Int.down) &&
            room.Contains(pos + Vector2Int.left) && room.Contains(pos + Vector2Int.right);
    }
}
