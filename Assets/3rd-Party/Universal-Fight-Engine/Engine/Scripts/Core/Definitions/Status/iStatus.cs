using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOHDragonFight3D
{
    public interface iStatus
    {
        void Init(ControlsScript player,
                  StatusResistanceInfo status,
                  bool DestroyOnEnd);
        void Run();
        bool IsRunning();
    }
}