using System.Collections.Generic;
using UnityEngine;

namespace TOHDragonFight3D
{
    [System.Serializable]
    public class MoveAttributeInfo : ScriptableObject
    {
        public MoveAtrributeType atrributeType;
        public string attributeName;
        public Sprite attributePicture;
        public string attributeDescription;
        public Effectiveness[] effectLinks = new Effectiveness[0];
    }
}