using UnityEngine;
using System;
using TOHDragonFight3D;

[System.Serializable]
public class MoveSetData : ICloneable
{
    public CombatStances combatStance = CombatStances.Stance1; // This move set combat stance
    public MoveInfo cinematicIntro;
    public MoveInfo cinematicOutro;

    public BasicMoves basicMoves = new BasicMoves(); // List of basic moves
    public MoveInfo[] attackMoves = new MoveInfo[0]; // List of attack moves
    public MoveInfo[] skillMoves = new MoveInfo[0]; // List of skill moves

    [HideInInspector] public bool enabledBasicMovesToggle;
    [HideInInspector] public bool basicMovesToggle;
    [HideInInspector] public bool attackMovesToggle;
    [HideInInspector] public bool skillMovesToggle;


    public StanceInfo ConvertData()
    {
        StanceInfo stanceData = new StanceInfo();
        stanceData.combatStance = this.combatStance;
        stanceData.cinematicIntro = this.cinematicIntro;
        stanceData.cinematicOutro = this.cinematicOutro;
        stanceData.basicMoves = this.basicMoves;
        stanceData.attackMoves = this.attackMoves;
        stanceData.skillMoves = this.skillMoves;

        return stanceData;
    }

    public object Clone()
    {
        return CloneObject.Clone(this);
    }
}