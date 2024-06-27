using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMP = TMPro.TextMeshProUGUI;

public enum StageType
{
    None,
    Stage1,
    Stage2,
    Stage3,
    Stage4,
    Stage5,
    Stage6,
    Stage7,
    Stage8,
    Stage9,
    Stage10,
    Stage11,
    Stage12,
    Stage13,
}


[CreateAssetMenu(fileName = "GeneralSettings", menuName = "Scriptables/GeneralSettings")]
public class GeneralSettings : ScriptableObject
{
    private const string path = "GeneralSettings";

    private static GeneralSettings instance;
    public static GeneralSettings Instance
    {
        get
        {
            if(instance == null)
            {
                instance = Resources.Load<GeneralSettings>(path);
                if(instance == null )
                {
                    Debug.LogError("no");
                }
            }
            return instance;
        }
    }

    [SerializeField] private PlayerSettings m_PlayerSettings;
    [SerializeField] private StageEvaluations m_StageEvaluation;
    [SerializeField] private Prehabs m_Prehabs;
    [SerializeField] private Sprites m_Sprites;
    [SerializeField] private Sounds m_Sounds;


    public Prehabs Prehab => m_Prehabs;
    public PlayerSettings PlayerSetting => m_PlayerSettings;
    public StageEvaluations StageEvals => m_StageEvaluation;
    public Sprites Sprite => m_Sprites;
    public Sounds Sound => m_Sounds;


    [System.Serializable]
    public class Prehabs
    {
        [SerializeField] private CharacterManager m_Character;
        [SerializeField] private StageSelectButton m_StageSelectBtn;
        [SerializeField] private FadeCanvasUI m_FadeCanvasUI;

        [Space(10), SerializeField] private BalloonEffect m_BalloonEffect;
        [SerializeField] private ClickEffect m_ClickEffect;
        [SerializeField] private JumpEffect m_JumpEffect;

        [SerializeField] private SoundManager m_SoundManager;
        [SerializeField] private EffectContainer m_EffectContainer;

        public CharacterManager Character => m_Character;
        public StageSelectButton StageSelectBtn => m_StageSelectBtn;
        public FadeCanvasUI FadeCanvasUI => m_FadeCanvasUI;
        public BalloonEffect BalloonEffect => m_BalloonEffect;
        public ClickEffect ClickEffect => m_ClickEffect;
        public JumpEffect JumpEffect => m_JumpEffect;
        public SoundManager SoundManager => m_SoundManager;
        public EffectContainer EffectContainer => m_EffectContainer;
    }

    [System.Serializable]
    public class PlayerSettings
    {
        [SerializeField] private string[] m_RandomNames;
        [SerializeField] private PhysicsMaterial2D m_PhysicsOnDead;
        public string GetRandomName() => m_RandomNames[UnityEngine.Random.Range(0, m_RandomNames.Length)];
        public PhysicsMaterial2D PhysicsOnDead => m_PhysicsOnDead;

    }

    [System.Serializable]
    public class StageEvaluations
    {
        [SerializeField] private StageEvaluation[] m_Stages = null;

        public StageEvaluation[] Stages => m_Stages;

        public int GetCullentLevel(StageType type, int sleepCnt) => GetStageEval(type).GetStarLevel(sleepCnt);
        private StageEvaluation GetStageEval(StageType stg) => Array.Find(Stages, stage => stage.StageType == stg);

        [System.Serializable]
        public class StageEvaluation
        {
            [SerializeField] private StageType m_StageType;
            [SerializeField] private int m_ThreeStar;
            [SerializeField] private int m_TwoStar;
            [SerializeField] private int m_OneStar;

            public StageType StageType => m_StageType;

            public int GetStarLevel(int sleepCnt)
            {
                if (sleepCnt <= m_ThreeStar)
                {
                    return 3;
                }
                else if (sleepCnt <= m_TwoStar)
                {
                    return 2;
                }

                return 1;
            }
        }
    }

    [System.Serializable]
    public class Sprites
    {
        [SerializeField] private Sprite m_AliveEye;
        [SerializeField] private Sprite m_DeathEye;
        [SerializeField] private Sprite m_ClearStar;
        [SerializeField] private Sprite m_UnclearStar;
        [SerializeField] private Sprite m_SwitchIdle;
        [SerializeField] private Sprite m_SwitchPush;

        public Sprite AliveEye => m_AliveEye;
        public Sprite DeathEye => m_DeathEye;
        public Sprite ClearStar => m_ClearStar;
        public Sprite UnclearStar => m_UnclearStar;
        public Sprite SwitchIdle => m_SwitchIdle;
        public Sprite SwitchPush => m_SwitchPush;
    }

    [System.Serializable]
    public class Sounds
    {
        [SerializeField] private AudioClip m_HoverSE;
        [SerializeField] private AudioClip m_SelectSE;
        [SerializeField] private AudioClip m_JumpSE;
        [SerializeField] private AudioClip m_TouchNeedleSE;
        [SerializeField] private AudioClip m_ClearSE;

        public AudioClip HoverSE => m_HoverSE;
        public AudioClip SelectSE => m_SelectSE;
        public AudioClip JumpSE => m_JumpSE;
        public AudioClip TouchNeedleSE => m_TouchNeedleSE;
        public AudioClip ClearSE => m_ClearSE;
    }
}
