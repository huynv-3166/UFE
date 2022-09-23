using System.Collections.Generic;
using UnityEngine;

namespace TOHDragonFight3D
{
    [System.Serializable]
    public class StatusResistanceInfo : ScriptableObject
    {
        public StatusResistanceType resistanceType;
        public StatusResistanceGroup resistanceGroup;
        public string resistanceName;
        public Sprite resistancePicture;
        public string resistanceDescription;
        public int maxCastingFrame;
        public int startUpFrames = 0;
        public int activeFrames = 1;
        public int recoveryFrames = 2;

        #region vfx
        public AbstractParticleEffect[] particleEffects = new AbstractParticleEffect[0];
        #endregion

        #region ability
        public float duration = 3f; // Time the status will last
        public bool canReduceHp = false;
        public float hpReduce = 0.7f; // Bleed : Reduce directly HP by 0.7%/1s
        public bool canSlow = false;
        public float slowStats = 0.65f; // Slow : Reduce speed by 35%
        #endregion
    }
}