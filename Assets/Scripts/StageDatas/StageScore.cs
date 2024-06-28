using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StageDatas
{
    [SerializeField] private StageScore[] m_StageScores;
    [SerializeField] private bool m_Credited;

    public StageScore[] StageScores => m_StageScores;
    public bool Credited => m_Credited;

    public StageDatas(StageScore[] scores, bool isCredited)
    {
        m_StageScores = scores;
        m_Credited = isCredited;
    }
}


[System.Serializable]
public class StageScore
{
    [SerializeField] private StageType m_StageType;
    [SerializeField] private int m_StarLevel;

    public StageType StageType => m_StageType;
    public int StarLevel => m_StarLevel;

    public StageScore(StageType stageName, int starLvl)
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
}
