using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToDeleteDataUI : MonoBehaviour
{
    [SerializeField] private Button m_DeleteButton;
    [SerializeField] private Button m_UndeleteButton;

    void Start()
    {
        SetDeleteButton();
        SetUnDeleteButton();
    }

    private void SetDeleteButton()
    {
        m_DeleteButton.onClick.RemoveAllListeners();
        m_DeleteButton.onClick.AddListener(async() =>
        {
            StageDataUtility.SetNewData();
            gameObject.SetActive(false);

            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeIn);
            await SceneLoadExtension.StartFadeIn();
            await SceneLoadExtension.FinishFadeIn(Config.SceneNames.Title);
            DontDestroyCanvas.Instance.ChangeOptionUIVisible(false);
            SoundManager.Instance.SetSubBGMVolume(0);
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeOut);
            await SceneLoadExtension.FadeOut();
        });
    }

    private void SetUnDeleteButton()
    {
        m_UndeleteButton.onClick.RemoveAllListeners();
        m_UndeleteButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            SoundManager.Instance.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
        });
    }
}
