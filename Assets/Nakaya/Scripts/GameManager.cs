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

    public static GameState GameState => m_GameState;
    public static StageType CullentStage => m_CullentStage;
    public static int SleepCount => m_SleepCount;


    public static void StartStage(StageType stg)
    {
        m_CullentStage = stg;
        m_SleepCount = 0;
    }

    public static void SleepCharacter() => m_SleepCount++;

    public static void Clear()
    {
        StageDataUtility.SetStageScore(m_CullentStage, m_SleepCount);

        SceneManager.LoadScene(Config.SceneNames.Result, LoadSceneMode.Additive);
    }
}
