using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public static class PGAlgorithm
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int walkLength) {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPos);
        var previousPos = startPos;

        for (int i = 0; i < walkLength; i++) {
            // walk one step
            var newPos = previousPos + Direction2D.GetRandomCardianlDirection();
            path.Add(newPos);
            previousPos = newPos;
        }
        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corridorLength) {
        // return value is List, to get last element of corridor
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardianlDirection();
        var currentPos = startPos;
        for (int i = 0; i < corridorLength; i++) {
            currentPos += direction;
            corridor.Add(currentPos);
        }
        return corridor;
    }
}


public static class Direction2D {
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int> {
        // a list of basic directions
        new Vector2Int(0, 1), // up
        new Vector2Int(1, 0), // right
        new Vector2Int(0, -1), // down
        new Vector2Int(-1, 0), // left
    };

    public static Vector2Int GetRandomCardianlDirection() {
        // get a random basic direction from the list above
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
    }
}