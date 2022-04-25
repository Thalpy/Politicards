using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FactionController))]
public class FactionControllerEditor : Editor
{
    // on pressing a button calls the SendRandomMana method
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FactionController myScript = (FactionController)target;
        if (GUILayout.Button("Send Random Mana"))
        {
            myScript.SendRandomMana();
        }

        if (GUILayout.Button("Send Random Happiness"))
        {
            myScript.ChangeRandomPower();
        }
    }
}