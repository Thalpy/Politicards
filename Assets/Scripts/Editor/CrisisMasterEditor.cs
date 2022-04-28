using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CrisisMaster))]
public class CrisisMasterEditor : Editor
{
     public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CrisisMaster myScript = (CrisisMaster)target;
        if(GUILayout.Button("List active crisises' progress")){
            myScript.SpeakActiveCrisisProgress();
        }
        if(GUILayout.Button("Run DEBUGCRISIS NAME")){
            myScript.StartCrisisFromText(myScript.DEBUGCRISIS);
        }
    }
}   