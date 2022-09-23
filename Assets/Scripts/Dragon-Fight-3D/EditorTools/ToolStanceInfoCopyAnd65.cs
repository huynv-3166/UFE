using System;
using System.Collections;
using System.Collections.Generic;
using FPLibrary;
using UnityEngine;

namespace TOHDragonFight3D
{
    public class ToolStanceInfoCopyAnd65 : MonoBehaviour
    {
        [SerializeField] StanceInfo copyStance;
        [SerializeField] StanceInfo pasteStance;
        [SerializeField] float slowStats = 0.65f;

        #region context methods
        [ContextMenu("Copy")]
        void Copy()
        {
            pasteStance.Sync(copyStance);
        }

        [ContextMenu("65")]
        void Do65()
        {
            Basic65(slowStats); // All animation speed to 65

            // Reduce all attack-moves
            //MoveInfo[] dashMove = Array.FindAll(pasteStance.attackMoves,
            //    dash => dash.moveName.ToLower().Contains("dash"));
            ReduceAttackMoves(pasteStance.attackMoves, slowStats);

            // Reduce basic attack moves

        }
        #endregion

        #region Private methods
        void Basic65(float speed = .65f)
        {
            pasteStance.basicMoves.idle._animationSpeed = speed;
            pasteStance.basicMoves.moveForward._animationSpeed = speed;
            pasteStance.basicMoves.moveBack._animationSpeed = speed;
            pasteStance.basicMoves.moveSideways._animationSpeed = speed;
            pasteStance.basicMoves.crouching._animationSpeed = speed;
            pasteStance.basicMoves.takeOff._animationSpeed = speed;
            pasteStance.basicMoves.jumpStraight._animationSpeed = speed;
            pasteStance.basicMoves.jumpBack._animationSpeed = speed;
            pasteStance.basicMoves.jumpForward._animationSpeed = speed;
            pasteStance.basicMoves.fallStraight._animationSpeed = speed;
            pasteStance.basicMoves.fallBack._animationSpeed = speed;
            pasteStance.basicMoves.fallForward._animationSpeed = speed;
            pasteStance.basicMoves.landing._animationSpeed = speed;
            pasteStance.basicMoves.blockingCrouchingPose._animationSpeed = speed;
            pasteStance.basicMoves.blockingCrouchingHit._animationSpeed = speed;
            pasteStance.basicMoves.blockingHighPose._animationSpeed = speed;
            pasteStance.basicMoves.blockingHighHit._animationSpeed = speed;
            pasteStance.basicMoves.blockingLowHit._animationSpeed = speed;
            pasteStance.basicMoves.blockingAirPose._animationSpeed = speed;
            pasteStance.basicMoves.blockingAirHit._animationSpeed = speed;
            pasteStance.basicMoves.parryCrouching._animationSpeed = speed;
            pasteStance.basicMoves.parryHigh._animationSpeed = speed;
            pasteStance.basicMoves.parryLow._animationSpeed = speed;
            pasteStance.basicMoves.parryAir._animationSpeed = speed;
            pasteStance.basicMoves.groundBounce._animationSpeed = speed;
            pasteStance.basicMoves.standingWallBounce._animationSpeed = speed;
            pasteStance.basicMoves.standingWallBounceKnockdown._animationSpeed = speed;
            pasteStance.basicMoves.airWallBounce._animationSpeed = speed;
            pasteStance.basicMoves.fallingFromGroundBounce._animationSpeed = speed;
            pasteStance.basicMoves.fallingFromAirHit._animationSpeed = speed;
            pasteStance.basicMoves.fallDown._animationSpeed = speed;
            pasteStance.basicMoves.airRecovery._animationSpeed = speed;
            pasteStance.basicMoves.getHitCrouching._animationSpeed = speed;
            pasteStance.basicMoves.getHitHigh._animationSpeed = speed;
            pasteStance.basicMoves.getHitLow._animationSpeed = speed;
            pasteStance.basicMoves.getHitHighKnockdown._animationSpeed = speed;
            pasteStance.basicMoves.getHitMidKnockdown._animationSpeed = speed;
            pasteStance.basicMoves.getHitAir._animationSpeed = speed;
            pasteStance.basicMoves.getHitCrumple._animationSpeed = speed;
            pasteStance.basicMoves.getHitKnockBack._animationSpeed = speed;
            pasteStance.basicMoves.getHitSweep._animationSpeed = speed;
            pasteStance.basicMoves.standUp._animationSpeed = speed;
            pasteStance.basicMoves.standUpFromAirHit._animationSpeed = speed;
            pasteStance.basicMoves.standUpFromKnockBack._animationSpeed = speed;
            pasteStance.basicMoves.standUpFromStandingHighHit._animationSpeed = speed;
            pasteStance.basicMoves.standUpFromStandingMidHit._animationSpeed = speed;
            pasteStance.basicMoves.standUpFromCrumple._animationSpeed = speed;
            pasteStance.basicMoves.standUpFromSweep._animationSpeed = speed;
            pasteStance.basicMoves.standUpFromStandingWallBounce._animationSpeed = speed;
            pasteStance.basicMoves.standUpFromAirWallBounce._animationSpeed = speed;
            pasteStance.basicMoves.standUpFromGroundBounce._animationSpeed = speed;
        }

        void ReduceAttackMoves(MoveInfo[] attacks, float slowStats = 0.65f)
        {
            Array.ForEach(attacks,
                patseAttack =>
                {
                    // Slow Apply Force
                    MoveInfo copyAttack = Array.Find(copyStance.attackMoves, cAttack => cAttack.moveName.Equals(patseAttack.moveName));
                    for (int i = 0; i < patseAttack.appliedForces.Length; i++)
                    {
                        // <5 too small to see the difference
                        //if (copyAttack.appliedForces[i]._force.magnitude > new Vector2(5, 0).magnitude)
                        //    patseAttack.appliedForces[i]._force = copyAttack.appliedForces[i]._force * slowStats;
                        //else
                        //    patseAttack.appliedForces[i]._force = copyAttack.appliedForces[i]._force;
                        patseAttack.appliedForces[i]._force = copyAttack.appliedForces[i]._force;
                    }

                    // Slow animation speed
                    patseAttack._animationSpeed = copyAttack._animationSpeed * slowStats;

                    int totalFramesTemp;
                    float animTime = 0;
                    int frameCounter = 0;
                    int currentKeyFrame = 0;
                    float frameSpeed = (float)patseAttack._animationSpeed;
                    do
                    {
                        frameCounter++;
                        int keyFrameCount = 0;
                        foreach (AnimSpeedKeyFrame speedKeyFrame in patseAttack.animSpeedKeyFrame)
                        {
                            keyFrameCount++;
                            if (frameCounter > speedKeyFrame.castingFrame && keyFrameCount > currentKeyFrame)
                            {
                                currentKeyFrame = keyFrameCount;
                                frameSpeed = (float)patseAttack._animationSpeed * (float)speedKeyFrame._speed;
                                break;
                            }
                        }
                        animTime += ((float)1 / patseAttack.fps) * frameSpeed;

                    } while (animTime < patseAttack.animMap.clip.length);
                    totalFramesTemp = frameCounter;
                    patseAttack.totalFrames = totalFramesTemp - 1;

                    // Slow hit's active frames
                    for (int i = 0; i < patseAttack.hits.Length; i++)
                    {
                        patseAttack.hits[i].activeFramesBegin = (int)Math.Round(copyAttack.hits[i].activeFramesBegin * ((float)patseAttack.totalFrames / (float)copyAttack.totalFrames));
                        patseAttack.hits[i].activeFramesEnds = (int)Math.Round(copyAttack.hits[i].activeFramesEnds * ((float)patseAttack.totalFrames / (float)copyAttack.totalFrames));
                    }
                });
        }
        #endregion
    }
}
