using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPText = TMPro.TextMeshProUGUI;

public class GameCanvasUI : MonoBehaviour
{
    [SerializeField] TMPText m_SleepCount;
    [SerializeField] DrawUI m_DrawUI;
    [SerializeField] private Button m_ViewStageBtn;
    [SerializeField] private Button m_ViewDrawUIBtn;

    private bool m_isVisible = false;

    public bool IsInputNameNow => m_DrawUI.IsInputNow;


    void Start()
    {
        SetViewStageButton();
        SetViewDrawUIButton();

        Init();
        m_DrawUI.Init(this, m_ViewDrawUIBtn);
        UpdateSleepText();
    }

    void SetViewStageButton()
    {
        m_ViewStageBtn.onClick.RemoveAllListeners();
        m_ViewStageBtn.onClick.AddListener(() =>
        {
            ChangeVisible();

            m_DrawUI.gameObject.SetActive(m_isVisible);
            m_ViewDrawUIBtn.gameObject.SetActive(!m_isVisible);
            CharacterCreator.SetCreatingCharaVisible(m_isVisible);
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
        });
    }
    void SetViewDrawUIButton()
    {
        m_ViewDrawUIBtn.onClick.RemoveAllListeners();
        m_ViewDrawUIBtn.onClick.AddListener(() =>
        {
            ChangeVisible();

            m_DrawUI.gameObject.SetActive(m_isVisible);
            m_ViewDrawUIBtn.gameObject.SetActive(!m_isVisible);
            CharacterCreator.SetCreatingCharaVisible(m_isVisible);
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
        });
    }

    private void Init()
    {
        m_DrawUI.gameObject.SetActive(m_isVisible);
        m_ViewDrawUIBtn.gameObject.SetActive(!m_isVisible);

        GameManager.SetUpdateSleepText(UpdateSleepText);
        GameManager.SetRestartDrawing(RestartDrawing);

        GetComponent<Canvas>().planeDistance = GeneralSettings.Instance.Priorities.StageCanvas;
    }

    private void ChangeVisible()
    {
        m_isVisible = !m_isVisible;
    }

    private void UpdateSleepText()
    {
        m_SleepCount.text = "Sleep Count : " + GameManager.SleepCount.ToString();
    }

    private void RestartDrawing()
    {
        m_DrawUI.gameObject.SetActive(true);
    }
}
