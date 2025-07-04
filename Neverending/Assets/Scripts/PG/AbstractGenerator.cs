using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractGenerator : MonoBehaviour
{
    [SerializeField]
    public TileMapVisualiser visualiser = null;
    [SerializeField]
    protected Vector2Int startPos = Vector2Int.zero;

    public void GenerateMap(bool editor) {
        visualiser.Clear();
        ClearOldMap(editor);
        RunPG();
    }

    protected abstract void RunPG();
    protected abstract void ClearOldMap(bool editor);
}
