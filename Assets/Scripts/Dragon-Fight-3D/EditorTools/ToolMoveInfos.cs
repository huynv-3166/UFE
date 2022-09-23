using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOHDragonFight3D
{
    public class ToolMoveInfos : MonoBehaviour
    {
        #region properties
        ToolMoveInfosDebug syncDebug
        {
            get
            {
                if (_syncDebug == null) _syncDebug = GetComponent<ToolMoveInfosDebug>();
                return _syncDebug;
            }
        }
        ToolMoveInfosDebug _syncDebug;
        #endregion
        [SerializeField] MoveInfo[] copyMoves = new MoveInfo[0];
        [SerializeField] MoveInfo[] pasteMoves = new MoveInfo[0];

        [ContextMenu("SyncAll")]
        void SyncAll()
        {
            if (syncDebug.MoveAttribute) SyncMoveAttributes();
            if (syncDebug.Gauges) SyncGauges();
            //if (syncDebug.Animation) SyncAnimation();
            if (syncDebug.ActiveFrames) SyncActiveFrames();
            if (syncDebug.OpponentOverride) SyncOpponentOverride();
            if (syncDebug.CharacterAssist) SyncCharacterAssist();
            if (syncDebug.PlayerConditions) SyncPlayerConditions();
            if (syncDebug.Input) SyncInputs();
            if (syncDebug.ChainMoves) SyncChainMoves();
            if (syncDebug.CinamaticOptions) SyncCinematicOptions();
            if (syncDebug.ParticleEffects) SyncParticaleEffects();
            if (syncDebug.SoundEffects) SyncSoundEffects();
            if (syncDebug.TextAlerts) SyncTextAlert();
            if (syncDebug.SortLayer) SyncSortLayer();
            if (syncDebug.StanceChanges) SyncStanceChanges();
            if (syncDebug.SelfAppliedForces) SyncSelfAppiedForces();
            if (syncDebug.BodyPartsVisibilityChanges) SyncBodyPartsVisibilityChanges();
            if (syncDebug.SlowMotionEffects) SyncSlowMotionEffects();
            if (syncDebug.ArmorOptions) SyncArmorOptions();
            if (syncDebug.SwitchCharacterOptions) SyncSwitchCharacterOptions();
            if (syncDebug.InvincibleFrames) SyncInvincibleFrames();
            if (syncDebug.StateOverride) SyncStateOverride();
            if (syncDebug.Projectiles) SyncProjectiles();
            if (syncDebug.AIDefinitions) SyncAIDefinitions();
            Debug.Log("Done Sync All");
        }
        void SyncMoveAttributes()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
                pasteMoves[i].moveAtrributes = copyMoves[i].moveAtrributes;
        }
        void SyncGauges()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
                pasteMoves[i].gauges = copyMoves[i].gauges;
        }
        //void SyncAnimation()
        //{
        //    if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
        //        copyMoves.Length != pasteMoves.Length) return;
        //    // Copy hit conditions
        //    for (int i = 0; i < copyMoves.Length; i++)
        //    {
        //        pasteMoves[i].animMap = copyMoves[i].animMap;
        //    }
        //}
        void SyncActiveFrames()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
            {
                pasteMoves[i].hits = copyMoves[i].hits;
                pasteMoves[i].blockableArea = copyMoves[i].blockableArea;
            }
        }
        void SyncOpponentOverride()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
            {
                pasteMoves[i].opponentOverride = copyMoves[i].opponentOverride;
            }
        }
        void SyncCharacterAssist()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
            {
                pasteMoves[i].characterAssist = copyMoves[i].characterAssist;
            }
        }
        void SyncPlayerConditions()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
            {
                pasteMoves[i].selfConditions = copyMoves[i].selfConditions;
                pasteMoves[i].opponentConditions = copyMoves[i].opponentConditions;
            }
        }
        void SyncInputs()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
            {
                pasteMoves[i].defaultInputs = copyMoves[i].defaultInputs;
                pasteMoves[i].altInputs = copyMoves[i].altInputs;
                pasteMoves[i].simulatedInputs = copyMoves[i].simulatedInputs;
            }
        }
        void SyncChainMoves()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
            {
                pasteMoves[i].previousMoves = copyMoves[i].previousMoves;
                pasteMoves[i].frameLinks = copyMoves[i].frameLinks;
            }
        }
        void SyncCinematicOptions()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
            {
                pasteMoves[i].cameraMovements = copyMoves[i].cameraMovements;
            }
        }
        void SyncParticaleEffects()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
                pasteMoves[i].particleEffects = copyMoves[i].particleEffects;
        }
        void SyncSoundEffects()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
                pasteMoves[i].soundEffects = copyMoves[i].soundEffects;
        }
        void SyncTextAlert()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
                pasteMoves[i].inGameAlert = copyMoves[i].inGameAlert;
        }
        void SyncSortLayer()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
                pasteMoves[i].sortOrder = copyMoves[i].sortOrder;
        }
        void SyncStanceChanges()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
                pasteMoves[i].stanceChanges = copyMoves[i].stanceChanges;
        }
        void SyncSelfAppiedForces()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
                pasteMoves[i].appliedForces = copyMoves[i].appliedForces;
        }
        void SyncBodyPartsVisibilityChanges()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
                pasteMoves[i].bodyPartVisibilityChanges = copyMoves[i].bodyPartVisibilityChanges;
        }
        void SyncSlowMotionEffects()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
                pasteMoves[i].slowMoEffects = copyMoves[i].slowMoEffects;
        }
        void SyncArmorOptions()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
                pasteMoves[i].armorOptions = copyMoves[i].armorOptions;
        }
        void SyncSwitchCharacterOptions()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
                pasteMoves[i].switchCharacterOptions = copyMoves[i].switchCharacterOptions;
        }
        void SyncInvincibleFrames()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
                pasteMoves[i].invincibleBodyParts = copyMoves[i].invincibleBodyParts;
        }
        void SyncStateOverride()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
                pasteMoves[i].stateOverride = copyMoves[i].stateOverride;
        }
        void SyncProjectiles()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
                pasteMoves[i].projectiles = copyMoves[i].projectiles;
        }
        void SyncAIDefinitions()
        {
            if (copyMoves.Length == 0 || pasteMoves.Length == 0 ||
                copyMoves.Length != pasteMoves.Length) return;
            // Copy hit conditions
            for (int i = 0; i < copyMoves.Length; i++)
                pasteMoves[i].moveClassification = copyMoves[i].moveClassification;
        }
    }
}
