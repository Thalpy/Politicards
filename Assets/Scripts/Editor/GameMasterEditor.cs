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
        
        List<Effect> effects = myScript.GetEffects();
        List<Trigger> triggers = myScript.GetTriggers();
        EditorGUILayout.BeginHorizontal();  
        EditorGUILayout.LabelField("Effects:", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();
        //displays the effects in the inspector UI
        EditorGUILayout.BeginHorizontal();  
        EditorGUILayout.LabelField("Name of effect", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Object name",  EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();


        for (int i = 0; i < effects.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();  
            EditorGUILayout.LabelField(effects[i].name);
            //get the type of the object
            EditorGUILayout.LabelField(effects[i].GetType().ToString());
            EditorGUILayout.EndHorizontal();
        }
        //Adds a small break in the UI
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        //Triggers in bold
        EditorGUILayout.LabelField("Triggers:",  EditorStyles.boldLabel);        
        EditorGUILayout.EndHorizontal();
        //Same as above but for triggers
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Name of trigger",  EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Object name", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();
        
        for (int i = 0; i < triggers.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(triggers[i].name);
            EditorGUILayout.LabelField(triggers[i].GetType().ToString());
            EditorGUILayout.EndHorizontal();
        }       
    }
}
