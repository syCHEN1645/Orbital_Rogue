using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_", menuName = "PG/SimpleRandomWalkData")]
public class SimpleRandomWalkData : ScriptableObject
{
    public int iterations = 8, walkLength = 8, minStepEachTime = 3, walkWidth = 4;
    public bool startRandomlyEachGeneration = true;
}
