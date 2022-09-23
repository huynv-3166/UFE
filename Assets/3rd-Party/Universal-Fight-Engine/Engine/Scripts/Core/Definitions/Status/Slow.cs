using System;
using System.Collections;
using System.Collections.Generic;
using FPLibrary;
using UnityEngine;

namespace TOHDragonFight3D
{
    public class Slow : MonoBehaviour, iStatus
    {
        #region command-pattern for restoring and update
        class SlowCommandBasicMove
        {
            BasicMoveInfo moveInfo;
            BasicMoveData moveData;

            SlowCommandBasicMove(BasicMoveInfo moveInfo)
            {
                this.moveInfo = moveInfo;
                moveData = new BasicMoveData();
                moveData._animationSpeed = moveInfo._animationSpeed;
            }

            public void Change(float slowStats)
            {
                this.moveInfo._animationSpeed *= slowStats;
            }

            public void Store()
            {
                this.moveInfo._animationSpeed = moveData._animationSpeed;
            }
        }
        #endregion

        #region Self trackable variables
        [HideInInspector] public bool running = false;
        bool destroyOnEnd = true;
        float startRun = 0f;
        List<SlowCommandBasicMove> basicCommands = new List<SlowCommandBasicMove>();
        #endregion

        #region inheritate variables
        float duration = 1f;
        [HideInInspector] public float slowStats = 0.65f;
        ControlsScript playercScript;
        #endregion

        #region public interface methods
        public void Init(ControlsScript playercScript,
                         StatusResistanceInfo status,
                         bool DestroyOnEnd)
        {
            this.running = false;
            this.playercScript = playercScript;
            this.duration = status.duration;
            this.slowStats = status.slowStats;
            this.destroyOnEnd = DestroyOnEnd;

            // Create slow-command for basic moves
            basicCommands = new List<SlowCommandBasicMove>();
        }

        public bool IsRunning()
        {
            return running;
        }

        public void Run()
        {
            this.startRun = Time.time;
            this.running = true;
            playercScript.MoveSet.ChangeMoveStances(CombatStances.Stance2);
        }

        public void Stop()
        {
            running = false;
            playercScript.MoveSet.ChangeMoveStances(CombatStances.Stance1);
            playercScript.MoveSet.PlayBasicMove(playercScript.MoveSet.basicMoves.idle, playercScript.MoveSet.basicMoves.idle.name, 0);
            playercScript.Physics.ForceGrounded();
            Destroy(this);
        }

        // Just add more slow time
        public void AddMoreSlow(float duration)
        {
            this.duration += duration;
        }
        #endregion

        #region Mono methods
        private void FixedUpdate()
        {
            if (!running) return;

            if (destroyOnEnd && Time.time - startRun > duration)
                Stop();
        }
        #endregion

        #region static methods
        public static Slow AddAndRunSlow(ControlsScript cScript, StatusResistanceInfo slowStatus)
        {
            Slow slow = cScript.gameObject.GetComponent<Slow>();
            if (slow == null)
            {
                slow = cScript.gameObject.AddComponent<Slow>();
                slow.Init(cScript, slowStatus, true);
                slow.Run();
            }
            else
            {
                slow.AddMoreSlow(slowStatus.duration);
            }
            return slow;
        }

        public static void StopSlow(ControlsScript cScript)
        {
            Slow slow = cScript.gameObject.GetComponent<Slow>();
            if (slow != null) slow.Stop();
        }
        #endregion
    }
}