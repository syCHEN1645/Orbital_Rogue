using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleRandomWalkGenerator: AbstractGenerator
{
    [SerializeField]
    protected SimpleRandomWalkData randomWalkData;

    protected override void RunPG() {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkData, startPos);
        visualiser.Clear();
        // paint floors
        visualiser.PaintFloorTiles(floorPositions);
        // paint walls
        // WallGenerator.CreateWalls(floorPositions, visualiser);
        WallTypes.CreateWalls(floorPositions, visualiser);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkData randomWalkData, Vector2Int pos) {
        var currentPos = pos;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < randomWalkData.iterations; i++) {
            var path = PGAlgorithm.SimpleRandomWalk(currentPos, randomWalkData.walkLength);
            // copy everything non-duplicate and merge
            floorPositions.UnionWith(path);
            if (randomWalkData.startRandomlyEachGeneration) {
                // set currentPos to a random position on floor
                currentPos = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }    
        }
        return floorPositions;
    }
}
