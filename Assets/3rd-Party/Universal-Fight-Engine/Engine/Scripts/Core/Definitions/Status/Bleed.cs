using System.Collections;
using System.Collections.Generic;
using FPLibrary;
using UnityEngine;

namespace TOHDragonFight3D
{
    public class Bleed : MonoBehaviour, iStatus
    {
        #region Self trackable variables
        bool running = false;
        bool destroyOnEnd = true;
        float startRun = 0f;
        float lasttimeReduceHP = 0f;
        #endregion

        #region inheritate variables
        float duration = 1f;
        float amountReduce = 0.7f;
        ControlsScript player;
        #endregion

        #region public interface methods
        public void Init(ControlsScript player,
                         StatusResistanceInfo status,
                         bool DestroyOnEnd)
        {
            this.running = false;
            this.player = player;
            this.duration = status.duration;
            this.amountReduce = status.hpReduce;
            this.destroyOnEnd = DestroyOnEnd;
        }

        public bool IsRunning()
        {
            return running;
        }

        public void Run()
        {
            this.startRun = Time.time;
            lasttimeReduceHP = startRun;
            this.running = true;
        }
        #endregion

        #region Mono methods
        private void FixedUpdate()
        {
            if (!running) return;

            if (Time.time - lasttimeReduceHP >= 1f)
            {
                Fix64 damage = player.currentLifePoints * amountReduce;
                player.DamageMe(damage, true);
                lasttimeReduceHP = Time.time;
            }

            if (destroyOnEnd && Time.time - startRun > duration)
            {
                running = false;
                Destroy(this);
            }
        }
        #endregion
    }
}