using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualiser: MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTileMap, wallTileMap;
    [SerializeField]
    private TileBase floorTile, wallGenericTile, wallTop, wallBottom, 
        wallLeft, wallRight, wallTopleft, wallTopRight, 
        wallBottomLeft, wallBottomRight;
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions) {
        foreach(var pos in floorPositions) {
            PaintSingleTile(floorTileMap, floorTile, pos);
        }
    }

    private void PaintSingleTile(Tilemap floorTileMap, TileBase floorTile, Vector2Int pos)
    {
        // convert world pos to cell pos
        var tilePosition = floorTileMap.WorldToCell((Vector3Int)pos);
        floorTileMap.SetTile(tilePosition, floorTile);
    }

    public void Clear()
    {
        floorTileMap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
    }

    public void PaintWallTiles(IEnumerable<Vector2Int> wallPositions)
    {
        foreach (var pos in wallPositions) {
            PaintSingleTile(wallTileMap, wallGenericTile, pos);
        }
    }

    public void PaintWallTile(Vector2Int pos, string bType) {
        // convert from binary to dec int
        int typeAsInt = Convert.ToInt32(bType, 2);
        TileBase tileBase = null;
        // check through each wall type
        if (WallTypes.wallSideTop.Contains(typeAsInt)) {
            tileBase = wallTop;
        } else if (WallTypes.wallSideBottom.Contains(typeAsInt)) {
            tileBase = wallBottom;
        } else if (WallTypes.wallSideLeft.Contains(typeAsInt)) {
            tileBase = wallLeft;
        } else if (WallTypes.wallSideRight.Contains(typeAsInt)) {
            tileBase = wallRight;
        } else if (WallTypes.wallSideBottomLeft.Contains(typeAsInt)) {
            tileBase = wallBottomLeft;
        } else if (WallTypes.wallSideBottomRight.Contains(typeAsInt)) {
            tileBase = wallBottomRight;
        } else if (WallTypes.wallSideTopLeft.Contains(typeAsInt)) {
            tileBase = wallTopleft;
        } else if (WallTypes.wallSideTopRight.Contains(typeAsInt)) {
            tileBase = wallTopRight;
        } else {
            tileBase = wallGenericTile;
        }
        if (tileBase != null) {
            PaintSingleTile(wallTileMap, tileBase, pos);
        }
    }
}
