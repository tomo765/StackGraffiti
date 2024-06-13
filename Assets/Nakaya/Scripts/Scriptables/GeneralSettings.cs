using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMP = TMPro.TextMeshProUGUI;

[CreateAssetMenu(fileName = "GeneralSettings", menuName = "Scriptables/GeneralSettings")]
public class GeneralSettings : ScriptableObject
{
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


        public StageEvaluation Stage1 => m_Stage1;
        public StageEvaluation Stage2 => m_Stage2;
        public StageEvaluation Stage3 => m_Stage3;
        public StageEvaluation Stage4 => m_Stage4;
        public StageEvaluation Stage5 => m_Stage5;
        public StageEvaluation Stage6 => m_Stage6;
        public StageEvaluation Stage7 => m_Stage7;
        public StageEvaluation Stage8 => m_Stage8;
        public StageEvaluation Stage9 => m_Stage9;
        public StageEvaluation Stage10 => m_Stage10;

        [System.Serializable]
        public struct StageEvaluation
        {
            [SerializeField] private int m_ThreeStar;
            [SerializeField] private int m_TwoStar;
            [SerializeField] private int m_OneStar;

            public int ThreeStar => m_ThreeStar;
            public int TwoStar => m_TwoStar;
            public int OneStar => m_OneStar;
            
        }
    }

    [System.Serializable]
    public class Sprites
    {
        [SerializeField] private Sprite m_AliveEye;
        [SerializeField] private Sprite m_DeathEye;

        public Sprite AliveEye => m_AliveEye;
        public Sprite DeathEye => m_DeathEye;
    }
}
