using System;
using UnityEngine;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public enum GameState
{
    Drawing,
    Playing,
    Goal,
    HowToPlay
}

/// <summary> ステージ上でのステートなどの管理をする </summary>
public static class GameManager
{
    private static GameState m_GameState = GameState.Drawing;
    private static StageLevel m_CullentStage = StageLevel.Stage1;
    private static int m_SleepCount = 0;
    private static GameObject m_SpawnArea;
    private static CancellationTokenSource m_Source;

    private static Action m_UpdSleepText;
    private static Action m_RestartDraw;

    public static GameState GameState => m_GameState;
    public static bool IsDrawing => m_GameState == GameState.Drawing;
    public static bool IsPlaying => m_GameState == GameState.Playing;
    public static bool IsClear => m_GameState == GameState.Goal;
    public static bool IsHowToPlay => m_GameState == GameState.HowToPlay;

    public static StageLevel CullentStage => m_CullentStage;
    public static bool IsLastStage => m_CullentStage == StageLevel.Stage10;

    public static int SleepCount => m_SleepCount;
    public static GameObject SpawnArea => m_SpawnArea;
    public static CancellationTokenSource Source => m_Source;

    public static void SetGameState(GameState state) => m_GameState = state;
    public static void SetSpawnPos(GameObject pos) => m_SpawnArea = pos;
    public static void SetUpdateSleepText(Action onSleep) => m_UpdSleepText = onSleep;
    public static void SetRestartDrawing(Action reDraw) => m_RestartDraw = reDraw;

    /// <summary> プレイ状況の初期化 </summary>
    public static void InitPlayState(StageLevel stg)
    {
        m_Source = new CancellationTokenSource();
        m_CullentStage = stg;
        m_UpdSleepText = null;
        SetGameState(GameState.Drawing);
        m_SleepCount = 0;
    }

    /// <summary> キャラが眠った時の処理 </summary>
    public static void SleepCharacter()
    {
        m_SleepCount++;
        SetGameState(GameState.Drawing);
        m_UpdSleepText();
        m_RestartDraw();
    }

    public static void StageClear()
    {
        SetGameState(GameState.Goal);
        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.ClearSE);

        StageDataUtility.SetStageScore(m_CullentStage, m_SleepCount);
        DontDestroyCanvas.Instance.ChangeResultUIVisible(true);
        DontDestroyCanvas.Instance.ResultUI.SetStarLevel();
    }

    /// <summary> 現在の星の取得数に応じて鳴らすサウンドを増やす </summary>
    public static void PlayBGMs()
    {
        StageDataUtility.LoadData();
        var allStarLevel = StageDataUtility.GetTotalStarLevel();

        SoundManager.Instance.SetSubBGMVolume(allStarLevel);
        SoundManager.Instance.PlayMainBGM();
        SoundManager.Instance.PlayCode(allStarLevel >= SoundManager.PlayCodeScore);
        SoundManager.Instance.PlayBass(allStarLevel >= SoundManager.PlayBassScore);
        SoundManager.Instance.PlayMarimba(0);
    }

    /// <summary> クレジットを流せるかチェック </summary>
    public static bool CheckPlayCredit()
    {
        var datas = StageDataUtility.LoadData();
        if(StageDataUtility.StageDatas.SawCredit) { return false; }
        if (datas.StageScores.Any(data => data.StarLevel < 1)) { return false; }
        return true;
    }
}
