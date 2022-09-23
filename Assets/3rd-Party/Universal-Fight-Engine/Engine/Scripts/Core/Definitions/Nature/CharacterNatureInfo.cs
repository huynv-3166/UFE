using System.Collections.Generic;
using UnityEngine;

namespace TOHDragonFight3D
{
    [System.Serializable]
    public class CharacterNatureInfo : ScriptableObject
    {
        public CharacterNatureType natureType;
        public string natureName;
        public Sprite naturePicture;
        public string natureDescription;
        public float odds;
    }
}