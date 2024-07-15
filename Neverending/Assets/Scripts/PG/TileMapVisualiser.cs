using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualiser: MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTileMap, wallTileMap;
    [SerializeField]
    private List<TileBase> floorTile, wallGenericTile, wallBottom, wallTop, 
        wallRight, wallLeft, wallBottomRight, wallBottomLeft, 
        wallTopRight, wallTopLeft, cornerBottomLeft, cornerBottomRight, testTile;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions) {
        foreach(var pos in floorPositions) {
            PaintSingleTile(floorTileMap, GetRandomTile(floorTile), pos);
        }
    }

    public void TestPaint(HashSet<Vector2Int> floors) {
        foreach(var pos in floors) {
            PaintSingleTile(floorTileMap, GetRandomTile(testTile), pos);
        }
    }

    public void PaintSingleTile(Tilemap floorTileMap, TileBase floorTile, Vector2Int pos)
    {
        // convert world pos to cell pos
        var tilePosition = floorTileMap.WorldToCell((Vector3Int)pos);
        floorTileMap.SetTile(tilePosition, floorTile);
    }

    public TileBase GetRandomTile(List<TileBase> tileBases) {
        return tileBases[UnityEngine.Random.Range(0, tileBases.Count)];
    }

    public void Clear()
    {
        floorTileMap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
    }

    public void PaintWallTiles(IEnumerable<Vector2Int> wallPositions)
    {
        foreach (var pos in wallPositions) {
            PaintSingleTile(wallTileMap, GetRandomTile(wallGenericTile), pos);
        }
    }

    public void PaintWallTile(Vector2Int pos, string bType) {
        // convert from binary to dec int
        int typeAsInt = Convert.ToInt32(bType, 2);
        TileBase tileBase = null;
        // check through each wall type
        if (WallTypes.wallSideTop.Contains(typeAsInt)) {
            tileBase = GetRandomTile(wallTop);
        } else if (WallTypes.wallSideBottom.Contains(typeAsInt)) {
            tileBase = GetRandomTile(wallBottom);
        } else if (WallTypes.wallSideLeft.Contains(typeAsInt)) {
            tileBase = GetRandomTile(wallLeft);
        } else if (WallTypes.wallSideRight.Contains(typeAsInt)) {
            tileBase = GetRandomTile(wallRight);
        } else if (WallTypes.wallSideBottomLeft.Contains(typeAsInt)) {
            tileBase = GetRandomTile(wallBottomLeft);
        } else if (WallTypes.wallSideBottomRight.Contains(typeAsInt)) {
            tileBase = GetRandomTile(wallBottomRight);
        } else if (WallTypes.wallSideTopLeft.Contains(typeAsInt)) {
            tileBase = GetRandomTile(wallTopLeft);
        } else if (WallTypes.wallSideTopRight.Contains(typeAsInt)) {
            tileBase = GetRandomTile(wallTopRight);
        } else if (WallTypes.bottomLeftCorner.Contains(typeAsInt)) {
            tileBase = GetRandomTile(cornerBottomLeft);
        } else if (WallTypes.bottomRightCorner.Contains(typeAsInt)) {
            tileBase = GetRandomTile(cornerBottomRight);
        } else {
            tileBase = GetRandomTile(wallGenericTile);
        }
        if (tileBase != null) {
            PaintSingleTile(wallTileMap, tileBase, pos);
        }
    }
}
