using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPText = TMPro.TextMeshProUGUI;

public class GameCanvasUI : MonoBehaviour
{
    [SerializeField] TMPText m_SleepCount;
    [SerializeField] DrawUI m_DrawUI;
    [SerializeField] private Button m_LookButton;
    [SerializeField] private TMPText m_LookButtonText;

    private bool m_isVisible = true;

    private const string ViewStage = "�X�e�[�W������";
    private const string CreateCharacter = "�L�����N�^�[�����";


    void Start()
    {
        Init();
        UpdateSleepText();
    }

    private void Init()
    {
        m_LookButton.onClick.RemoveAllListeners();
        m_LookButton.onClick.AddListener(() =>
        {
            ChangeVisible();
            m_DrawUI.gameObject.SetActive(m_isVisible);
            m_LookButtonText.text = m_isVisible ? CreateCharacter : ViewStage;
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
        });
        m_DrawUI.gameObject.SetActive(true);

        GameManager.SetUpdateSleepText(UpdateSleepText);
        GameManager.SetRestartDrawing(RestartDrawing);
    }

    private void ChangeVisible()
    {
        m_isVisible = !m_isVisible;
    }

    private void UpdateSleepText()
    {
        m_SleepCount.text = "�������� : " + GameManager.SleepCount.ToString();
    }

    private void RestartDrawing()
    {
        m_DrawUI.gameObject.SetActive(true);
        m_LookButton.gameObject.SetActive(true);
    }
}
