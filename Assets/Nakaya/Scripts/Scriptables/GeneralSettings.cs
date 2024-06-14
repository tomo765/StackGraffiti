using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMP = TMPro.TextMeshProUGUI;

public enum StageState
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
    Stage10
}


[CreateAssetMenu(fileName = "GeneralSettings", menuName = "Scriptables/GeneralSettings")]
public class GeneralSettings : ScriptableObject  //ToDo : ステージセレクトのプレハブを作る
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



    [SerializeField] private Prehabs m_Prehabs;
    [SerializeField] private StageEvaluations m_StageEvaluation;
    [SerializeField] private Sprites m_Sprites;
    [SerializeField] private Sounds m_Sounds;


    public Prehabs Prehab => m_Prehabs;
    public StageEvaluations StageEval => m_StageEvaluation;
    public Sprites Sprite => m_Sprites;
    public Sounds Sound => m_Sounds;


    [System.Serializable]
    public class Prehabs
    {
        [SerializeField] private GameObject m_Eye;
        //[SerializeField] private GameObject m_Head;
        [SerializeField] private TMP m_NameText;
        [SerializeField] private StageSelectButton m_StageSelectBtn;

        public GameObject Eye => m_Eye;
        public TMP NameText => m_NameText;
        public StageSelectButton StageSelectBtn => m_StageSelectBtn;
    }

    [System.Serializable]
    public class StageEvaluations
    {
        [SerializeField] private StageEvaluation m_Stage1;
        [SerializeField] private StageEvaluation m_Stage2;
        [SerializeField] private StageEvaluation m_Stage3;
        [SerializeField] private StageEvaluation m_Stage4;
        [SerializeField] private StageEvaluation m_Stage5;
        [SerializeField] private StageEvaluation m_Stage6;
        [SerializeField] private StageEvaluation m_Stage7;
        [SerializeField] private StageEvaluation m_Stage8;
        [SerializeField] private StageEvaluation m_Stage9;
        [SerializeField] private StageEvaluation m_Stage10;

        Dictionary<StageState, StageEvaluation> m_StageScores;
        public Dictionary<StageState, StageEvaluation> StageScores
        {
            get
            {
                if(m_StageScores == null)
                {
                    m_StageScores = new Dictionary<StageState, StageEvaluation>()
                    {
                        {StageState.Stage1, m_Stage1 },
                        {StageState.Stage2, m_Stage2 },
                        {StageState.Stage3, m_Stage3 },
                        {StageState.Stage4, m_Stage4 },
                        {StageState.Stage5, m_Stage5 },
                        {StageState.Stage6, m_Stage6 },
                        {StageState.Stage7, m_Stage7 },
                        {StageState.Stage8, m_Stage8 },
                        {StageState.Stage9, m_Stage9 },
                        {StageState.Stage10, m_Stage10 }
                    };
                }
                return m_StageScores;
            }
        }

        [System.Serializable]
        public class StageEvaluation
        {
            [SerializeField] private int m_ThreeStar;
            [SerializeField] private int m_TwoStar;
            [SerializeField] private int m_OneStar;

            private int m_CullentEval = 0;
            public int CullentEval => m_CullentEval;

            private Dictionary<int, int> m_ClearLevel;
            public Dictionary<int, int> ClearLevel
            {
                get
                {
                    if(m_ClearLevel == null)
                    {
                        m_ClearLevel = new Dictionary<int, int>()
                        {
                            {m_ThreeStar, 3 },
                            {m_TwoStar, 2},
                            {m_OneStar, 1}
                        };
                    }
                    return m_ClearLevel;
                }
            }

            public void SetClearEval(int i)
            {
                if(m_CullentEval >= i) { return; }
                m_CullentEval = i;
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

        public Sprite AliveEye => m_AliveEye;
        public Sprite DeathEye => m_DeathEye;
        public Sprite ClearStar => m_ClearStar;
        public Sprite UnclearStar => m_UnclearStar;


        [SerializeField] private Sprite m_Stage1;
        [SerializeField] private Sprite m_Stage2;
        [SerializeField] private Sprite m_Stage3;
        [SerializeField] private Sprite m_Stage4;
        [SerializeField] private Sprite m_Stage5;
        [SerializeField] private Sprite m_Stage6;
        [SerializeField] private Sprite m_Stage7;
        [SerializeField] private Sprite m_Stage8;
        [SerializeField] private Sprite m_Stage9;
        [SerializeField] private Sprite m_Stage10;
        [SerializeField] private Sprite m_Stage11;
        [SerializeField] private Sprite m_Stage12;
        [SerializeField] private Sprite m_Stage13;

        private Dictionary<int, Sprite> m_StageCuts;
        public Dictionary<int, Sprite> StageCuts
        {
            get
            {
                m_StageCuts ??= new Dictionary<int, Sprite>()  //nullチェックをして、nullなら代入
                {
                    {1, m_Stage1 },
                    {2, m_Stage2 },
                    {3, m_Stage3 },
                    {4, m_Stage4 },
                    {5, m_Stage5 },
                    {6, m_Stage6 },
                    {7, m_Stage7 },
                    {8, m_Stage8 },
                    {9, m_Stage9 },
                    {10, m_Stage10 },
                    {11, m_Stage11 },
                    {12, m_Stage12 },
                    {13, m_Stage13 },
                };
                return m_StageCuts;
            }
        }
    }

    [System.Serializable]
    public class Sounds
    {
        [SerializeField] private AudioClip m_SelectSE;

        public AudioClip SelectSE => m_SelectSE;
    }
}
