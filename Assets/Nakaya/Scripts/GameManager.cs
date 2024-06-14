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
    private static StageState m_CullentStage;
    private static int m_SleepCount = 0;

    public static GameState GameState => m_GameState;
    public static StageState CullentStage => m_CullentStage;
    public static int SleepCount => m_SleepCount;


    public static void StartStage(StageState stg)
    {
        m_CullentStage = stg;
        m_SleepCount = 0;
    }

    public static void SetCullentStage(StageState stg) => m_CullentStage = stg;

    public static void Clear()
    {
        SceneManager.LoadScene("Result", LoadSceneMode.Additive);
    }
}
