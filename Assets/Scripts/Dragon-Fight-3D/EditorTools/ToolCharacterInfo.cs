using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOHDragonFight3D
{
    public class ToolCharacterInfo : MonoBehaviour
    {
        #region properties
        ToolCharacterInfoDebug syncDebug
        {
            get
            {
                if (_syncDebug == null) _syncDebug = GetComponent<ToolCharacterInfoDebug>();
                return _syncDebug;
            }
        }
        ToolCharacterInfoDebug _syncDebug;
        #endregion

        [SerializeField] CharacterInfo copyCharacter;
        [SerializeField] CharacterInfo pasteCharacter;

        [ContextMenu("SyncAll")]
        void SyncAll()
        {
            if (syncDebug.StatisticsInfo) SyncStatisticsInfo();

        }
        void SyncStatisticsInfo()
        {
            if (copyCharacter == null || pasteCharacter == null) return;
            //pasteCharacter. = copyCharacter.hits;
        }
    }
}
