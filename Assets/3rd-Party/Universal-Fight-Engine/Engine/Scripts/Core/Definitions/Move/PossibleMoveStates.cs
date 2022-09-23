using System;

namespace TOHDragonFight3D
{
    [System.Serializable]
    public class PossibleMoveStates : ICloneable
    {
        public PossibleStates possibleState;
        public JumpArc jumpArc;
        public int jumpArcBegins = 0;
        public int jumpArcEnds = 100;

        public CharacterDistance opponentDistance;
        public float proximityRangeBegins = 0f;
        public float proximityRangeEnds = 100f;

        public bool movingForward = true;
        public bool movingBack = true;

        public bool standBy = true;
        public bool blocking;
        public bool stunned;

        public PossibleSide possibleSide;

        public object Clone()
        {
            return CloneObject.Clone(this);
        }
    }
}