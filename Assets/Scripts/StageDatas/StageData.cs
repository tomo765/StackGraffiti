using System;
using System.IO;
using System.Linq;
using UnityEngine;

public static class StageDataUtility
{
    public static readonly string FolderPath = Path.Combine(Application.dataPath, "SaveDatas");
    public static readonly string FilePath = Path.Combine(FolderPath, "StageData.json");


    private static StageDatas m_StageDatas;

    public static StageDatas StageDatas => m_StageDatas;

    public static void ExistData()
    {
        // ディレクトリが存在しない場合は作成
        if (!Directory.Exists(FolderPath))
        {
            Directory.CreateDirectory(FolderPath);
        }

        if(!File.Exists(FilePath))
        {
            SetNewData();
        }
    }

    public static void SetNewData()
    {
        m_StageDatas = CreateData();
        SaveData();
    }

    private static StageDatas CreateData()
    {
        StageScore[] scores = new StageScore[Enum.GetValues(typeof(StageType)).Length - 1];
        for (int i = 1; i <= scores.Length; i++)
        {
            scores[i - 1] = new StageScore((StageType)i, 0);
        }
        return new StageDatas(scores, false);
    }

    public static void SaveData()
    {
        string json = JsonUtility.ToJson(m_StageDatas);
        File.WriteAllText(FilePath, json);
    }

    public static StageDatas LoadData()
    {
        ExistData();

        string jsonData = File.ReadAllText(FilePath);
        m_StageDatas = JsonUtility.FromJson<StageDatas>(jsonData);
        return m_StageDatas;
    }

    private static StageScore GetScore(StageType state) => Array.Find(m_StageDatas.StageScores, score => score.StageType == state);

    public static void SetStageScore(StageType stage, int sleepCnt)
    {
        ExistData();

        int starLevel = GeneralSettings.Instance.StageInfos.GetCullentLevel(stage, sleepCnt);
        GetScore(stage).SetStarLevel(starLevel);
    }

    public static int GetAllStarLevel()
    {
        int allScore = 0;
        m_StageDatas.StageScores.Select(score => allScore += score.StarLevel).ToArray();

        return allScore;
    }

    public static bool IsSelectable(int stageLevel)
    {
        if(stageLevel == 0) {  return true; }
        if (StageDatas.StageScores[GeneralSettings.Instance.StageInfos.LastTutorialStage - 1].StarLevel != 0) { return true; }

        for(int i = 0; i < stageLevel; i++)
        {
            if(StageDatas.StageScores[i].StarLevel == 0) { return false; }
        }
        return true;
    }
}