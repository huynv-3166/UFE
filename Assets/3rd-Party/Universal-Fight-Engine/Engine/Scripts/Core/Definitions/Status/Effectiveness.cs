using UnityEngine;
using System;
using TOHDragonFight3D;

[System.Serializable]
public class Effectiveness : ICloneable
{
    public CharacterAttributeInfo targetAttribute;
    public float effectiveness = 30;

    #region trackable definitions
    public bool cancelable { get; set; }
    #endregion

    public object Clone()
    {
        return CloneObject.Clone(this);
    }
}