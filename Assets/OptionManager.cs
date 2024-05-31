using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    private SoundManager m_SoundManager => FindObjectOfType<SoundManager>();
    private SceneManagings m_SceneManagings => FindObjectOfType<SceneManagings>();

    [SerializeField] private Button returnButton;

    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SESlider;



    void Start()
    {
        InitSlider();
    }

    public void InitSlider()
    {
        returnButton.onClick.AddListener(() =>
        {
            m_SceneManagings.SetOptionState();
            SceneManager.UnloadSceneAsync("OptionScene");
        });

        BGMSlider.value = m_SoundManager.m_BgmVol;
        SESlider.value = m_SoundManager.m_SeVol;

        BGMSlider.onValueChanged.AddListener((f) =>
        {
            m_SoundManager.SetBGMVol(f);
        });
        SESlider.onValueChanged.AddListener((f) =>
        {
            m_SoundManager.SetSEVol(f);
        });
    }
}
