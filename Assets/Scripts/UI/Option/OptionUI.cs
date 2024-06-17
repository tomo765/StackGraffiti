using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField] private Button m_ReturnButton;

    [SerializeField] private Slider m_BGMSlider;
    [SerializeField] private Slider m_SESlider;


    void Start()
    {
        SetReturnButton();
        SetBGMSlider();
        SetSESlider();
    }

    private void SetReturnButton()
    {
        m_ReturnButton.onClick.RemoveAllListeners();
        m_ReturnButton.onClick.AddListener(() =>
        {
            SceneManager.UnloadSceneAsync(gameObject.scene);
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
}
