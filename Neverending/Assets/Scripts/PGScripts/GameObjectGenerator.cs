using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectGenerator
{
    protected List<HashSet<Vector2Int>> roomsList;
    protected HashSet<Vector2Int> floorPositions;
    protected int level;
    virtual protected void Initialise() {
        // set parameters here

    }
    virtual protected void Generate() {

    }
}
