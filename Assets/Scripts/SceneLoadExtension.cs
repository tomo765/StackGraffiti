using Config;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoadExtension
{
    public static bool IsFading = false;

    public static async Task StartFadeIn(AudioClip playClp)
    {
        IsFading = true;

        if (FadeCanvasUI.Instance == null)
        {
            MonoBehaviour.Instantiate(GeneralSettings.Instance.Prehab.FadeCanvasUI);
            await TaskExtension.WaitUntiil(() => FadeCanvasUI.Instance != null);
        }

        if (FadeCanvasUI.Instance.OnFade()) { return; }
        SoundManager.Instance?.PlayNewSE(playClp);
        FadeCanvasUI.Instance.StartFadeIn();

        await FadeCanvasUI.Instance.IsCompleteFadeIn();
    }

    public static async Task StartFadeWait(string sceneName)
    {
        FadeCanvasUI.Instance.StartWait();
        await SceneManager.LoadSceneAsync(sceneName);
        await FadeCanvasUI.Instance.WaitToFadeOut();
    }

    public static async Task StartFadeOut(AudioClip playClp)
    {
        FadeCanvasUI.Instance.StartFadeOut();
        SoundManager.Instance?.PlayNewSE(playClp);
        await FadeCanvasUI.Instance.IsCompleteFadeOut();
        FadeCanvasUI.Instance.FinishFadeOut();

        IsFading = false;
    }
}