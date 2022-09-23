using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOHDragonFight3D
{
    public class ToolMoveInfo : MonoBehaviour
    {
        [SerializeField] MoveInfo copyMove;
        [SerializeField] MoveInfo pasteMove;

        [ContextMenu("SyncAll")]
        void SyncAll()
        {
            SyncActiveFrames();
        }

        [ContextMenu("SyncBasicAttack")]
        void SyncActiveFrames()
        {
            if (copyMove == null || pasteMove == null) return;
            // Copy hit conditions
            pasteMove.hits = copyMove.hits;
        }
    }
}
