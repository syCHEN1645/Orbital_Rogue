using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class RoomFirstGenerator : SimpleRandomWalkGenerator
{
    [SerializeField]
    private int minWidth = 4, minHeight = 4;
    [SerializeField]
    private int width = 20, height = 20;
    [SerializeField]
    [Range(0, 10)]
    // offset: gap between rooms
    private int offset = 1;
    [SerializeField]
    private bool useRandomWalk = false;

    protected override void RunPG()
    {
        visualiser.Clear();
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = PGAlgorithm.BinarySpacePartitioning(
            new BoundsInt((Vector3Int)startPos, new Vector3Int(width, height, 0)), 
            minWidth,
            minHeight);
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        foreach(var room in roomsList) {
            for (int col = offset; col < room.size.x - offset; col++) {
                for (int row = offset; row < room.size.y - offset; row++) {
                    // add rooms into floor to be painted
                    Vector2Int pos = (Vector2Int)room.min + new Vector2Int(col, row);
                    floorPositions.Add(pos);
                }
            }
        }
        
        visualiser.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, visualiser);
    }
}
