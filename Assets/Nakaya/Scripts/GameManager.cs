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
    private static Action m_OnCharacterSleep;

    public static GameState GameState => m_GameState;
    public static StageType CullentStage => m_CullentStage;
    public static int SleepCount => m_SleepCount;


    public static void SetGameState(GameState state) => m_GameState = state;
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
