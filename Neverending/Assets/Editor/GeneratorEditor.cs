using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractGenerator), true)]
public class GeneratorEditor : Editor
{
    AbstractGenerator generator;
    private void Awake() {
        // target: object being inspected
        generator = (AbstractGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        // if the button Create Map is clicked
        if (GUILayout.Button("Create Map")){
            generator.GenerateMap();
        }
    }
}
