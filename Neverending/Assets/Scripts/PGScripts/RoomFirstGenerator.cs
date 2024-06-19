using System.Collections;
using System.Collections.Generic;
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

        // connect approximate centres of rooms through corridors
        List<Vector2Int> roomCentres = new List<Vector2Int>();
        foreach (var room in roomsList) {
            roomCentres.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCentres);
        floorPositions.UnionWith(corridors);
        
        visualiser.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, visualiser);
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCentres)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        // get a random room centre
        var currentCentre =  roomCentres[Random.Range(0, roomCentres.Count)];
        roomCentres.Remove(currentCentre);

        while (roomCentres.Count > 0) {
            Vector2Int nearestCentre = FindNearestCentreTo(currentCentre, roomCentres);
            roomCentres.Remove(nearestCentre);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentCentre, nearestCentre);
            // set the currentCentre to nearestCentre
            currentCentre = nearestCentre;
            corridor.UnionWith(newCorridor);
        }
        return corridor;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int start, Vector2Int des)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var pos = start;
        corridor.Add(pos);
        while (pos.y != des.y) {
            if (des.y > pos.y) {
                pos += Vector2Int.up;
            } else if (des.y < pos.y) {
                pos += Vector2Int.down;
            }
            corridor.Add(pos);
        }
        while (pos.x != des.x) {
            if (des.x > pos.x) {
                pos += Vector2Int.right;
            } else if (des.x < pos.x) {
                pos += Vector2Int.left;
            }
            corridor.Add(pos);
        }
        // here: has just reached des
        return corridor;
    }

    private Vector2Int FindNearestCentreTo(Vector2Int currentCentre, List<Vector2Int> roomCentres)
    {
        Vector2Int nearestCentre = Vector2Int.zero;
        float length = float.MaxValue;
        foreach (var centre in roomCentres) {
            // find smallest distance
            float currentDistance = Vector2.Distance(centre, currentCentre);
            if (currentDistance < length) {
                length = currentDistance;
                nearestCentre = centre;
            }
        }
        return nearestCentre;
    }
}
