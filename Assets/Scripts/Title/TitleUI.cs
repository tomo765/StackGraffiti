using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private Button m_StartButton;
    [SerializeField] private Button m_FinishButton;

    void Start()
    {
        SetStartButton();
        SetFinishButton();
    }

    private void SetStartButton()
    {
        m_StartButton.onClick.RemoveAllListeners();
        m_StartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(Config.SceneNames.StageSelect);
            SoundManager.Instance.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
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
