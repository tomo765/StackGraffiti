using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayUI : MonoBehaviour
{

    [SerializeField] private Image[] m_Pages;  //遊び方のページ全体
    private int m_CullentShowPage;

    [SerializeField] private Button m_ShowPreviousButton;  //前のページを表示するボタン
    [SerializeField] private Button m_ShowNextButton;  //次のページを表示するボタン

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
