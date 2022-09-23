using UnityEngine;
using System;
using System.Collections.Generic;

namespace TOHDragonFight3D
{
    [System.Serializable]
    public class ParticleInfo : ICloneable
    {
        public bool editorToggle;
        public IdentityType identity = IdentityType.self;
        public GameObject prefab;
        public float duration = 1;
        public bool stick = false;
        public bool destroyOnMoveOver = false;
        public bool followRotation = false;
        public bool lockLocalPosition = false;
        public bool mirrorOn2PSide = false;
        public bool overrideRotation = true;
        public Vector3 initialRotation;
        public Vector3 positionOffSet;
        public BodyPart bodyPart;

        public object Clone()
        {
            return CloneObject.Clone(this);
        }

        public string[] Difference(ParticleInfo other)
        {
            List<string> diffs = new List<string>();

            return diffs.ToArray();
        }
    }
}