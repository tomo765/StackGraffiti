using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
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
        m_Video.Play();
    }

    private void SetStartButton()
    {
        m_StartButton.onClick.RemoveAllListeners();
        m_StartButton.onClick.AddListener(() =>
        {
            _ = SceneLoadExtension.LoadWithFade(Config.SceneNames.StageSelect, GeneralSettings.Instance.Sound.FadeSE);
        });
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
