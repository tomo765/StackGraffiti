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
            await TaskExtension.WaitUntiil(() => FadeCanvasUI.Instance != null);
        }

        if (FadeCanvasUI.Instance.OnFade()) { return; }  //ToDo : Žè‘O‚É != null‚ð‚Â‚¯‚½‚Ù‚¤‚ª‚¢‚¢‚©‚à
        SoundManager.Instance?.PlayNewSE(playClp);
        FadeCanvasUI.Instance.StartFade();

        await FadeCanvasUI.Instance.IsCompleteFadeIn();
        await SceneManager.LoadSceneAsync(sceneName);
        DontDestroyCanvas.Instance.SetNewRenderCamera();

        await FadeCanvasUI.Instance.IsCompleteFadeOut();
        FadeCanvasUI.Instance.FinishFade();

        IsFading = false;
    }
}