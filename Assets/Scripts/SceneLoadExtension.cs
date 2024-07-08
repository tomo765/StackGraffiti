using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoadExtension
{
    private static bool m_IsFading = false;
    public static bool IsFading => m_IsFading;

    public static async Task StartFadeIn()
    {
        if (m_IsFading) { return; }
        m_IsFading = true;

        if (FadeCanvasUI.Instance == null)
        {
            MonoBehaviour.Instantiate(GeneralSettings.Instance.Prehab.FadeCanvasUI);
            await TaskExtension.WaitUntiil(() => FadeCanvasUI.Instance != null);
        }

        FadeCanvasUI.Instance.StartFadeIn();
        await FadeCanvasUI.Instance.IsCompleteFadeIn();
    }

    public static async Task StartFadeWait(string sceneName)
    {
        FadeCanvasUI.Instance.StartWait();
        await SceneManager.LoadSceneAsync(sceneName);
        await FadeCanvasUI.Instance.WaitToFadeOut();
    }

    public static async Task StartFadeOut()
    {
        FadeCanvasUI.Instance.StartFadeOut();
        await FadeCanvasUI.Instance.IsCompleteFadeOut();
        FadeCanvasUI.Instance.FinishFadeOut();

        m_IsFading = false;
    }
}