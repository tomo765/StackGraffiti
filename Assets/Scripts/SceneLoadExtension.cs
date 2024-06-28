using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoadExtension
{
    public static async void LoadWithFade(string load, AudioClip playClp)
    {
        if (FadeCanvasUI.Instance == null)
        {
            MonoBehaviour.Instantiate(GeneralSettings.Instance.Prehab.FadeCanvasUI);
            await FadeCanvasUI.WaitUntiil(() => FadeCanvasUI.Instance != null);
        }

        if (FadeCanvasUI.Instance.OnFade) { return; }
        SoundManager.Instance?.PlayNewSE(playClp);
        FadeCanvasUI.Instance.StartFade();

        await FadeCanvasUI.Instance.IsCompleteFadeIn();
        SceneManager.LoadScene(load);

        await FadeCanvasUI.Instance.IsCompleteFadeOut();
        FadeCanvasUI.Instance.FinishFade();
    }
}