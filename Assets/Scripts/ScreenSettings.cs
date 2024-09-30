using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ゲームのスクリーンサイズを設定する </summary>
public static class ScreenSettings
{
    private static readonly Dictionary<ScreenSize, (int, int, bool)> m_ScreenValues = new Dictionary<ScreenSize, (int, int, bool)>()
    {
      //{ ScreenSize , (width, height, isFullScreen)},
        { ScreenSize._FullScreen, (1920, 1080, true)},
        { ScreenSize._1920x1080, (1920, 1080, false)},
        { ScreenSize._1280x720, (1280, 720, false)},
        { ScreenSize._960x540, (960, 540, false)}
    };

    /// <summary> サイズ変更を適用 </summary>
    public static void SetScreenSize(ScreenSize newSize)
    {
        var newScreenValue = m_ScreenValues[newSize];
        Screen.SetResolution(newScreenValue.Item1, newScreenValue.Item2, newScreenValue.Item3);
    }

    /// <summary> m_ScreenValues の順で番号を取得する </summary>
    public static int GetCullentScreenSizeIndex()  //ToDo : m_ScreenValues の 
    {
        return Screen.fullScreen ? 0 : Screen.width == 1920 ? 1 : Screen.width == 1280 ? 2 : 3;
    }

    /// <summary> UI上での画面サイズの一覧を見る </summary>
    public static string[] GetAllSizeName()
    {
        int index = 0;
        string[] names = new string[m_ScreenValues.Count];

        foreach(var key in m_ScreenValues.Keys)
        {
            names[index] = key.ToString().Replace("_", "");
            index++;
        }
        return names;
    }
}

/// <summary> 使用可能な画面サイズを定義 </summary>
public enum ScreenSize
{
    _FullScreen,
    _1920x1080,
    _1280x720,
    _960x540
}