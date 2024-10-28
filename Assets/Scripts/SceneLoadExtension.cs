using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary> �t�F�[�h�����Ȃ���V�[����؂�ւ���N���X </summary>
public static class SceneLoadExtension
{
    private static bool m_IsFading = false;
    public static bool IsFading => m_IsFading;

    /// <summary> �t�F�[�h�C���J�n </summary>
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

    /// <summary> �t�F�[�h�C���I���A�\������V�[����ǂݍ��� </summary>
    /// <param name="sceneName"> �ǂݍ��ރV�[���� </param>
    public static async Task FinishFadeIn(string sceneName)
    {
        FadeCanvasUI.Instance.FinishFadeIn();
        await SceneManager.LoadSceneAsync(sceneName);
        await FadeCanvasUI.Instance.WaitToFadeOut();
    }

    /// <summary> �t�F�[�h�A�E�g�J�n </summary>
    public static async Task FadeOut()
    {
        FadeCanvasUI.Instance.StartFadeOut();
        await FadeCanvasUI.Instance.IsCompleteFadeOut();
        FadeCanvasUI.Instance.FinishFadeOut();

        DontDestroyCanvas.Instance.SetNewRenderCamera();

        m_IsFading = false;
    }
}