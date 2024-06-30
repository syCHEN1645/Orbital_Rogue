using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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
            Vector2Int dir = Direction2D.GetRandomCardianlDirection();
            var newPos = previousPos + dir;
            path.Add(newPos);
            // can adjust path width
            AddWidthTo(path, newPos, dir, 2);
            previousPos = newPos;
        }
        return path;
    }

    public static HashSet<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corridorLength) {
        // return value is List, to get last element of corridor
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var dir = Direction2D.GetRandomCardianlDirection();
        var currentPos = startPos;
        for (int i = 0; i < corridorLength; i++) {
            currentPos += dir;
            AddWidthTo(corridor, currentPos, dir, 2);
            corridor.Add(currentPos);
        }
        return corridor;
    }

    private static void AddWidthTo(HashSet<Vector2Int> path, Vector2Int pos, Vector2Int dir, int x) {
        // widen path to x grids
        for (int i = 0; i < x; i++) {
            var widenPos = pos + Direction2D.CCW90(dir) * i;
            path.Add(widenPos);
        }
    }
}


public static class Direction2D {
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int> {
        // a list of basic directions
        new Vector2Int(1, 0), // right
        new Vector2Int(0, 1), // up
        new Vector2Int(-1, 0), // left
        new Vector2Int(0, -1), // down
    };

    public static Vector2Int GetRandomCardianlDirection() {
        // get a random basic direction from the list above
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
    }

    public static Vector2Int CCW90(Vector2Int dir) {
        /*
            add wid 90 degrees ccw
            right (1, 0) -> up (0, 1)
            up (0, 1) -> left (-1, 0)
            left -> down
            down -> right
        */
        int index = cardinalDirectionList.IndexOf(dir);
        index = (index + 1) % 4;
        return cardinalDirectionList[index];
    }
}