using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOHDragonFight3D
{
    public class ToolStanceInfo : MonoBehaviour
    {
        [SerializeField] StanceInfo copyStance;
        [SerializeField] StanceInfo pasteStance;

        [ContextMenu("CompareStances")]
        void CompareStances()
        {
            
        }
    }
}
