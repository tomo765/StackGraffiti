using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField] private Button m_ReturnButton;
    [SerializeField] private TMP_Dropdown m_ScreenResizer;
    [SerializeField] private Slider m_BGMSlider;
    [SerializeField] private Slider m_SESlider;


    void Start()
    {
        SetReturnButton();
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
        m_ScreenResizer.options = new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData(ScreenSize._FullScreen.ToString().Replace("_", "")),
            new TMP_Dropdown.OptionData(ScreenSize._1920x1080.ToString().Replace("_", "")),
            new TMP_Dropdown.OptionData(ScreenSize._1280x720.ToString().Replace("_", "")),
            new TMP_Dropdown.OptionData(ScreenSize._960x540.ToString().Replace("_", ""))
        };

        m_ScreenResizer.value = ScreenSettings.GetCullentScreenSizeIndex();
        m_ScreenResizer.onValueChanged.AddListener((i) =>
        {
            ScreenSettings.SetScreenSize((ScreenSize)i);
        });
    }
}
