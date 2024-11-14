using System.Collections;
using UnityEditor;

#if UNITY_EDITOR
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
#endif