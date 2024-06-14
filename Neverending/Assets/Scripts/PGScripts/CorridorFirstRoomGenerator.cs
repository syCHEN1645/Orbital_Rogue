using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstRoomGenerator : SimpleRandomWalkGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    protected float roomPercent;

    protected override void RunPG()
    {
        // base.RunPG();
        GenerateCorridorFirstRoom();
    }

    private void GenerateCorridorFirstRoom()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
        
        CreateCorridor(floorPositions, potentialRoomPositions);

        // generate rooms from potentialRoonPositions
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);
        
        // find all dead ends and create a room there
        // now floorPositions include only the corridors
        List<Vector2Int> deadEnds = FindDeadEnds(floorPositions);
        // create rooms at dead ends and merge with roomPositions
        CreateRoomsDeadEnd(deadEnds, roomPositions);
        
        // merge new rooms into floorPositions (map)
        floorPositions.UnionWith(roomPositions);

        visualiser.PaintFloorTiles(floorPositions);
    }

    private void CreateRoomsDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloorPositions)
    {
        foreach (var pos in deadEnds) {
            // if no room is covering this dead end pos, then need to create a new room
            if (!roomFloorPositions.Contains(pos)) {
                // create a new room
                var room = RunRandomWalk(randomWalkData, pos);
                // merge this room into existing rooms collection
                roomFloorPositions.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var pos in floorPositions) {
            int neighboursCount = 0;
            foreach (var dir in Direction2D.cardinalDirectionList) {
                // if the tile pos + dir exists in floorPosition, then there is an open  end
                if (floorPositions.Contains(pos + dir)) {
                    neighboursCount++;
                }
            }
            // if there is only 1 open end for this tile at pos, it is a dead end
            if (neighboursCount == 1) {
                deadEnds.Add(pos);
            }
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomsToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);
        
        // Guid: globally unique identifier (a random id)
        // OrderBy(...): in a random order
        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomsToCreateCount).ToList();

        foreach(var roomPos in roomsToCreate) {
            // generate a room at this "roomPos"
            var roomFloor = RunRandomWalk(randomWalkData, roomPos);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private void CreateCorridor(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPos = startPos;
        potentialRoomPositions.Add(currentPos);
        
        for (int i = 0; i < corridorCount; i++) {
            // generate a number of corridor
            var corridor = PGAlgorithm.RandomWalkCorridor(currentPos, corridorLength);
            // currentPos = last tile of the corridor
            currentPos = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPos);
            // merge corridor into map
            floorPositions.UnionWith(corridor);
        }
    }
}
