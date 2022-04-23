using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(GameMaster))]
public class GameMasterEditor : Editor
{
    // on pressing the button, calls the TestPieChart method
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameMaster myScript = (GameMaster)target;
        if (GUILayout.Button("TestPieChart"))
        {
            myScript.TestPieChart();
        }
    }
}
