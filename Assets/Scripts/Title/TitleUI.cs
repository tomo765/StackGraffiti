using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary> ゲームタイトルのUI </summary>
public class TitleUI : MonoBehaviour
{
    [SerializeField] private Button m_StartButton;
    [SerializeField] private Button m_FinishButton;

    [SerializeField] private VideoPlayer m_Video;

    void Start()
    {
        SetStartButton();
        SetFinishButton();

        GameManager.PlayBGMs();
        SetVideoPlayer();
    }

    private void SetStartButton()
    {
        m_StartButton.onClick.RemoveAllListeners();
        m_StartButton.onClick.AddListener(async() =>
        {
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeIn);
            await SceneLoadExtension.StartFadeIn();
            await SceneLoadExtension.FinishFadeIn(Config.SceneNames.StageSelect);
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeOut);
            await SceneLoadExtension.FadeOut();
        });
    }

    private void SetVideoPlayer()
    {
        m_Video.Play();
        //m_Video.SetLoopPointReached(() => m_Video.Play());
    }

    private void SetFinishButton()
    {
        m_FinishButton.onClick.RemoveAllListeners();
        m_FinishButton.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
        });
    }
}
