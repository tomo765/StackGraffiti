using System.Collections;
using UnityEditor;

public static class EditorCoroutine
{
    public static void StartEditorCoroutine(IEnumerator coroutine)
    {
        EditorApplication.update += () => EditorApplicationCoroutine(coroutine);
    }

    private static void EditorApplicationCoroutine(IEnumerator coroutine)
    {
        if (!coroutine.MoveNext())
        {
            EditorApplication.update -= () => EditorApplicationCoroutine(coroutine);
        }
    }
}
