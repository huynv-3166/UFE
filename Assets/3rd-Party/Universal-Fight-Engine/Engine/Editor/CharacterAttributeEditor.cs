using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(TOHDragonFight3D.CharacterAttributeInfo))]
public class CharacterAttributeEditor : Editor {
	public override void OnInspectorGUI(){
		if (GUILayout.Button("Open Attribute Editor")) 
			CharacterAttributeEditorWindow.Init();
			
	}
}
