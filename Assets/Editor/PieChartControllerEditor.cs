using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PieChartController))]
public class PieChartControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PieChartController pieChartController = (PieChartController)target;

        if (GUILayout.Button("Update Random"))
        {
            pieChartController.updateRandomPartyPower();
        }
    }
}


