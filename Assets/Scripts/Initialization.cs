using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    private static bool initialized = false;

    void Awake()
    {
        if(initialized) { return; }

        if(DontDestroyCanvas.Instance == null)
        {
            DontDestroyCanvas.CreateCanvas().CreateAllUI();
        }

        SetCursor();

        initialized = true;
    }

    void SetCursor()
    {
        var useCursorInfo = GeneralSettings.Instance.Cursor.Default;
        Cursor.SetCursor(useCursorInfo.Item1, Vector2.zero, CursorMode.ForceSoftware);
    }
}
