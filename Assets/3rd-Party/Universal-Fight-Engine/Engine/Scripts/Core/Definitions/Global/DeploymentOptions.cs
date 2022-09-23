using UnityEngine;
using FPLibrary;
using System.Collections.Generic;

namespace TOHDragonFight3D
{
    public enum DeploymentType
    {
        FullInterface,
        VersusMode,
        TrainingMode,
        ChallengeMode
    }

    [System.Serializable]
    public class DeploymentOptions
    {
        public DeploymentType deploymentType;
        //public MatchType matchType;
        public List<CharacterInfo> activeCharacters = new List<CharacterInfo>() { null, null };
        public List<bool> AIControlled = new List<bool>() { false, false };

        public int selectedTeamMode = 0;
        public int selectedChallengeMode = 0;

        public bool skipLoadingScreen = false;
    }
}