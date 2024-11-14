using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayUI : MonoBehaviour
{

    [SerializeField] private Image[] m_Pages;  //�V�ѕ��̃y�[�W�S��
    private int m_CullentShowPage;

    [SerializeField] private Button m_ShowPreviousButton;  //�O�̃y�[�W��\������{�^��
    [SerializeField] private Button m_ShowNextButton;  //���̃y�[�W��\������{�^��

    private void Start()
    {
        gameObject.SetActive(false);
        SetPreviousButton();
        SetNextButton();
    }

    private void SetPreviousButton()
    {
        m_ShowPreviousButton.onClick.RemoveAllListeners();
        m_ShowPreviousButton.onClick.AddListener(() =>
        {
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
            if (m_CullentShowPage == 0) { return; }
            m_Pages[m_CullentShowPage].gameObject.SetActive(false);
            m_Pages[--m_CullentShowPage].gameObject.SetActive(true);
        });
    }
    private void SetNextButton()
    {
        m_ShowNextButton.onClick.RemoveAllListeners();
        m_ShowNextButton.onClick.AddListener(() =>
        {
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
            if (m_CullentShowPage == m_Pages.Length - 1) { return; }
            m_Pages[m_CullentShowPage].gameObject.SetActive(false);
            m_Pages[++m_CullentShowPage].gameObject.SetActive(true);
        });
    }
}
