using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoadExtension
{
    public static bool IsFading = false;

    public static async Task LoadWithFade(string sceneName, AudioClip playClp)
    {
        IsFading = true;

        if (FadeCanvasUI.Instance == null)
        {
            MonoBehaviour.Instantiate(GeneralSettings.Instance.Prehab.FadeCanvasUI);
            await FadeCanvasUI.WaitUntiil(() => FadeCanvasUI.Instance != null);
        }

        if (FadeCanvasUI.Instance.OnFade) { return; }
        SoundManager.Instance?.PlayNewSE(playClp);
        FadeCanvasUI.Instance.StartFade();

        await FadeCanvasUI.Instance.IsCompleteFadeIn();
        SceneManager.LoadScene(sceneName);

        await FadeCanvasUI.Instance.IsCompleteFadeOut();
        DontDestroyCanvas.Instance.SetNewRenderCamera();
        FadeCanvasUI.Instance.FinishFade();

        IsFading = false;
    }
}