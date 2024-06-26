using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectGenerator
{
    protected List<GameObject> objectList;
    protected List<HashSet<Vector2Int>> roomsList;
    protected HashSet<Vector2Int> floorPositions;
    protected int level;
    public GameObjectGenerator() {
        // set parameters here

    }
    virtual protected void GenerateOneRoom(HashSet<Vector2Int> room) {

    }
    protected bool RandomBool() {
        // randomly generate a bool
        int x = Random.Range(0, 10);
        if (x <= 1) {
            return true;
        }
        return false;
    }

    protected int RandomInt(int min, int max) {
        // randomly generate an int
        return Random.Range(min, max + 1);
    }
}
