using UnityEngine;
using UnityEditor;
using System;
using TOHDragonFight3D;

public class AIAsset
{
	[MenuItem("Assets/Create/U.F.E./A.I. File")]
    public static void CreateAsset ()
    {
        ScriptableObjectUtility.CreateAsset<AIInfo>();
    }
}
