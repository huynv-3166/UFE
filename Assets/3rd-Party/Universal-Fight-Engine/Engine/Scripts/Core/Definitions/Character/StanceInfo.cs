using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TOHDragonFight3D
{
    [System.Serializable]
    public class StanceInfo : ScriptableObject
    {
        public CombatStances combatStance = CombatStances.Stance1;
        public MoveInfo cinematicIntro;
        public MoveInfo cinematicOutro;

        public BasicMoves basicMoves = new BasicMoves();
        public MoveInfo[] attackMoves = new MoveInfo[0];
        public MoveInfo[] skillMoves = new MoveInfo[0];

        public MoveSetData ConvertData()
        {
            MoveSetData moveSet = new MoveSetData();
            moveSet.combatStance = this.combatStance;
            moveSet.cinematicIntro = this.cinematicIntro;
            moveSet.cinematicOutro = this.cinematicOutro;
            moveSet.basicMoves = this.basicMoves;
            moveSet.attackMoves = this.attackMoves;
            moveSet.skillMoves = this.skillMoves;

            return moveSet;
        }

        public void Sync(StanceInfo other)
        {
            //this.combatStance = other.combatStance;
            this.cinematicIntro = other.cinematicIntro;
            this.cinematicOutro = other.cinematicOutro;
            this.basicMoves = other.basicMoves;
            this.attackMoves = other.attackMoves;
            this.skillMoves = other.skillMoves;
        }
#if UNITY_EDITOR
        #region context methods
        //[ContextMenu("SetAllMovesScaleSpeedWithCharacterTrue")]
        //public void SetAllMovesScaleSpeedWithCharacterTrue()
        //{
        //basicMoves.AllScaleWithCharacterSpeed(true);
        //Array.ForEach(attackMoves, move => move.scaleCharacterSpeed = true);
        //Array.ForEach(skillMoves, move => move.scaleCharacterSpeed = true);
        //}

        [ContextMenu("SortAttacks")]
        void SortAttacks()
        {
            List<MoveInfo> attackList = attackMoves.ToList();
            attackList.Sort((a,b) => a.moveName.CompareTo(b.moveName));
            attackMoves = attackList.ToArray();
        }
        #endregion
#endif
    }
}