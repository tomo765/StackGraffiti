using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    [SerializeField] private Button m_OptionButton;
    void Start()
    {
        SetOptionButton();
    }


    private void SetOptionButton()
    {
        m_OptionButton.onClick.RemoveAllListeners();
        m_OptionButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(Config.SceneNames.Option, LoadSceneMode.Additive);
            SoundManager.Instance.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
        });
    }
}
