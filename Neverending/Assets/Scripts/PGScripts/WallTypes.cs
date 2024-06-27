using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class WallTypes
{
    // see annotations in WallTypes
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TileMapVisualiser visualiser) {
        var wallPositions = FindWallsInDir(floorPositions, Direction2D.cardinalDirectionList);
        foreach (var pos in wallPositions) {
            string wallBinaryType = "";
            foreach (var dir in Direction2D.cardinalDirectionList) {
                var neighbourPos = pos + dir;
                if (floorPositions.Contains(neighbourPos)) {
                    // if floor tile, assgin 1
                    wallBinaryType += "1";
                } else {
                    // if empty tile, assign 0
                    wallBinaryType += "0";
                }
            }
            visualiser.PaintWallTile(pos, wallBinaryType);
        }
        
    }

    private static IEnumerable<Vector2Int> FindWallsInDir(HashSet<Vector2Int> floorPositions, List<Vector2Int> dirList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var pos in floorPositions) {
            foreach (var dir in dirList) {
                var neighbourPos = pos + dir;
                if (!floorPositions.Contains(neighbourPos)) {
                    wallPositions.Add(neighbourPos);
                }
            }
        }
        return wallPositions;
    }

    /*

         2
        3#1
         4

        0b1234

        #: current tile
        1: is a floor
        0: is empty tile

        dir: the smooth side of the wall tile is on the "dir" side
        e.g. wallSideTop: 
        ________
        |------|
        |      |
        |______|
        e.g. wallSideBottom: 
        ________
        |      |
        |______|
        |______|

    */
    public static HashSet<int> wallSideTop = new HashSet<int> {
        // tile 2 only is floor
        0b0100
    };
    public static HashSet<int> wallSideRight = new HashSet<int> {
        // tile 1 only is floor
        0b1000
    };
    public static HashSet<int> wallSideBottom = new HashSet<int> {
        // tile 4 only is floor
        0b0001
    };
    public static HashSet<int> wallSideLeft = new HashSet<int> {
        // tile 3 only is floor
        0b0010
    };
    public static HashSet<int> wallSideTopLeft = new HashSet<int> {
        // tiles 2 and 3 are floor
        0b0110
    };
    public static HashSet<int> wallSideBottomLeft = new HashSet<int> {
        // tiles 3 and 4 are floor
        0b0011
    };
    public static HashSet<int> wallSideTopRight = new HashSet<int> {
        // tiles 1 and 2 are floor
        0b1100
    };
    public static HashSet<int> wallSideBottomRight = new HashSet<int> {
        // tiles 1 and 4 are floor
        0b1001
    };
}
