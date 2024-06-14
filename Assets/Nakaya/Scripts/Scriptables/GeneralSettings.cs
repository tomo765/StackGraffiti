using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMP = TMPro.TextMeshProUGUI;

public enum Stages
{
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



    [SerializeField] private Prehabs m_Prehabs;
    [SerializeField] private StageEvaluations m_StageEvaluation;
    [SerializeField] private Sprites m_Sprites;
    public Prehabs Prehab => m_Prehabs;
    public StageEvaluations StageEval => m_StageEvaluation;
    public Sprites Sprite => m_Sprites;



    [System.Serializable]
    public class Prehabs
    {
        [SerializeField] private GameObject m_Eye;
        //[SerializeField] private GameObject m_Head;
        [SerializeField] private TMP m_NameText;

        public GameObject Eye => m_Eye;
        public TMP NameText => m_NameText;
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

        Dictionary<Stages, StageEvaluation> m_StageScores;
        public Dictionary<Stages, StageEvaluation> StageScores
        {
            get
            {
                if(m_StageScores == null)
                {
                    m_StageScores = new Dictionary<Stages, StageEvaluation>()
                    {
                        {Stages.Stage1, m_Stage1 },
                        {Stages.Stage2, m_Stage2 },
                        {Stages.Stage3, m_Stage3 },
                        {Stages.Stage4, m_Stage4 },
                        {Stages.Stage5, m_Stage5 },
                        {Stages.Stage6, m_Stage6 },
                        {Stages.Stage7, m_Stage7 },
                        {Stages.Stage8, m_Stage8 },
                        {Stages.Stage9, m_Stage9 },
                        {Stages.Stage10, m_Stage10 }
                    };
                }
                return m_StageScores;
            }
        }

        [System.Serializable]
        public struct StageEvaluation
        {
            [SerializeField] private int m_ThreeStar;
            [SerializeField] private int m_TwoStar;
            [SerializeField] private int m_OneStar;

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
    }
}
