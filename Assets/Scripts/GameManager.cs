using System;
using UnityEngine;
using System.Linq;
using System.Threading;

public enum GameState
{
    Drawing,
    Playing,
    Goal,
    HowToPlay
}

/// <summary> �X�e�[�W��ł̃X�e�[�g�Ȃǂ̊Ǘ������� </summary>
public static class GameManager
{
    private static Config.Language m_Language = Config.Language.English;

    /// <summary> DontDestroyCanvas���̃e�L�X�g�̃��[�J���C�Y�̊֐���ǉ�����  </summary>
    public static void AddStaticAction(Action action) => m_UpdateStaticText += action;
    private static Action m_UpdateStaticText;  //DontDestroyCanvas���̃e�L�X�g�̃��[�J���C�Y������


    /// <summary> �V�[����̃e�L�X�g�̃��[�J���C�Y�̊֐���ǉ�����  </summary>
    public static void AddSceneLocalizeAction(Action action) => m_UpdateSceneText += action;
    /// <summary> �V�[���؂�ւ����ɐ؂�ւ��O�ɒǉ������֐��̎Q�Ƃ�j������ </summary>
    public static void ResetSceneLocalizeAction() => m_UpdateSceneText = null;
    private static Action m_UpdateSceneText;   //�V�[����̃e�L�X�g�̃��[�J���C�Y������

    public static Config.Language Language
    {
        get { return m_Language; }
        set
        {
            if(m_Language == value) { return; }
            m_Language = value;
            m_UpdateStaticText();
            m_UpdateSceneText();
        }
    }


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
    public static bool IsLastStage => m_CullentStage == (StageLevel)Config.SceneNames.m_StageNames.Length;

    public static int SleepCount => m_SleepCount;
    public static GameObject SpawnArea => m_SpawnArea;
    public static CancellationTokenSource Source => m_Source;

    public static void SetGameState(GameState state) => m_GameState = state;
    public static void SetSpawnPos(GameObject pos) => m_SpawnArea = pos;
    public static void SetUpdateSleepText(Action onSleep) => m_UpdSleepText = onSleep;
    public static void SetRestartDrawing(Action reDraw) => m_RestartDraw = reDraw;

    /// <summary> �v���C�󋵂̏����� </summary>
    public static void InitPlayState(StageLevel stg)
    {
        m_Source = new CancellationTokenSource();
        m_CullentStage = stg;
        m_UpdSleepText = null;
        SetGameState(GameState.Drawing);
        m_SleepCount = 0;
    }

    /// <summary> �L���������������̏��� </summary>
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

    /// <summary> ���݂̐��̎擾���ɉ����Ė炷�T�E���h�𑝂₷ </summary>
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

    /// <summary> �N���W�b�g�𗬂��邩�`�F�b�N </summary>
    public static bool CheckPlayCredit()
    {
        var datas = StageDataUtility.LoadData();
        if(StageDataUtility.StageDatas.SawCredit) { return false; }
        if (datas.StageScores.Any(data => data.StarLevel < 1)) { return false; }
        return true;
    }
}
