using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using FPLibrary;
using TOHDragonFight3D;

public class CharacterAttributeEditorWindow : EditorWindow
{
    public static CharacterAttributeEditorWindow characterattributeEditorWindow;
    public static TOHDragonFight3D.CharacterAttributeInfo sentAttributeInfo;
    private TOHDragonFight3D.CharacterAttributeInfo attributeInfo;
    private Dictionary<int, MoveSetData> instantiatedMoveSet = new Dictionary<int, MoveSetData>();

    private Vector2 scrollPos;
    private GameObject character;
    private HitBoxesScript hitBoxesScript;
    private bool characterPreviewToggle;

    private bool basicInfoOption;
    private int bloodTypeChoice;
    private string[] bloodTypeChoices = new string[] { "Unknown", "O-", "O+", "A-", "A+", "B-", "B+", "AB-", "AB+" };

    private int selectedPrefabIndex;
    private GameObject selectedPrefab;
    private string prefabResourcePath;

    private bool effectiveOptions;
    private bool prefabsOption;
    private bool hitBoxesOption;
    private bool hitBoxesToggle;
    private bool bendingSegmentsToggle;
    private bool nonAffectedJointsToggle;
    private bool altCostumesToggle;

    private bool physicsOption;
    private bool headLookOption;
    private bool customControlsOption;
    private bool gaugeDisplayOptions;
    private bool inputOptions;
    private bool moveSetOption;
    private bool aiInstructionsOption;
    private bool characterWarning;
    private string errorMsg;


    private string titleStyle;
    private string addButtonStyle;
    private string rootGroupStyle;
    private string subGroupStyle;
    private string arrayElementStyle;
    private string subArrayElementStyle;
    private string toggleStyle;
    private string foldStyle;
    private string enumStyle;

    [MenuItem("Window/U.F.E./Character Attribute Editor")]
    public static void Init()
    {
        characterattributeEditorWindow = EditorWindow.GetWindow<CharacterAttributeEditorWindow>(false, "Character Attribute", true);
        characterattributeEditorWindow.Show();
        characterattributeEditorWindow.Populate();
    }

    void OnSelectionChange()
    {
        Populate();
        Repaint();
    }

    void OnEnable()
    {
        Populate();
    }

    void OnFocus()
    {
        Populate();
    }

    void OnDisable()
    {
        ClosePreview();
    }

    void OnDestroy()
    {
        ClosePreview();
    }

    void OnLostFocus()
    {
        //ClosePreview();
    }

    public void ClosePreview()
    {
        characterPreviewToggle = false;
        if (character != null)
        {
            DestroyImmediate(character);
            character = null;
        }
        selectedPrefab = null;
        hitBoxesScript = null;
    }

    void helpButton(string page)
    {
        if (GUILayout.Button("?", GUILayout.Width(18), GUILayout.Height(18)))
            Application.OpenURL("http://www.ufe3d.com/doku.php/" + page);
    }

    void Update()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode && character != null)
        {
            ClosePreview();
        }
    }

    void Populate()
    {
        this.titleContent = new GUIContent("Character Attribute", (Texture)Resources.Load("Icons/Square"));

        // Style Definitions
        titleStyle = "MeTransOffRight";
        addButtonStyle = "CN CountBadge";
        rootGroupStyle = "GroupBox";
        subGroupStyle = "ObjectFieldThumb";
        arrayElementStyle = "FrameBox";
        subArrayElementStyle = "HelpBox";
        foldStyle = "Foldout";
        enumStyle = "MiniPopup";
        toggleStyle = "BoldToggle";


#if UFE_LITE
		UFE.isAiAddonInstalled = false;
#else
        UFE.isAiAddonInstalled = UFE.IsInstalled("RuleBasedAI");
#endif
        UFE.isControlFreak2Installed = UFE.IsInstalled("ControlFreak2.UFEBridge");

        if (sentAttributeInfo != null)
        {
            EditorGUIUtility.PingObject(sentAttributeInfo);
            Selection.activeObject = sentAttributeInfo;
            sentAttributeInfo = null;
        }

        UnityEngine.Object[] selection = Selection.GetFiltered(typeof(TOHDragonFight3D.CharacterAttributeInfo), SelectionMode.Assets);
        if (selection.Length > 0)
        {
            if (selection[0] == null) return;
            attributeInfo = (TOHDragonFight3D.CharacterAttributeInfo)selection[0];
        }
    }

    public void OnGUI()
    {
        if (attributeInfo == null)
        {
            GUILayout.BeginHorizontal("GroupBox");
            GUILayout.Label("Select a attribute file\nor create a new attribute.", "CN EntryInfo");
            GUILayout.EndHorizontal();
            EditorGUILayout.Space();
            if (GUILayout.Button("Create new attribute"))
                ScriptableObjectUtility.CreateAsset<TOHDragonFight3D.CharacterAttributeInfo>();
            return;
        }

        GUIStyle fontStyle = new GUIStyle();
        fontStyle.font = (Font)Resources.Load("EditorFont");
        fontStyle.fontSize = 30;
        fontStyle.alignment = TextAnchor.UpperCenter;
        fontStyle.normal.textColor = Color.white;
        fontStyle.hover.textColor = Color.white;
        EditorGUILayout.BeginVertical(titleStyle);
        {
            EditorGUILayout.BeginHorizontal();
            {
#if !UFE_LITE && !UFE_BASIC
                EditorGUILayout.LabelField("", attributeInfo.attributeName == "" ? "New Character" : attributeInfo.attributeName, fontStyle, GUILayout.Height(32));
#else
                EditorGUILayout.LabelField("", characterInfo.characterName == ""? "New Character" : characterInfo.characterName, fontStyle, GUILayout.Height(32));
#endif
                helpButton("character:start");
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        {
            EditorGUILayout.BeginVertical(rootGroupStyle);
            {
#if !UFE_LITE && !UFE_BASIC

#endif
                EditorGUILayout.BeginHorizontal();
                {
                    attributeInfo.attributePicture = (Sprite)EditorGUILayout.ObjectField(attributeInfo.attributePicture, typeof(Sprite), false, GUILayout.Width(100), GUILayout.Height(100));

                    EditorGUILayout.BeginVertical(rootGroupStyle);
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            basicInfoOption = EditorGUILayout.Foldout(basicInfoOption, "Basic Info", foldStyle);
                            helpButton("attribute:prefabs");
                        }
                        EditorGUILayout.EndHorizontal();
                        if (basicInfoOption)
                        {
                            EditorGUILayout.BeginVertical();
                            {
                                EditorGUIUtility.labelWidth = 90;
                                attributeInfo.attributeName = EditorGUILayout.TextField("Name:", attributeInfo.attributeName);
                                attributeInfo.stab = EditorGUILayout.FloatField("Stab:", attributeInfo.stab);
                            }
                            EditorGUILayout.EndVertical();
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();

                EditorGUIUtility.labelWidth = 180;

                EditorGUILayout.Space();
                EditorGUIUtility.labelWidth = 200;
                GUILayout.Label("Description:");
                Rect rect = GUILayoutUtility.GetRect(50, 90);
                EditorStyles.textField.wordWrap = true;
                attributeInfo.attributeDescription = EditorGUI.TextArea(rect, attributeInfo.attributeDescription);

                EditorGUILayout.Space();
                EditorGUIUtility.labelWidth = 150;

                EditorGUILayout.Space();
            }
            EditorGUILayout.EndVertical();

            // Begin Effectiness Options
            EditorGUILayout.BeginVertical(rootGroupStyle);
            {
                EditorGUILayout.BeginHorizontal();
                {
                    effectiveOptions = EditorGUILayout.Foldout(effectiveOptions, "Effectiveness", EditorStyles.foldout);
                    helpButton("move:chainmoves");
                }
                EditorGUILayout.EndHorizontal();

                if (effectiveOptions)
                {
                    EditorGUILayout.BeginVertical(subGroupStyle);
                    {
                        EditorGUI.indentLevel += 1;

                        // Move Links
                        EditorGUILayout.BeginVertical(subGroupStyle);
                        {
                            EditorGUI.indentLevel += 1;

                            for (int i = 0; i < attributeInfo.effectLinks.Length; i++)
                            {
                                EditorGUILayout.Space();
                                EditorGUILayout.BeginVertical(arrayElementStyle);
                                {
                                    EditorGUILayout.Space();
                                    EditorGUILayout.BeginHorizontal();
                                    {
                                        attributeInfo.effectLinks[i].targetAttribute = (CharacterAttributeInfo)EditorGUILayout.ObjectField("Target:", attributeInfo.effectLinks[i].targetAttribute, typeof(CharacterAttributeInfo), true); ;
                                        if (GUILayout.Button("", "PaneOptions"))
                                        {
                                            PaneOptions<Effectiveness>(attributeInfo.effectLinks, attributeInfo.effectLinks[i], delegate (Effectiveness[] newElement) { attributeInfo.effectLinks = newElement; });
                                        }
                                    }
                                    EditorGUILayout.EndHorizontal();
                                    attributeInfo.effectLinks[i].effectiveness = EditorGUILayout.FloatField("", attributeInfo.effectLinks[i].effectiveness);
                                }
                                EditorGUILayout.EndVertical();
                            }
                            if (StyledButton("New Effectiveness Link"))
                                attributeInfo.effectLinks = AddElement<Effectiveness>(attributeInfo.effectLinks, null);

                            EditorGUILayout.Space();
                            EditorGUI.indentLevel -= 1;

                            EditorGUILayout.EndVertical();
                        }
                        EditorGUI.indentLevel -= 1;

                    }
                    EditorGUILayout.EndVertical();
                }
                //GUILayout.Box("", "TimeScrubber", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});
                // End Effectiness Options
            }
            EditorGUILayout.EndVertical();
            // End Move Link Options

        }
        EditorGUILayout.EndScrollView();


        if (GUI.changed)
        {
            Undo.RecordObject(attributeInfo, "Character Editor Modify");
            EditorUtility.SetDirty(attributeInfo);
            if (UFE.autoSaveAssets) AssetDatabase.SaveAssets();
        }
    }

    private void SubGroupTitle(string _name)
    {
        Texture2D originalBackground = GUI.skin.box.normal.background;
        GUI.skin.box.normal.background = Texture2D.grayTexture;

        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(_name);
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        GUI.skin.box.normal.background = originalBackground;
    }

    public bool StyledButton(string label)
    {
        EditorGUILayout.Space();
        GUILayoutUtility.GetRect(1, 20);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        bool clickResult = GUILayout.Button(label, addButtonStyle);
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        return clickResult;
    }

    public float StyledSlider(string label, float targetVar, int indentLevel, float minValue, float maxValue)
    {
        int indentSpacing = 25 * indentLevel;
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        Rect tempRect = GUILayoutUtility.GetRect(1, 10);

        Rect rect = new Rect(indentSpacing, tempRect.y, position.width - indentSpacing - 100, 20);
        EditorGUI.ProgressBar(rect, Mathf.Abs((float)targetVar / maxValue), label);

        tempRect = GUILayoutUtility.GetRect(1, 20);
        rect.y += 10;
        rect.x = indentLevel * 10;
        rect.width = (position.width - (indentLevel * 10) - 100) + 55; // Changed for 4.3;

        return EditorGUI.Slider(rect, "", targetVar, minValue, maxValue);
    }

    public void ValidatePrefab(GameObject prefab, bool alt)
    {
        characterWarning = false;
        errorMsg = "";

        if (prefab != null)
        {
#if UNITY_2018_3_OR_NEWER
            if (PrefabUtility.GetPrefabAssetType(prefab) != PrefabAssetType.Regular)
            {
#else
            if (PrefabUtility.GetPrefabType(prefab) != PrefabType.Prefab) {
#endif
                characterWarning = true;
                errorMsg = "This character is not a prefab.";
            }
            else if (prefab.GetComponent<HitBoxesScript>() == null)
            {
                characterWarning = true;
                errorMsg = "This character doesn't have hitboxes!\n Please add the HitboxesScript and try again.";
            }
            /*} else if (alt && characterInfo.characterPrefab.GetComponent<HitBoxesScript>() != prefab.GetComponent<HitBoxesScript>()) {
                characterWarning = true;
                errorMsg = "This Hitbox setup is different from the main prefab.";
                ClosePreview();
            }*/
        }
    }

    public void StanceBlock(MoveSetData moveSet, bool resource = false)
    {
        EditorGUILayout.BeginVertical(resource ? subArrayElementStyle : arrayElementStyle);
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            {
                moveSet.combatStance = (CombatStances)EditorGUILayout.EnumPopup("Stance Number:", moveSet.combatStance, enumStyle);
                if (!resource && GUILayout.Button("", "PaneOptions"))
                {
                    //PaneOptions<MoveSetData>(attributeInfo.moves, moveSet, delegate (MoveSetData[] newElement) { attributeInfo.moves = newElement; });
                }
            }
            EditorGUILayout.EndHorizontal();


            moveSet.cinematicIntro = (MoveInfo)EditorGUILayout.ObjectField("Cinematic Intro:", moveSet.cinematicIntro, typeof(MoveInfo), false);
            moveSet.cinematicOutro = (MoveInfo)EditorGUILayout.ObjectField("Cinematic Outro:", moveSet.cinematicOutro, typeof(MoveInfo), false);

            EditorGUILayout.Space();

            moveSet.basicMovesToggle = EditorGUILayout.Foldout(moveSet.basicMovesToggle, "Basic Moves", foldStyle);
            if (moveSet.basicMovesToggle)
            {
                EditorGUILayout.BeginVertical(subGroupStyle);
                {
                    EditorGUILayout.Space();

                    moveSet.enabledBasicMovesToggle = EditorGUILayout.Foldout(moveSet.enabledBasicMovesToggle, "Enabled Moves", foldStyle);
                    if (moveSet.enabledBasicMovesToggle)
                    {
                        EditorGUILayout.BeginVertical(subArrayElementStyle);
                        {
                            EditorGUI.indentLevel += 1;
                            EditorGUILayout.Space();
                            moveSet.basicMoves.moveEnabled = EditorGUILayout.Toggle("Move", moveSet.basicMoves.moveEnabled);
                            moveSet.basicMoves.jumpEnabled = EditorGUILayout.Toggle("Jump", moveSet.basicMoves.jumpEnabled);
                            moveSet.basicMoves.crouchEnabled = EditorGUILayout.Toggle("Crouch", moveSet.basicMoves.crouchEnabled);
                            moveSet.basicMoves.blockEnabled = EditorGUILayout.Toggle("Block", moveSet.basicMoves.blockEnabled);
                            moveSet.basicMoves.parryEnabled = EditorGUILayout.Toggle("Parry", moveSet.basicMoves.parryEnabled);
                            EditorGUILayout.Space();
                            EditorGUI.indentLevel -= 1;
                        }
                        EditorGUILayout.EndVertical();
                    }
                    EditorGUILayout.Space();

                    SubGroupTitle("Standard Animations");
                    BasicMoveBlock("Idle (*)", moveSet.basicMoves.idle, WrapMode.Loop, false, true, false, false, false);
                    EditorGUI.BeginDisabledGroup(!moveSet.basicMoves.moveEnabled);
                    {
                        BasicMoveBlock("Move Forward (*)", moveSet.basicMoves.moveForward, WrapMode.Loop, false, true, false, false, false);
#if !UFE_LITE && !UFE_BASIC

#else
                        BasicMoveBlock("Move Back (*)", moveSet.basicMoves.moveBack, WrapMode.Loop, false, true, false, false, false);
#endif
                    }
                    EditorGUI.EndDisabledGroup();
                    EditorGUI.BeginDisabledGroup(!moveSet.basicMoves.crouchEnabled);
                    {
                        BasicMoveBlock("Crouching (*)", moveSet.basicMoves.crouching, WrapMode.Loop, false, true, false, false, false);
                    }
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.Space();

                    if (moveSet.basicMoves.jumpEnabled)
                    {
                        SubGroupTitle("Jump Animations");
                        BasicMoveBlock("Take Off", moveSet.basicMoves.takeOff, WrapMode.ClampForever, true, true, false, true, false);
                        BasicMoveBlock("Jump Straight (*)", moveSet.basicMoves.jumpStraight, WrapMode.ClampForever, true, true, false, false, false);
                        BasicMoveBlock("Jump Back", moveSet.basicMoves.jumpBack, WrapMode.ClampForever, true, true, false, false, false);
                        BasicMoveBlock("Jump Forward", moveSet.basicMoves.jumpForward, WrapMode.ClampForever, true, true, false, false, false);
                        EditorGUILayout.Space();

                        SubGroupTitle("Fall Animations");
                        BasicMoveBlock("Fall Straight (*)", moveSet.basicMoves.fallStraight, WrapMode.ClampForever, true, true, false, false, false);
                        BasicMoveBlock("Fall Back", moveSet.basicMoves.fallBack, WrapMode.ClampForever, true, true, false, false, false);
                        BasicMoveBlock("Fall Forward", moveSet.basicMoves.fallForward, WrapMode.ClampForever, true, true, false, false, false);
                        BasicMoveBlock("Landing", moveSet.basicMoves.landing, WrapMode.Once, true, true, false, false, false);
                        EditorGUILayout.Space();
                    };

                    if (moveSet.basicMoves.blockEnabled)
                    {
                        SubGroupTitle("Block Animations");
                        BasicMoveBlock("Standing Pose", moveSet.basicMoves.blockingHighPose, WrapMode.ClampForever, false, true, false, false, false);
                        BasicMoveBlock("Standing High Hit", moveSet.basicMoves.blockingHighHit, WrapMode.Once, true, true, true, false, false);
                        BasicMoveBlock("Standing Low Hit", moveSet.basicMoves.blockingLowHit, WrapMode.Once, true, true, true, false, false);
                        BasicMoveBlock("Crouching Pose", moveSet.basicMoves.blockingCrouchingPose, WrapMode.ClampForever, false, true, false, false, false);
                        BasicMoveBlock("Crouching Hit", moveSet.basicMoves.blockingCrouchingHit, WrapMode.Once, true, true, true, false, false);
                        BasicMoveBlock("Air Pose", moveSet.basicMoves.blockingAirPose, WrapMode.ClampForever, false, true, false, false, false);
                        BasicMoveBlock("Air Hit", moveSet.basicMoves.blockingAirHit, WrapMode.Once, true, true, true, false, false);
                        EditorGUILayout.Space();
                    };

                    if (moveSet.basicMoves.parryEnabled)
                    {
                        SubGroupTitle("Parry Animations");
                        BasicMoveBlock("Standing High Hit", moveSet.basicMoves.parryHigh, WrapMode.Once, true, true, false, false, false);
                        BasicMoveBlock("Standing Low Hit", moveSet.basicMoves.parryLow, WrapMode.Once, true, true, false, false, false);
                        BasicMoveBlock("Crouching Hit", moveSet.basicMoves.parryCrouching, WrapMode.Once, true, true, false, false, false);
                        BasicMoveBlock("Air Hit", moveSet.basicMoves.parryAir, WrapMode.Once, true, true, false, false, false);
                        EditorGUILayout.Space();
                    };

                    SubGroupTitle("Hit Reactions");
                    BasicMoveBlock("Standing High Hit (*)", moveSet.basicMoves.getHitHigh, WrapMode.Once, true, true, true, true, false);
                    BasicMoveBlock("Standing Low Hit", moveSet.basicMoves.getHitLow, WrapMode.Once, true, true, true, true, false);
                    EditorGUI.BeginDisabledGroup(!moveSet.basicMoves.crouchEnabled);
                    {
                        BasicMoveBlock("Crouching Hit (*)", moveSet.basicMoves.getHitCrouching, WrapMode.Once, true, true, true, true, false);
                    }
                    EditorGUI.EndDisabledGroup();
                    BasicMoveBlock("Air Juggle [Launcher]", moveSet.basicMoves.getHitAir, WrapMode.ClampForever, true, true, false, true, false);
                    BasicMoveBlock("Knock Back [Knockdown]", moveSet.basicMoves.getHitKnockBack, WrapMode.ClampForever, true, true, false, true, true);
                    BasicMoveBlock("Standing High Hit [Knockdown]", moveSet.basicMoves.getHitHighKnockdown, WrapMode.ClampForever, false, true, false, true, true);
                    BasicMoveBlock("Standing Mid Hit [Knockdown]", moveSet.basicMoves.getHitMidKnockdown, WrapMode.ClampForever, false, true, false, true, true);
                    BasicMoveBlock("Sweep [Knockdown]", moveSet.basicMoves.getHitSweep, WrapMode.ClampForever, false, true, false, true, true);
                    BasicMoveBlock("Crumple [Knockdown]", moveSet.basicMoves.getHitCrumple, WrapMode.ClampForever, true, true, false, true, true);
                    EditorGUILayout.Space();

                    SubGroupTitle("Stage Reactions");
                    BasicMoveBlock("Ground Bounce [Launcher]", moveSet.basicMoves.groundBounce, WrapMode.ClampForever, true, true, false, true, false);
                    BasicMoveBlock("Standing Wall Bounce", moveSet.basicMoves.standingWallBounce, WrapMode.ClampForever, false, true, false, true, false);
                    BasicMoveBlock("Standing Wall Bounce [Knockdown]", moveSet.basicMoves.standingWallBounceKnockdown, WrapMode.ClampForever, false, true, false, true, false);
                    BasicMoveBlock("Air Wall Bounce [Knockdown]", moveSet.basicMoves.airWallBounce, WrapMode.ClampForever, true, true, false, true, false);
                    EditorGUILayout.Space();

                    SubGroupTitle("Fall Down Reactions");
                    BasicMoveBlock("Default (*)", moveSet.basicMoves.fallDown, WrapMode.ClampForever, true, true, false, true, false);
                    BasicMoveBlock("From Air Juggle [Knockdown]", moveSet.basicMoves.fallingFromAirHit, WrapMode.ClampForever, true, true, false, true, false);
                    BasicMoveBlock("From Ground Bounce [Knockdown]", moveSet.basicMoves.fallingFromGroundBounce, WrapMode.ClampForever, true, true, false, true, false);
                    BasicMoveBlock("Air Recovery", moveSet.basicMoves.airRecovery, WrapMode.ClampForever, true, true, false, true, false);
                    EditorGUILayout.Space();

                    SubGroupTitle("Stand Up Animations");
                    BasicMoveBlock("Default (*)", moveSet.basicMoves.standUp, WrapMode.ClampForever, true, true, false, true, false);
                    BasicMoveBlock("From Air Juggle", moveSet.basicMoves.standUpFromAirHit, WrapMode.ClampForever, true, true, false, true, false);
                    BasicMoveBlock("From Knock Back", moveSet.basicMoves.standUpFromKnockBack, WrapMode.ClampForever, true, true, false, true, false);
                    BasicMoveBlock("From Standing High Hit", moveSet.basicMoves.standUpFromStandingHighHit, WrapMode.ClampForever, true, true, false, true, false);
                    BasicMoveBlock("From Standing Mid Hit", moveSet.basicMoves.standUpFromStandingMidHit, WrapMode.ClampForever, true, true, false, true, false);
                    BasicMoveBlock("From Sweep", moveSet.basicMoves.standUpFromSweep, WrapMode.ClampForever, true, true, false, true, false);
                    BasicMoveBlock("From Crumple", moveSet.basicMoves.standUpFromCrumple, WrapMode.ClampForever, true, true, false, true, false);
                    BasicMoveBlock("From Standing Wall Bounce", moveSet.basicMoves.standUpFromStandingWallBounce, WrapMode.ClampForever, true, true, false, true, false);
                    BasicMoveBlock("From Air Wall Bounce", moveSet.basicMoves.standUpFromAirWallBounce, WrapMode.ClampForever, true, true, false, true, false);
                    BasicMoveBlock("From Ground Bounce", moveSet.basicMoves.standUpFromGroundBounce, WrapMode.ClampForever, true, true, false, true, false);

                    EditorGUILayout.Space();

                    GUILayout.Label("* Required", "MiniBoldLabel");

                }
                EditorGUILayout.EndVertical();
            }

            moveSet.attackMovesToggle = EditorGUILayout.Foldout(moveSet.attackMovesToggle, "Attack & Special Moves (" + moveSet.attackMoves.Length + ")", foldStyle);
            if (moveSet.attackMovesToggle)
            {
                EditorGUILayout.BeginVertical(subGroupStyle);
                {
                    EditorGUILayout.Space();
                    EditorGUI.indentLevel += 1;

                    for (int y = 0; y < moveSet.attackMoves.Length; y++)
                    {
                        EditorGUILayout.Space();
                        EditorGUILayout.BeginVertical(subArrayElementStyle);
                        {
                            EditorGUILayout.Space();
                            EditorGUIUtility.labelWidth = 120;
                            EditorGUILayout.BeginHorizontal();
                            {
                                moveSet.attackMoves[y] = (MoveInfo)EditorGUILayout.ObjectField("Move File:", moveSet.attackMoves[y], typeof(MoveInfo), false);
                                if (GUILayout.Button("", "PaneOptions"))
                                {
                                    PaneOptions<MoveInfo>(moveSet.attackMoves, moveSet.attackMoves[y], delegate (MoveInfo[] newElement) { moveSet.attackMoves = newElement; });
                                }
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUIUtility.labelWidth = 150;

                            if (GUILayout.Button("Open in the Move Editor"))
                            {
                                MoveEditorWindow.sentMoveInfo = moveSet.attackMoves[y];
                                MoveEditorWindow.Init();
                            }
                        }
                        EditorGUILayout.EndVertical();
                    }
                    EditorGUILayout.Space();
                    if (StyledButton("New Move"))
                        moveSet.attackMoves = AddElement<MoveInfo>(moveSet.attackMoves, null);

                    EditorGUILayout.Space();
                    EditorGUI.indentLevel -= 1;
                }
                EditorGUILayout.EndVertical();
            }
            
            EditorGUILayout.Space();
        }

        if (!resource && StyledButton("Export Stance"))
            ScriptableObjectUtility.CreateAsset<StanceInfo>(moveSet.ConvertData());

        EditorGUILayout.EndVertical();
    }

    public void BasicMoveBlock(string label, BasicMoveInfo basicMove, WrapMode wrapMode, bool autoSpeed, bool hasSound, bool hasHitStrength, bool invincible, bool loops)
    {
        basicMove.editorToggle = EditorGUILayout.Foldout(basicMove.editorToggle, label, foldStyle);

        //GUIStyle foldoutStyle;
        //foldoutStyle = new GUIStyle(EditorStyles.foldout);
        //foldoutStyle.normal.textColor = Color.cyan;
        //basicMove.editorToggle = EditorGUI.Foldout(EditorGUILayout.GetControlRect(), basicMove.editorToggle, label, true, foldoutStyle);

        if (basicMove.editorToggle)
        {
            EditorGUILayout.BeginVertical(subArrayElementStyle);
            {
                EditorGUILayout.Space();
                EditorGUI.indentLevel += 1;
                EditorGUIUtility.labelWidth = 180;

                if (label != "Idle (*)")
                {
                    basicMove.useMoveFile = EditorGUILayout.Toggle("Use Move File", basicMove.useMoveFile, toggleStyle);
                }
                else
                {
                    basicMove.useMoveFile = false;
                }

                if (basicMove.useMoveFile)
                {
                    basicMove.moveInfo = (MoveInfo)EditorGUILayout.ObjectField("Move:", basicMove.moveInfo, typeof(MoveInfo), false);
                }
                else
                {
                    if (hasHitStrength)
                    {
                        // UFE 2.0.3 update
                        if (basicMove.animMap.Length <= 8)
                        {
                            Array.Resize(ref basicMove.animMap, 9);
                            basicMove.animMap[6] = new SerializedAnimationMap();
                            basicMove.animMap[7] = new SerializedAnimationMap();
                            basicMove.animMap[8] = new SerializedAnimationMap();
                        }

                        string required = label.IndexOf("*") != -1 ? " (*)" : "";
                        AnimationFieldBlock(basicMove.animMap[0], "Weak Hit", required);
                        AnimationFieldBlock(basicMove.animMap[1], "Medium Hit");
                        AnimationFieldBlock(basicMove.animMap[2], "Heavy Hit");
                        AnimationFieldBlock(basicMove.animMap[3], "Custom 1 Hit");
                        AnimationFieldBlock(basicMove.animMap[4], "Custom 2 Hit");
                        AnimationFieldBlock(basicMove.animMap[5], "Custom 3 Hit");
                        AnimationFieldBlock(basicMove.animMap[6], "Custom 4 Hit");
                        AnimationFieldBlock(basicMove.animMap[7], "Custom 5 Hit");
                        AnimationFieldBlock(basicMove.animMap[8], "Custom 6 Hit");

                    }
                    else if (label == "Idle (*)")
                    {
                        AnimationFieldBlock(basicMove.animMap[0], "Default", " (*)");
                        AnimationFieldBlock(basicMove.animMap[1], "AFK 1");
                        AnimationFieldBlock(basicMove.animMap[2], "AFK 2");
                        AnimationFieldBlock(basicMove.animMap[3], "AFK 3");
                        AnimationFieldBlock(basicMove.animMap[4], "AFK 4");
                        AnimationFieldBlock(basicMove.animMap[5], "AFK 5");
                        basicMove._restingClipInterval = EditorGUILayout.FloatField("Resting Interval:", (float)basicMove._restingClipInterval);

                    }
                    else if (label == "Stand Up (*)")
                    {
                        AnimationFieldBlock(basicMove.animMap[0], "Default", " (*)");
                        AnimationFieldBlock(basicMove.animMap[1], "High Knockdown");
                        AnimationFieldBlock(basicMove.animMap[2], "Low Knockdown");
                        AnimationFieldBlock(basicMove.animMap[3], "Sweep");
                        AnimationFieldBlock(basicMove.animMap[4], "Crumple");
                        AnimationFieldBlock(basicMove.animMap[5], "Wall Bounce");

                    }
                    else if (label == "Crouching (*)")
                    {
                        AnimationFieldBlock(basicMove.animMap[0], "Crouched");
                        AnimationFieldBlock(basicMove.animMap[1], "Crouching Down");
                        AnimationFieldBlock(basicMove.animMap[2], "Standing Up");
                    }
                    else if (label.IndexOf("[Knockdown]") != -1)
                    {
                        AnimationFieldBlock(basicMove.animMap[0], "Fall Clip", " (*)");
                        AnimationFieldBlock(basicMove.animMap[1], "Down Clip");
                        basicMove.loopDownClip = EditorGUILayout.Toggle("Loop Down Clip", basicMove.loopDownClip, toggleStyle);
                    }
                    else if (loops)
                    {
                        AnimationFieldBlock(basicMove.animMap[1], "Transition");
                        AnimationFieldBlock(basicMove.animMap[0], "Animation", " (*)");
                    }
                    else
                    {
                        AnimationFieldBlock(basicMove.animMap[0], "Animation");
                    }

                    if (autoSpeed)
                    {
                        basicMove.autoSpeed = EditorGUILayout.Toggle("Auto Speed", basicMove.autoSpeed, toggleStyle);
                    }
                    else
                    {
                        basicMove.autoSpeed = false;
                    }

                    if (basicMove.autoSpeed)
                    {
                        EditorGUI.BeginDisabledGroup(true);
                        EditorGUILayout.TextField("Animation Speed:", basicMove._animationSpeed.ToString());
                        EditorGUI.EndDisabledGroup();
                    }
                    else
                    {
                        basicMove._animationSpeed = EditorGUILayout.FloatField("Animation Speed:", (float)basicMove._animationSpeed);
                        //basicMove.scaleCharacterSpeed = EditorGUILayout.Toggle("Scale with Character Speed", basicMove.scaleCharacterSpeed, toggleStyle);
                    }

                    EditorGUILayout.BeginHorizontal();
                    {
                        basicMove.wrapMode = (WrapMode)EditorGUILayout.EnumPopup("Wrap Mode:", basicMove.wrapMode, enumStyle);
                        if (basicMove.wrapMode == WrapMode.Default) basicMove.wrapMode = wrapMode;
                        if (GUILayout.Button("Default", "minibutton", GUILayout.Width(60))) basicMove.wrapMode = wrapMode;

                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();
                    GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
                    EditorGUILayout.Space();

                    basicMove.overrideBlendingIn = EditorGUILayout.Toggle("Override Blending (In)", basicMove.overrideBlendingIn, toggleStyle);
                    if (basicMove.overrideBlendingIn)
                    {
                        basicMove._blendingIn = EditorGUILayout.FloatField("Blend In Duration:", (float)basicMove._blendingIn);
                    }

                    basicMove.overrideBlendingOut = EditorGUILayout.Toggle("Override Blending (Out)", basicMove.overrideBlendingOut, toggleStyle);
                    if (basicMove.overrideBlendingOut)
                    {
                        basicMove._blendingOut = EditorGUILayout.FloatField("Blend Out Duration:", (float)basicMove._blendingOut);
                    }

                    if (invincible) basicMove.invincible = EditorGUILayout.Toggle("Hide hitboxes", basicMove.invincible, toggleStyle);

                    basicMove.disableHeadLook = EditorGUILayout.Toggle("Disable Head Look", basicMove.disableHeadLook, toggleStyle);
                    basicMove.applyRootMotion = EditorGUILayout.Toggle("Apply Root Motion", basicMove.applyRootMotion, toggleStyle);
                    if (basicMove.applyRootMotion)
                    {
                        EditorGUI.indentLevel += 1;
                        basicMove.lockXMotion = EditorGUILayout.Toggle("Lock X Motion", basicMove.lockXMotion, toggleStyle);
                        basicMove.lockYMotion = EditorGUILayout.Toggle("Lock Y Motion", basicMove.lockYMotion, toggleStyle);
                        basicMove.lockZMotion = EditorGUILayout.Toggle("Lock Z Motion", basicMove.lockZMotion, toggleStyle);
                        EditorGUI.indentLevel -= 1;
                    }

                    EditorGUILayout.Space();
                    GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
                    EditorGUILayout.Space();

                    basicMove.particleEffect.editorToggle = EditorGUILayout.Foldout(basicMove.particleEffect.editorToggle, "Particle Effect", foldStyle);
                    if (basicMove.particleEffect.editorToggle)
                    {
                        EditorGUILayout.BeginVertical(subGroupStyle);
                        {
                            EditorGUILayout.Space();
                            basicMove.particleEffect.prefab = (GameObject)EditorGUILayout.ObjectField("Particle Prefab:", basicMove.particleEffect.prefab, typeof(UnityEngine.GameObject), true);
                            basicMove.particleEffect.identity = (IdentityType)EditorGUILayout.EnumPopup("Identity:", basicMove.particleEffect.identity, enumStyle);
                            basicMove.particleEffect.duration = EditorGUILayout.FloatField("Duration (seconds):", basicMove.particleEffect.duration);
                            basicMove.particleEffect.stick = EditorGUILayout.Toggle("Sticky", basicMove.particleEffect.stick, toggleStyle);
                            basicMove.particleEffect.bodyPart = (BodyPart)EditorGUILayout.EnumPopup("Body Part:", basicMove.particleEffect.bodyPart, enumStyle);
                            basicMove.particleEffect.positionOffSet = EditorGUILayout.Vector3Field("Off Set (relative):", basicMove.particleEffect.positionOffSet);
                            basicMove.particleEffect.mirrorOn2PSide = EditorGUILayout.Toggle("Mirror on Right Side", basicMove.particleEffect.mirrorOn2PSide);

                            EditorGUILayout.Space();
                        }
                        EditorGUILayout.EndVertical();
                    }
                    if (hasSound)
                    {
                        basicMove.soundEffectsToggle = EditorGUILayout.Foldout(basicMove.soundEffectsToggle, "Possible Sound Effects (" + basicMove.soundEffects.Length + ")", EditorStyles.foldout);
                        if (basicMove.soundEffectsToggle)
                        {
                            EditorGUILayout.BeginVertical(subGroupStyle);
                            {
                                basicMove.continuousSound = EditorGUILayout.Toggle("Continuous Sound", basicMove.continuousSound, toggleStyle);
                                EditorGUILayout.Space();

                                EditorGUIUtility.labelWidth = 150;
                                for (int i = 0; i < basicMove.soundEffects.Length; i++)
                                {
                                    EditorGUILayout.Space();
                                    EditorGUILayout.BeginVertical(subArrayElementStyle);
                                    {
                                        EditorGUILayout.Space();
                                        EditorGUILayout.BeginHorizontal();
                                        {
                                            basicMove.soundEffects[i] = (AudioClip)EditorGUILayout.ObjectField("Audio Clip:", basicMove.soundEffects[i], typeof(UnityEngine.AudioClip), true);
                                            if (GUILayout.Button("", "PaneOptions"))
                                            {
                                                PaneOptions<AudioClip>(basicMove.soundEffects, basicMove.soundEffects[i], delegate (AudioClip[] newElement) { basicMove.soundEffects = newElement; });
                                            }
                                        }
                                        EditorGUILayout.EndHorizontal();
                                        EditorGUILayout.Space();
                                    }
                                    EditorGUILayout.EndVertical();
                                }
                                if (StyledButton("New Sound Effect"))
                                    basicMove.soundEffects = AddElement<AudioClip>(basicMove.soundEffects, null);

                            }
                            EditorGUILayout.EndVertical();
                        }
                    }
                }

                EditorGUI.indentLevel -= 1;
                EditorGUILayout.Space();

            }
            EditorGUILayout.EndVertical();
        }
    }

    public Transform FindTransform(string searchString)
    {
        if (character == null) return null;
        Transform[] transformChildren = character.GetComponentsInChildren<Transform>();
        Transform found;
        foreach (Transform transformChild in transformChildren)
        {
            found = transformChild.Find("mixamorig:" + searchString);
            if (found == null) found = transformChild.Find(character.name + ":" + searchString);
            if (found == null) found = transformChild.Find(searchString);
            if (found != null) return found;
        }
        return null;
    }

    private void AnimationFieldBlock(SerializedAnimationMap animMap, string label, string required = "")
    {
        EditorGUILayout.BeginHorizontal();
        animMap.clip = (AnimationClip)EditorGUILayout.ObjectField(label + " Clip" + required + ":", animMap.clip, typeof(AnimationClip), false, GUILayout.ExpandWidth(true));
        EditorGUILayout.EndHorizontal();

        if (animMap.hitBoxDefinitionType == HitBoxDefinitionType.Custom)
        {
            animMap.customHitBoxDefinition = (CustomHitBoxesInfo)EditorGUILayout.ObjectField(label + " Map" + required + ":", animMap.customHitBoxDefinition, typeof(CustomHitBoxesInfo), false, GUILayout.ExpandWidth(true));
            if (animMap.customHitBoxDefinition != null && animMap.customHitBoxDefinition.clip != null && animMap.clip == null)
            {
                animMap.clip = animMap.customHitBoxDefinition.clip;
                animMap.length = animMap.clip.length;
            }
        }

    }

    public void PaneOptions<T>(T[] elements, T element, System.Action<T[]> callback)
    {
        if (elements == null || elements.Length == 0) return;
        GenericMenu toolsMenu = new GenericMenu();

        if ((elements[0] != null && elements[0].Equals(element)) || (elements[0] == null && element == null) || elements.Length == 1)
        {
            toolsMenu.AddDisabledItem(new GUIContent("Move Up"));
            toolsMenu.AddDisabledItem(new GUIContent("Move To Top"));
        }
        else
        {
            toolsMenu.AddItem(new GUIContent("Move Up"), false, delegate () { callback(MoveElement<T>(elements, element, -1)); });
            toolsMenu.AddItem(new GUIContent("Move To Top"), false, delegate () { callback(MoveElement<T>(elements, element, -elements.Length)); });
        }
        if ((elements[elements.Length - 1] != null && elements[elements.Length - 1].Equals(element)) || elements.Length == 1)
        {
            toolsMenu.AddDisabledItem(new GUIContent("Move Down"));
            toolsMenu.AddDisabledItem(new GUIContent("Move To Bottom"));
        }
        else
        {
            toolsMenu.AddItem(new GUIContent("Move Down"), false, delegate () { callback(MoveElement<T>(elements, element, 1)); });
            toolsMenu.AddItem(new GUIContent("Move To Bottom"), false, delegate () { callback(MoveElement<T>(elements, element, elements.Length)); });
        }

        toolsMenu.AddSeparator("");

        if (element != null && element is System.ICloneable)
        {
            toolsMenu.AddItem(new GUIContent("Copy"), false, delegate () { callback(CopyElement<T>(elements, element)); });
        }
        else
        {
            toolsMenu.AddDisabledItem(new GUIContent("Copy"));
        }

        if (element != null && CloneObject.objCopy != null && CloneObject.objCopy.GetType() == typeof(T))
        {
            toolsMenu.AddItem(new GUIContent("Paste"), false, delegate () { callback(PasteElement<T>(elements, element)); });
        }
        else
        {
            toolsMenu.AddDisabledItem(new GUIContent("Paste"));
        }

        toolsMenu.AddSeparator("");

        if (!(element is System.ICloneable))
        {
            toolsMenu.AddDisabledItem(new GUIContent("Duplicate"));
        }
        else
        {
            toolsMenu.AddItem(new GUIContent("Duplicate"), false, delegate () { callback(DuplicateElement<T>(elements, element)); });
        }
        toolsMenu.AddItem(new GUIContent("Remove"), false, delegate () { callback(RemoveElement<T>(elements, element)); });

        toolsMenu.ShowAsContext();
        EditorGUIUtility.ExitGUI();
    }

    public T[] RemoveElement<T>(T[] elements, T element)
    {
        List<T> elementsList = new List<T>(elements);
        elementsList.Remove(element);
        return elementsList.ToArray();
    }

    public T[] AddElement<T>(T[] elements, T element)
    {
        List<T> elementsList = new List<T>(elements);
        elementsList.Add(element);
        return elementsList.ToArray();
    }

    public T[] CopyElement<T>(T[] elements, T element)
    {
        CloneObject.objCopy = (object)(element as ICloneable).Clone();
        return elements;
    }

    public T[] PasteElement<T>(T[] elements, T element)
    {
        if (CloneObject.objCopy == null) return elements;
        List<T> elementsList = new List<T>(elements);
        elementsList.Insert(elementsList.IndexOf(element) + 1, (T)CloneObject.objCopy);
        //CloneObject.objCopy = null;
        return elementsList.ToArray();
    }

    public T[] DuplicateElement<T>(T[] elements, T element)
    {
        List<T> elementsList = new List<T>(elements);
        elementsList.Insert(elementsList.IndexOf(element) + 1, (T)(element as ICloneable).Clone());
        return elementsList.ToArray();
    }

    public T[] MoveElement<T>(T[] elements, T element, int steps)
    {
        List<T> elementsList = new List<T>(elements);
        int newIndex = Mathf.Clamp(elementsList.IndexOf(element) + steps, 0, elements.Length - 1);
        elementsList.Remove(element);
        elementsList.Insert(newIndex, element);
        return elementsList.ToArray();
    }
}