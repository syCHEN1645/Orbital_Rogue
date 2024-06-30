using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// obsolete
public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TileMapVisualiser visualiser) {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionList);
        visualiser.PaintWallTiles(basicWallPositions);
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach(var pos in floorPositions) {
            // loop through every floor
            foreach(var dir in directionList) {
                // loop through every neighbour pos of floor
                var neighbourPos = pos + dir;
                if (!floorPositions.Contains(neighbourPos)) {
                    // if this neighbour pos is not a floor, then it should be a wall
                    wallPositions.Add(neighbourPos);
                }
            }
        }
        return wallPositions;
    }
}
