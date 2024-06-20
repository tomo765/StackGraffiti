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
    public static bool OnMove => MoveKey && !GameManager.IsClear;
    public static bool OnStopMove => StopMoveKey && !GameManager.IsClear;

    public static bool StartJump => JumpKey && !GameManager.IsClear;
    public static bool OnSleep => SleepKey && !GameManager.IsClear;

    private static bool MoveKey => Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A);
    private static bool StopMoveKey => Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A);
    private static bool JumpKey => Input.GetKeyDown(KeyCode.Space);
    private static bool SleepKey => Input.GetKeyDown(KeyCode.E);

    public static bool EscapeStage => Input.GetKeyDown(KeyCode.Escape);
    public static bool ResetStage => Input.GetKeyDown(KeyCode.G);

    public static Vector2 MoveVec(float i) => new Vector2(Input.GetAxisRaw("Horizontal") * i, 0);
}