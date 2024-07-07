using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoadExtension
{
    public static bool IsFading = false;

    public static async Task StartFadeIn()
    {
        IsFading = true;

        if (FadeCanvasUI.Instance == null)
        {
            MonoBehaviour.Instantiate(GeneralSettings.Instance.Prehab.FadeCanvasUI);
            await TaskExtension.WaitUntiil(() => FadeCanvasUI.Instance != null);
        }

        if (FadeCanvasUI.Instance.OnFade()) { return; }
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

        IsFading = false;
    }
}