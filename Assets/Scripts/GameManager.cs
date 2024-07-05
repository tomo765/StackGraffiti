using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public enum GameState
{
    Drawing,
    Playing,
    Goal,
    HowToPlay
}

public static class GameManager
{
    private static GameState m_GameState = GameState.Drawing;
    private static StageType m_CullentStage = StageType.Stage1;
    private static int m_SleepCount = 0;
    private static GameObject m_SpawnArea;

    private static Action m_UpdSleepText;
    private static Action m_RestartDraw;

    public static GameState GameState => m_GameState;
    public static bool IsDrawing => m_GameState == GameState.Drawing;
    public static bool IsPlaying => m_GameState == GameState.Playing;
    public static bool IsClear => m_GameState == GameState.Goal;
    public static bool IsHowToPlay => m_GameState == GameState.HowToPlay;

    public static StageType CullentStage => m_CullentStage;

    public static int SleepCount => m_SleepCount;
    public static GameObject SpawnArea => m_SpawnArea;


    public static void SetGameState(GameState state) => m_GameState = state;
    public static void SetSpawnPos(GameObject pos) => m_SpawnArea = pos;
    public static void SetUpdateSleepText(Action onSleep) => m_UpdSleepText = onSleep;
    public static void SetRestartDrawing(Action reDraw) => m_RestartDraw = reDraw;

    public static void StartStage(StageType stg)
    {
        m_CullentStage = stg;
        m_UpdSleepText = null;
        SetGameState(GameState.Drawing);
        m_SleepCount = 0;
        Debug.Log(stg);
    }

    public static void SleepCharacter()
    {
        AddSleepCount();
        SetGameState(GameState.Drawing);
        m_UpdSleepText();
        m_RestartDraw();
    }

    public static void AddSleepCount() => m_SleepCount++;

    public static void StageClear()
    {
        SetGameState(GameState.Goal);
        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.ClearSE);

        StageDataUtility.SetStageScore(m_CullentStage, m_SleepCount);
        DontDestroyCanvas.Instance.ChangeResultUIVisible();
    }

    public static void CheckStarLevel()
    {
        var datas = StageDataUtility.LoadData();
        var allStarLevel = StageDataUtility.GetAllStarLevel();

        if(datas.StageScores.All(data => data.StarLevel != 0) && !datas.Credited)  //初めてすべてのステージをクリアしていたら
        {
            //クレジット
            //StageDataUtility.
        }

        SoundManager.Instance.SetSubBGMVolume(allStarLevel);
        SoundManager.Instance.PlayMainBGM();
        SoundManager.Instance.PlayCode(allStarLevel >= SoundManager.PlayCodeScore);
        SoundManager.Instance.PlayBass(allStarLevel >= SoundManager.PlayBassScore);
        SoundManager.Instance.PlayMarimba(0);
    }
}
