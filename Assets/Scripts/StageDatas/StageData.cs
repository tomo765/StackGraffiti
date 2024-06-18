using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public static class StageDataUtility
{
    public static readonly string FolderPath = Path.Combine(Application.dataPath, "SaveDatas");
    public static readonly string FilePath = Path.Combine(FolderPath, "StageData.json");


    private static StageDatas m_StageDatas;

    public static StageDatas StageDatas => m_StageDatas;

    public static void FindData()
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
        else
        {
            LoadData();
        }
    }

    public static void SetNewData()
    {
        CreateData();
        SaveData();
    }

    private static void CreateData()
    {
        StageScore[] scores = new StageScore[Enum.GetValues(typeof(StageType)).Length - 1];
        for (int i = 1; i <= scores.Length; i++)
        {
            scores[i - 1] = new StageScore((StageType)i, 0);
        }
        m_StageDatas = new StageDatas(scores);
    }

    public static void SaveData()
    {
        string json = JsonUtility.ToJson(m_StageDatas);
        File.WriteAllText(FilePath, json);
    }

    public static StageDatas LoadData()
    {
        string jsonData = File.ReadAllText(FilePath);
        m_StageDatas = JsonUtility.FromJson<StageDatas>(jsonData);

        return m_StageDatas;
    }

    private static StageScore GetScore(StageType state) => Array.Find(m_StageDatas.StageScores, score => score.StageType == state);

    public static void SetStageScore(StageType stage, int sleepCnt)
    {
        int starLevel = GeneralSettings.Instance.StageEvals.GetCullentLevel(stage, sleepCnt);
        GetScore(stage).SetStarLevel(starLevel);
    }

    public static int GetCullentStarLevel(StageType stage)
    {
        var score = Array.Find(m_StageDatas.StageScores, score => score.StageType == StageType.Stage1);
        return score.StarLevel;
    }
}