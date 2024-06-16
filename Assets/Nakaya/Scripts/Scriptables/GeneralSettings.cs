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
    public StageEvaluations StageEvals => m_StageEvaluation;
    public Sprites Sprite => m_Sprites;
    public Sounds Sound => m_Sounds;


    [System.Serializable]
    public class Prehabs
    {
        [SerializeField] private GameObject m_Eye;
        [SerializeField] private CharacterManager m_Character;
        [SerializeField] private TMP m_NameText;
        [SerializeField] private StageSelectButton m_StageSelectBtn;

        public GameObject Eye => m_Eye;
        public CharacterManager Character => m_Character;
        public TMP NameText => m_NameText;
        public StageSelectButton StageSelectBtn => m_StageSelectBtn;
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

        public Sprite AliveEye => m_AliveEye;
        public Sprite DeathEye => m_DeathEye;
        public Sprite ClearStar => m_ClearStar;
        public Sprite UnclearStar => m_UnclearStar;
    }

    [System.Serializable]
    public class Sounds
    {
        [SerializeField] private AudioClip m_HoverSE;
        [SerializeField] private AudioClip m_SelectSE;
        [SerializeField] private AudioClip m_JumpSE;

        public AudioClip HoverSE => m_HoverSE;
        public AudioClip SelectSE => m_SelectSE;
        public AudioClip JumpSE => m_JumpSE;
    }
}
