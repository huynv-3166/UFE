using System.Collections.Generic;
using UnityEngine;

namespace TOHDragonFight3D
{
    [System.Serializable]
    public class CharacterAttributeInfo : ScriptableObject
    {
        public CharacterAtrributeType atrributeType;
        public string attributeName;
        public Sprite attributePicture;
        public string attributeDescription;
        public float stab = 20;
        public Effectiveness[] effectLinks = new Effectiveness[0];
    }
}