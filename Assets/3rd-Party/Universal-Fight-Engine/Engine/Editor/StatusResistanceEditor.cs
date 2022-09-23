using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(TOHDragonFight3D.StatusResistanceInfo))]
public class StatusResistanceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Open Status Resistance Editor"))
            StatusResistanceEditorWindow.Init();

    }
}
