using System;
using System.IO;
using System.Linq;
using UnityEngine;

/// <summary> ステージごとのクリア状況やスコアを保存する </summary>
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

    /// <summary> セーブデータを新規作成する </summary>
    public static void SetNewData()
    {
        m_StageDatas = CreateData();
        SaveData();
    }

    /// <summary> 新規データの </summary>
    private static StageDatas CreateData()
    {
        StageScore[] scores = new StageScore[Enum.GetValues(typeof(StageLevel)).Length - 1];
        for (int i = 1; i <= scores.Length; i++)
        {
            scores[i - 1] = new StageScore((StageLevel)i, 0);
        }
        return new StageDatas(scores, false);
    }

    /// <summary> 現在のクリア状況をセーブする </summary>
    public static void SaveData()
    {
        string json = JsonUtility.ToJson(m_StageDatas);
        File.WriteAllText(FilePath, json);
    }

    /// <summary> セーブデータをロード </summary>
    public static StageDatas LoadData()
    {
        ExistData();

        string jsonData = File.ReadAllText(FilePath);
        m_StageDatas = JsonUtility.FromJson<StageDatas>(jsonData);
        return m_StageDatas;
    }

    /// <summary> 指定したステージスコア情報を取得 </summary>
    private static StageScore GetScore(StageLevel state) => Array.Find(m_StageDatas.StageScores, score => score.StageType == state);

    public static void SetStageScore(StageLevel stage, int sleepCnt)
    {
        ExistData();

        int starLevel = GeneralSettings.Instance.StageInfos.GetCullentLevel(stage, sleepCnt);
        GetScore(stage).SetStarLevel(starLevel);
    }

    /// <summary> 全ステージの星の合計獲得数を取得 </summary>
    public static int GetTotalStarLevel()
    {
        int totalScore = 0;
        foreach(var score in m_StageDatas.StageScores)
        {
            totalScore += score.StarLevel;
        }

        return totalScore;
    }

    public static bool CanPlayStage(int stageLevel)
    {
        for (int i = 0; i < stageLevel; i++)
        {
            if (!m_StageDatas.StageScores[i].IsCleear())
            {
                return false;
            }
        }
        return true;
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