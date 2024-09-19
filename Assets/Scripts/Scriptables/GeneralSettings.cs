using System;
using UnityEngine;

/// <summary> ステージのレベル列挙 </summary>
public enum StageLevel  //これも自動生成でよかったかも
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
}

public enum IndicatesPriority  //表示優先度,カメラからの距離を設定 
{
    Layer1 = 1,  //z軸の-9
    Layer2 = 2,
    Layer3 = 3,
    Layer4 = 4,
    Layer5 = 5,
    Layer6 = 6,
    Layer7 = 7,
    Layer8 = 8,
    Layer9 = 9,
    Layer10 = 10,  //z軸の0
    Layer11 = 11,
    Layer12 = 12,
    Layer13 = 13,
    Layer14 = 14,
    Layer15 = 15,
    Layer16 = 16,
    Layer17 = 17,
    Layer18 = 18,
    Layer19 = 19,
    Layer20 = 20, //z軸の10
}

///<summary>使用するアセットを管理するクラス</summary>
///<remarks>
///     <para>GeneralSettings.Instance でアクセスし、必要なアセットを呼び出す。</para>
///     <para>値の書き換えができないように、呼び出しはゲッターのプロパティからのみできるようにする。(セッターは書かない)</para>
///     <para>get { } を [プロパティ名] => [変数] と書ける</para>
///</remarks>
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
                    Debug.LogError("No");
                }
            }
            return instance;
        }
    }

    [SerializeField] private PlayerSettings m_PlayerSettings;
    [SerializeField] private StageInfomations m_StageInfos;
    [SerializeField] private Prehabs m_Prehabs;
    [SerializeField] private Sprites m_Sprites;
    [SerializeField] private Sounds m_Sounds;
    [SerializeField] private IndicatesPrioritySettings m_Priorities;


    public Prehabs Prehab => m_Prehabs;
    public PlayerSettings PlayerSetting => m_PlayerSettings;
    public StageInfomations StageInfos => m_StageInfos;
    public Sprites Sprite => m_Sprites;
    public Sounds Sound => m_Sounds;
    public IndicatesPrioritySettings Priorities => m_Priorities;

    /// <summary>プレハブ管理クラス</summary>
    /// <remarks>シーン上に登場するアセットを呼び出す</remarks>
    [System.Serializable]
    public class Prehabs
    {
        [SerializeField] private CharacterManager m_Character;
        [SerializeField] private StageSelectButton m_StageSelectBtn;

        [SerializeField] private DontDestroyCanvas m_DontDestroyCanvas;
        [SerializeField] private OptionUI m_OptionUI;
        [SerializeField] private ResultUI m_ResultUI;
        [SerializeField] private FadeCanvasUI m_FadeCanvasUI;
        [SerializeField] private StageIntroUI m_StageIntroUI;
        [SerializeField] private ResetStageUI m_ResetStageUI;

        [Space(10), SerializeField] private BalloonEffect m_BalloonEffect;
        [SerializeField] private ClickEffect m_ClickEffect;
        [SerializeField] private JumpEffect m_JumpEffect;
        [SerializeField] private ConfettiEffect m_ConfettiEffect;
        [SerializeField] private SleepEffect m_SleepEffect;
        [SerializeField] private DeleteCharaEffect m_DeleteCharaEffect;

        [SerializeField] private SoundManager m_SoundManager;
        [SerializeField] private EffectContainer m_EffectContainer;

        public CharacterManager Character => m_Character;
        public StageSelectButton StageSelectBtn => m_StageSelectBtn;
        public DontDestroyCanvas DontDestroyCanvas => m_DontDestroyCanvas;
        public OptionUI OptionUI => m_OptionUI;
        public ResultUI ResultUI => m_ResultUI;
        public FadeCanvasUI FadeCanvasUI => m_FadeCanvasUI;
        public StageIntroUI StageIntroUI => m_StageIntroUI;
        public ResetStageUI ResetStageUI => m_ResetStageUI;
        public BalloonEffect BalloonEffect => m_BalloonEffect;
        public ClickEffect ClickEffect => m_ClickEffect;
        public JumpEffect JumpEffect => m_JumpEffect;
        public ConfettiEffect ConfettiEffect => m_ConfettiEffect;
        public SleepEffect SleepEffect => m_SleepEffect;
        public DeleteCharaEffect DeleteCharaEffect => m_DeleteCharaEffect;
        public SoundManager SoundManager => m_SoundManager;
        public EffectContainer EffectContainer => m_EffectContainer;
    }

    /// <summary>操作するキャラクターの管理クラス</summary>
    [System.Serializable]
    public class PlayerSettings
    {
        [SerializeField] private string[] m_RandomNames;
        [SerializeField] private PhysicsMaterial2D m_PhysicsOnDead;
        public string GetRandomName() => m_RandomNames[UnityEngine.Random.Range(0, m_RandomNames.Length)];
        public PhysicsMaterial2D PhysicsOnDead => m_PhysicsOnDead;

    }

    /// <summary>ステージの情報を管理するクラス</summary>
    [System.Serializable]
    public class StageInfomations
    {
        [SerializeField] private StageInfomation[] m_Stages = null;
        [SerializeField] private int m_LastTutorialStage;

        public StageInfomation[] Stages => m_Stages;
        public int LastTutorialStage
        {
            get
            {
                if(m_LastTutorialStage == 0) { return 0; }
                return m_LastTutorialStage;
            }
        }

        public int GetCullentLevel(StageLevel type, int sleepCnt) => GetStageInfo(type).GetStarLevel(sleepCnt);
        private StageInfomation GetStageInfo(StageLevel stg) => Array.Find(Stages, stage => stage.StageLevel == stg);
        public string GetStageTextEN(int stg) => Array.Find(Stages, stage => stage.StageLevel == (StageLevel)stg).StageTitleEN;
        public string GetStageTextJP(int stg) => Array.Find(Stages, stage => stage.StageLevel == (StageLevel)stg).StageTitleJP;

        [System.Serializable]
        public class StageInfomation
        {
            [SerializeField] private StageLevel m_StageLevel;
            [SerializeField] private string m_StageTitleJP;
            [SerializeField] private string m_StageTitleEN;
            [SerializeField] private int m_ThreeStar;
            [SerializeField] private int m_TwoStar;

            public StageLevel StageLevel => m_StageLevel;
            public string StageTitleJP => m_StageTitleJP;
            public string StageTitleEN => m_StageTitleEN;

            public int GetStarLevel(int sleepCnt)
            {
                if (sleepCnt <= m_ThreeStar) { return 3; }
                else if (sleepCnt <= m_TwoStar) { return 2; }
                else { return 1; }
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
        [SerializeField] private FadesSound m_Fade1;

        public AudioClip HoverSE => m_HoverSE;
        public AudioClip SelectSE => m_SelectSE;
        public AudioClip JumpSE => m_JumpSE;
        public AudioClip TouchNeedleSE => m_TouchNeedleSE;
        public AudioClip ClearSE => m_ClearSE;
        public FadesSound Fade1 => m_Fade1;

        [System.Serializable]
        public class FadesSound
        {
            [SerializeField] private AudioClip m_FadeIn;
            [SerializeField] private AudioClip m_FadeOut;
            public AudioClip FadeIn => m_FadeIn;
            public AudioClip FadeOut => m_FadeOut;

        }
    }

    [System.Serializable]
    public class IndicatesPrioritySettings
    {
        [SerializeField] private IndicatesPriority m_CreateChara = IndicatesPriority.Layer2;
        [SerializeField] private IndicatesPriority m_StageCanvas = IndicatesPriority.Layer5;
        [SerializeField] private IndicatesPriority m_Effect = IndicatesPriority.Layer10;
        [SerializeField] private IndicatesPriority m_CharaOnStage = IndicatesPriority.Layer10;
        [SerializeField] private IndicatesPriority m_DontDestroyCanvas = IndicatesPriority.Layer11;


        public Vector3 CreateCharaLayer => Camera.main.transform.position + new Vector3(0, 0, (int)m_CreateChara);
        public float StageCanvas => (float)m_StageCanvas;
        public float DontDestroyCanvas => (float)m_DontDestroyCanvas;
        public Vector3 StageCanvasLayer => Camera.main.transform.position + new Vector3(0, 0, (int)m_StageCanvas);
        public Vector3 EffectLayer => Camera.main.transform.position + new Vector3(0, 0, (int)m_Effect);
        public Vector3 CharaOnStageLayer => Camera.main.transform.position + new Vector3(0, 0, (int)m_CharaOnStage);
    }
}
