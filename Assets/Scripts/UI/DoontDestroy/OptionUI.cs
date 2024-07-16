using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField] private GameObject m_AskDeleteData;
    [SerializeField] private Button m_ReturnButton;
    [SerializeField] private Button m_DeleteDataButton;
    [SerializeField] private TMP_Dropdown m_ScreenResizer;
    [SerializeField] private Slider m_BGMSlider;
    [SerializeField] private Slider m_SESlider;


    void Start()
    {
        SetReturnButton();
        SetDeleteDataButton();
        SetScreenResizer();
        SetBGMSlider();
        SetSESlider();
    }

    private void SetReturnButton()
    {
        m_ReturnButton.onClick.RemoveAllListeners();
        m_ReturnButton.onClick.AddListener(() =>
        {
            DontDestroyCanvas.Instance.ChangeOptionUIVisible(false);
            SoundManager.Instance.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
        });
    }

    private void SetDeleteDataButton()
    {
        m_DeleteDataButton.onClick.RemoveAllListeners();
        m_DeleteDataButton.onClick.AddListener(() =>
        {
            m_AskDeleteData.SetActive(true);
            SoundManager.Instance.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
        });
    }

    private void SetBGMSlider()
    {
        m_BGMSlider.value = SoundManager.Instance.BGMVol;
        m_BGMSlider.onValueChanged.AddListener((i) =>
        {
            SoundManager.Instance.SetBGMVol(i);
        });
    }

    private void SetSESlider()
    {
        m_SESlider.value = SoundManager.Instance.SEVol;
        m_SESlider.onValueChanged.AddListener((i) =>
        {
            SoundManager.Instance.SetSEVol(i);
        });
    }

    private void SetScreenResizer()
    {
        var names = ScreenSettings.GetAllSizeName();
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>(names.Length);
        foreach (var name in names)
        {
            options.Add(new TMP_Dropdown.OptionData(name));
        }
        m_ScreenResizer.options = options;

        m_ScreenResizer.value = ScreenSettings.GetCullentScreenSizeIndex();
        ScreenSettings.SetScreenSize((ScreenSize)m_ScreenResizer.value);
        m_ScreenResizer.onValueChanged.AddListener((i) =>
        {
            ScreenSettings.SetScreenSize((ScreenSize)i);
        });
    }
}
