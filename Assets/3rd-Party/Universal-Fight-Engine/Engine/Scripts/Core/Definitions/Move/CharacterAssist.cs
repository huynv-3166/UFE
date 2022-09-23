using UnityEngine;
using System;
using FPLibrary;
using TOHDragonFight3D;

[System.Serializable]
public class CharacterAssist : ICloneable
{
    public FPVector _spawnPosition;
    public int castingFrame;
    public TOHDragonFight3D.CharacterInfo characterInfo;
    public MoveInfo enterMove;
    public MoveInfo exitMove;

    [HideInInspector] public bool animationPreview = false;

    #region trackable definitions
    public bool casted { get; set; }
    #endregion

    public object Clone()
    {
        return CloneObject.Clone(this);
    }
}
