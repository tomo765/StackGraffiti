using UnityEngine;

public static partial class InputExtension
{
    public static bool MouseLeftDown => Input.GetMouseButtonDown(0);
    public static bool MouseLeftUp => Input.GetMouseButtonUp(0);

    public static Vector2 ScreenMousePos => Input.mousePosition;
    public static Vector2 WorldMousePos => Camera.main.ScreenToWorldPoint(ScreenMousePos);
}

public static partial class InputExtension
{
    public static bool OnMove => Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A);
    public static bool OnStopMove => Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A);

    public static bool StartJump => Input.GetKeyDown(KeyCode.Space);
    public static bool OnSleep => Input.GetKeyDown(KeyCode.E);
}