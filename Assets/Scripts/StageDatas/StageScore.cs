using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �S�X�e�[�W�̃N���A�󋵁A�N���W�b�g�Đ��ς݂���ۑ�����N���X </summary>
[System.Serializable]
public class StageDatas
{
    [SerializeField] private StageScore[] m_StageScores;
    [SerializeField] private bool m_SawCredit;

    public StageScore[] StageScores => m_StageScores;
    public bool SawCredit => m_SawCredit;

    public StageDatas(StageScore[] scores, bool isCredited)
    {
        m_StageScores = scores;
        m_SawCredit = isCredited;
    }

    public void BrowseCredit()
    {
        m_SawCredit = true;
        StageDataUtility.SaveData();
    }
}

/// <summary> StageType �� </summary>
[System.Serializable]
public class StageScore
{
    [SerializeField] private StageLevel m_StageType;
    [SerializeField] private int m_StarLevel;

    public StageLevel StageType => m_StageType;
    public int StarLevel => m_StarLevel;

    public StageScore(StageLevel stageName, int starLvl)
    {
        m_StageType = stageName;
        m_StarLevel = starLvl;
    }


    public void SetStarLevel(int level)
    {
        if (m_StarLevel >= level) { return; }
        m_StarLevel = level;

        StageDataUtility.SaveData();
    }

    public bool IsCleear() => m_StarLevel != 0;
}
