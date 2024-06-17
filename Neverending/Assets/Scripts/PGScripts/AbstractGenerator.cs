using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractGenerator : MonoBehaviour
{
    [SerializeField]
    protected TileMapVisualiser visualiser = null;
    [SerializeField]
    protected Vector2Int startPos = Vector2Int.zero;
    public void GenerateMap() {
        visualiser.Clear();
        RunPG();
    }

    protected abstract void RunPG();
}
