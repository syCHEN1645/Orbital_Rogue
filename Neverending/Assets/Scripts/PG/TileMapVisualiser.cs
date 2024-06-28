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
    private TileBase floorTile, wallTile;
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
            PaintSingleTile(wallTileMap, wallTile, pos);
        }
    }
}
