using UnityEngine;
using UnityEditor;
using System;
using TOHDragonFight3D;

public class GlobalAsset
{
	[MenuItem("Assets/Create/U.F.E./Config File")]
    public static void CreateAsset ()
    {
        ScriptableObjectUtility.CreateAsset<GlobalInfo> ();
    }
}
