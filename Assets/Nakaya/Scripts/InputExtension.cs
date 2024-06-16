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
    public static bool MoveRight => Input.GetKey(KeyCode.D);
}