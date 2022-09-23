using System;

namespace TOHDragonFight3D
{
    [Serializable]
    public class AbstractParticleEffect : ICloneable
    {
        public int castingFrame;
        public ParticleInfo particleEffect;

        #region trackable definitions
        public bool casted { get; set; }
        #endregion

        public object Clone()
        {
            return CloneObject.Clone(this);
        }
    }
}