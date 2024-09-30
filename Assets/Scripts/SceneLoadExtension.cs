using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary> フェードをしながらシーンを切り替えるクラス </summary>
public static class SceneLoadExtension
{
    private static bool m_IsFading = false;
    public static bool IsFading => m_IsFading;

    /// <summary> フェードイン開始 </summary>
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

    /// <summary> フェードイン終了、表示するシーンを読み込む </summary>
    /// <param name="sceneName"> 読み込むシーン名 </param>
    public static async Task FinishFadeIn(string sceneName)
    {
        FadeCanvasUI.Instance.FinishFadeIn();
        await SceneManager.LoadSceneAsync(sceneName);
        await FadeCanvasUI.Instance.WaitToFadeOut();
    }

    /// <summary> フェードアウト開始 </summary>
    public static async Task FadeOut()
    {
        FadeCanvasUI.Instance.StartFadeOut();
        await FadeCanvasUI.Instance.IsCompleteFadeOut();
        FadeCanvasUI.Instance.FinishFadeOut();

        DontDestroyCanvas.Instance.SetNewRenderCamera();

        m_IsFading = false;
    }
}