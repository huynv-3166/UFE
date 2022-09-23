using UnityEngine;
using FPLibrary;

namespace TOHDragonFight3D
{
    [System.Serializable]
    public class CharacterInfo : ScriptableObject
    {
        public GameplayType gameplayType;
        public Texture2D profilePictureSmall;
        public Texture2D profilePictureBig;
        public string characterName;
        public int rareness = 1;
        public Gender gender;
        public string characterDescription;
        public CharacterAttributeInfo[] characterAtrributes = new CharacterAttributeInfo[0];
        public StatusResistanceInfo[] statusResistances = new StatusResistanceInfo[0];
        public StatusResistanceInfo[] currentImpact = new StatusResistanceInfo[0];
        public AnimationClip selectionAnimation;
        public AudioClip selectionSound;
        public AudioClip deathSound;
        public float height;
        public float size;
        public int age;
        public string bloodType;
        public int levelPoints = 1;
        public int healthPoints = 7345;
        public int attackPoints = 532;
        public int luckPoints = 20;
        public int critPoints = 150;
        public int speedPoints = 110;
        public int reducedamagePoints = 23; // Percent
        public CharacterNatureInfo[] natures = new CharacterNatureInfo[0];
        public int maxEPS; //Experience
        public StorageMode characterPrefabStorage = StorageMode.Prefab;
        public GameObject characterPrefab; // The prefab representing the character (must have hitBoxScript attached to it)
        public string prefabResourcePath; // Resource Path alternative loading
        public AltCostume[] alternativeCostumes = new AltCostume[0];
        public FPVector initialPosition;
        public FPQuaternion initialRotation;

        public PhysicsData physics;
        public HeadLook headLook;
        public CustomControls customControls;
        public bool[] hideGauges = new bool[10];

        public Fix64 _executionTiming = .3; // How fast the player needs to press each key during the execution of a special move
        public int possibleAirMoves = 1; // How many moves this character can perform while in the air
        public Fix64 _blendingTime = .1; // The speed of transiction between basic moves

        public AnimationType animationType;
        public Avatar avatar; // Mecanim variable
        public bool useScaleFlip; // Mecanim variable
        public bool applyRootMotion; // Mecanim variable
        public AnimationFlow animationFlow;
        public bool useAnimationMaps;
        public bool normalizeAnimationFrames;

        public string[] stanceResourcePath = new string[0];
        public MoveSetData[] moves = new MoveSetData[0];
        public AIInstructionsSet[] aiInstructionsSet = new AIInstructionsSet[0];
    }
}