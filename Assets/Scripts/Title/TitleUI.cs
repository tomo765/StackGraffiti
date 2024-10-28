using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private Button m_StartButton;
    [SerializeField] private Button m_FinishButton;

    [SerializeField] private VideoPlayer m_Video;

    void Start()
    {
        SetStartButton();
        SetFinishButton();

        GameManager.CheckStarLevel();
        SetVideoPlayer();
    }

    private void SetStartButton()
    {
        m_StartButton.onClick.RemoveAllListeners();
        m_StartButton.onClick.AddListener(async() =>
        {
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeIn);
            await SceneLoadExtension.StartFadeIn();
            await SceneLoadExtension.StartFadeWait(Config.SceneNames.StageSelect);
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeOut);
            await SceneLoadExtension.StartFadeOut();
        });
    }

    private void SetVideoPlayer()
    {
        m_Video.Prepare();
        m_Video.prepareCompleted += OnPrepareCompleted;
        void OnPrepareCompleted(VideoPlayer player) => player.Play();
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
