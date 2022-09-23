using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(TOHDragonFight3D.CharacterNatureInfo))]
public class CharacterNatureEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Open Nature Editor"))
            CharacterNatureEditorWindow.Init();
    }
}
