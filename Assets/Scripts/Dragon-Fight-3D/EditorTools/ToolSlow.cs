#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace TOHDragonFight3D
{
    public class ToolSlow : MonoBehaviour
    {
        public StatusResistanceInfo slowStatus;

        [ContextMenu("Slow cScript 1")]
        void SlowPlayer1()
        {
            ControlsScript cScript = UFE.GetControlsScript(1);
            Slow.AddAndRunSlow(cScript, slowStatus);
        }

        [ContextMenu("Slow cScript 2")]
        void SlowPlayer2()
        {
            ControlsScript cScript = UFE.GetControlsScript(2);
            Slow.AddAndRunSlow(cScript, slowStatus);
        }

        [ContextMenu("DebugSlowStance")]
        void DebugSlowStance()
        {
            ControlsScript cScript1 = UFE.GetControlsScript(1);
            Debug.Log("Stance P1 : " + cScript1.currentCombatStance);
            ControlsScript cScript2 = UFE.GetControlsScript(2);
            Debug.Log("Stance P2 : " + cScript2.currentCombatStance);
        }

        [ContextMenu("Stop cScript1")]
        void StopSlowPlayer1()
        {
            ControlsScript cScript = UFE.GetControlsScript(1);
            Slow.StopSlow(cScript);
        }

        [ContextMenu("Slow cScript1 Forever")]
        void SlowPlayer1Forever()
        {
            ControlsScript cScript = UFE.GetControlsScript(1);
            Slow slow = Slow.AddAndRunSlow(cScript, slowStatus);
            slow.AddMoreSlow(9999f);
        }
    }
}
#endif