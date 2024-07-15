using UnityEngine;

public static partial class InputExtension
{
    public static bool MouseLeftDown => Input.GetMouseButtonDown(0);
    public static bool MouseLeftPush => Input.GetMouseButton(0);
    public static bool MouseLeftUp => Input.GetMouseButtonUp(0);

    public static Vector2 ScreenMousePos => Input.mousePosition;
    public static Vector2 WorldMousePos => Camera.main.ScreenToWorldPoint(ScreenMousePos);
}

public static partial class InputExtension
{
    public static bool OnMove => MoveKey && !GameManager.IsClear;
    private static bool MoveKey => MoveLeft || MoveRight;
    private static bool MoveLeft => Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
    public static bool MoveRight => Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

    public static bool StartJump => JumpKey && !GameManager.IsClear;
    private static bool JumpKey => Input.GetKeyDown(KeyCode.Space);
    private static bool SleepKey => Input.GetKeyDown(KeyCode.E);

    public static bool OnSleep => SleepKey && !GameManager.IsClear;
    public static bool EscapeStage => Input.GetKeyDown(KeyCode.Escape);
    public static bool ResetStage => Input.GetKeyDown(KeyCode.R);
    public static bool ShowHowToPlay => Input.GetKeyDown(KeyCode.Q);

    public static Vector2 MoveByAxis(float i) => new Vector2(Input.GetAxisRaw("Horizontal") * i, 0);
    public static Vector2 MoveByKey(float i) => new Vector2(GetMoveDir() * i, 0);
    private static float GetMoveDir() => (MoveLeft ? -1 : 0) + (MoveRight ? 1 : 0);
}