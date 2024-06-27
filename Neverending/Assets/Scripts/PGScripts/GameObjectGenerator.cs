using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectGenerator
{
    protected GameObject[] objectList;
    protected List<HashSet<Vector2Int>> roomsList;
    protected HashSet<Vector2Int> floorPositions;
    protected int level;
    public GameObjectGenerator() {
        // set parameters here

    }
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
}
