using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


// use this to generate walls
public static class WallTypes
{
    // see annotations in WallTypes
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TileMapVisualiser visualiser) {
        HashSet<Vector2Int> wallPositions = FindWallsInDir(floorPositions, Direction2D.cardinalDirectionList);
        
        HashSet<Vector2Int> cornerPositions = new HashSet<Vector2Int>();
        // draw walls
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
            

            /*
            1 2 3
            8 # 4
            7 6 5
            
            #: current tile
            1: is a floor
            0: is not a floor
            0b12345678
            */

            /*
              2
            3 # 1
              4
            
            #: current tile
            1: is a floor
            0: is not a floor
            0b1234
            */

            // draw corners
            // 0 1 1 1
            // 1
            // 1
            // 1
            // if up is not a wall, right is a floor, up + right is a wall, then up is a corner
            // corner AT topleft
            if (!wallPositions.Contains(pos + Vector2Int.up) &&
            !floorPositions.Contains(pos + Vector2Int.up) &&
            wallPositions.Contains(pos + Vector2Int.up + Vector2Int.right) && 
            floorPositions.Contains(pos + Vector2Int.right)) {
                cornerPositions.Add(pos + Vector2Int.up);
                // paint a corner
                visualiser.PaintWallTile(pos + Vector2Int.up, "10010");
                }
            // similarly, corner AT bottomleft
            // 1
            // 1
            // 0 1 1 1
            if (!wallPositions.Contains(pos + Vector2Int.down) &&
            !floorPositions.Contains(pos + Vector2Int.down) &&
            wallPositions.Contains(pos + Vector2Int.down + Vector2Int.right) && 
            floorPositions.Contains(pos + Vector2Int.right)) {
                cornerPositions.Add(pos + Vector2Int.down);
                // paint corner
                visualiser.PaintWallTile(pos + Vector2Int.down, "10000");
            }
            // if up is not a wall, and up + left is a wall, then up is a corner
            // 1 1 0
            //     1
            //     1
            // corner AT topright
            if (!wallPositions.Contains(pos + Vector2Int.up) && 
            !floorPositions.Contains(pos + Vector2Int.up) &&
            wallPositions.Contains(pos + Vector2Int.up + Vector2Int.left) && 
            floorPositions.Contains(pos + Vector2Int.left)) {
                cornerPositions.Add(pos + Vector2Int.up);
                // paint corner
                visualiser.PaintWallTile(pos + Vector2Int.up, "10011");
                }
            // similarly, corner AT bottomright
            //     1
            //     1
            // 1 1 0
            if (!wallPositions.Contains(pos + Vector2Int.down) &&
            !floorPositions.Contains(pos + Vector2Int.down) &&
            wallPositions.Contains(pos + Vector2Int.down + Vector2Int.left) && 
            floorPositions.Contains(pos + Vector2Int.left)) {
                cornerPositions.Add(pos + Vector2Int.down);
                // paint corner
                visualiser.PaintWallTile(pos + Vector2Int.down, "10001");
            }
        }
        // merge corners into walls
        wallPositions.UnionWith(cornerPositions);
    }

    private static HashSet<Vector2Int> FindWallsInDir(HashSet<Vector2Int> floorPositions, List<Vector2Int> dirList)
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
        whether 1/2/3/4 is a floor

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

          2
        3 # 1
          4
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
    public static HashSet<int> bottomRightCorner = new HashSet<int> {
        // tiles 1 and 4 are floor
        0b10001
    };
    public static HashSet<int> bottomLeftCorner = new HashSet<int> {
        // tiles 1 and 4 are floor
        0b10000
    };
    public static HashSet<int> topLeftCorner = new HashSet<int> {
        // tiles 1 and 4 are floor
        0b10010
    };
    public static HashSet<int> topRightCorner = new HashSet<int> {
        // tiles 1 and 4 are floor
        0b10011
    };
}
