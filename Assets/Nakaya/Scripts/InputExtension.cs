using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class InputExtension
{
    public static bool MouseLeftDown => Input.GetMouseButtonDown(0);
    public static bool MouseLeftUp => Input.GetMouseButtonUp(0);

    public static Vector2 ScreenMousePpos => Input.mousePosition;
    public static Vector2 WorldMousePos => Camera.main.ScreenToWorldPoint(ScreenMousePpos);
}

public static partial class InputExtension
{
    public static bool MoveRight => Input.GetKey(KeyCode.D);
}