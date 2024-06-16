using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Drawing,
    Playing,
    Goal
}

public static class GameManager
{
    private static GameState m_GameState = GameState.Drawing;
    private static StageType m_CullentStage;
    private static int m_SleepCount = 0;
    private static Vector2 m_CharacterSpawnPos;

    private static Action m_OnCharacterSleep;

    public static GameState GameState => m_GameState;
    public static bool IsDrawing => m_GameState == GameState.Drawing;
    public static bool IsPlaying => m_GameState == GameState.Playing;
    public static bool IsClear => m_GameState == GameState.Goal;

    public static StageType CullentStage => m_CullentStage;

    public static int SleepCount => m_SleepCount;
    public static Vector2 SpawnPos => m_CharacterSpawnPos;


    public static void SetGameState(GameState state) => m_GameState = state;
    public static void SetSpawnPos(Vector2 pos) => m_CharacterSpawnPos = pos;
    public static void AddCharaSleepAction(Action onSleep) => m_OnCharacterSleep += onSleep;

    public static void StartStage(StageType stg)
    {
        m_CullentStage = stg;
        m_OnCharacterSleep = null;
        m_SleepCount = 0;
    }

    public static void SleepCharacter()
    {
        AddSleepCount();
        SetGameState(GameState.Drawing);
        m_OnCharacterSleep();
    }

    public static void AddSleepCount() => m_SleepCount++;

    public static void StageClear()
    {
        StageDataUtility.SetStageScore(m_CullentStage, m_SleepCount);

        SceneManager.LoadScene(Config.SceneNames.Result, LoadSceneMode.Additive);
    }
}
