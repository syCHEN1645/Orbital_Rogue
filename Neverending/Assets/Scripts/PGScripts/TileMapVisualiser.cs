using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualiser: MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTileMap;
    [SerializeField]
    private TileBase floorTile;
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions) {
        PaintTiles(floorPositions, floorTileMap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> floorPositions, Tilemap floorTileMap, TileBase floorTile)
    {
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
    }
}
