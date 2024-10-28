using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenSettings
{
    private static ScreenSize m_ScreenSize;

    //value : (width, height, isFullScreen)
    private static readonly Dictionary<ScreenSize, (int, int, bool)> m_ScreenValues = new Dictionary<ScreenSize, (int, int, bool)>()
    {
        { ScreenSize._FullScreen, (1920, 1080, true) },
        { ScreenSize._1920x1080, (1920, 1080, false)},
        { ScreenSize._1280x720, (1280, 720, false)},
        { ScreenSize._960x540, (960, 540, false)}
    };

    public static ScreenSize ScreenSize => m_ScreenSize;

    public static void SetScreenSize(ScreenSize newSize)
    {
        var newScreenValue = m_ScreenValues[newSize];
        Screen.SetResolution(newScreenValue.Item1, newScreenValue.Item2, newScreenValue.Item3);
    }

    public static int GetCullentScreenSizeIndex()
    {
        return Screen.fullScreen ? 0 : Screen.width == 1920 ? 1 : Screen.width == 1280 ? 2 : 3;
    }
    public static string[] GetAllSizeName()
    {
        int index = 0;
        string[] names = new string[m_ScreenValues.Count];

        foreach(var key in m_ScreenValues.Keys)
        {
            names[index] = key.ToStr();
            index++;
        }
        return names;
    }
}

public enum ScreenSize  //ToDo : 画面サイズ変更できるようにする
{
    _FullScreen,
    _1920x1080,
    _1280x720,
    _960x540
}
public static class ScreenSizeExtension
{
    public static string ToStr(this ScreenSize size) => size.ToString().Replace("_", "");
}