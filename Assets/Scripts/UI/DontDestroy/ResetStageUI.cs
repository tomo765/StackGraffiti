using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ResetStageUI : MonoBehaviour
{
    [SerializeField] private Button m_ResetButton;
    [SerializeField] private Button m_ReturnButton;

    private Func<bool> m_InputNameNow;
    private string m_StageName;
    void Start()
    {
        SetResetButton();
        SetReturnButton();
    }

    public void SetInputName(Func<bool> f)
    {
        m_InputNameNow = f;
    }

    public void SetStageName(string stageName) => m_StageName = stageName;

    private void SetResetButton()
    {
        m_ResetButton.onClick.RemoveAllListeners();
        m_ResetButton.onClick.AddListener(() =>
        {
            ResetStage().FireAndForget();
            gameObject.SetActive(false);
        });
    }

    private void SetReturnButton()
    {
        m_ReturnButton.onClick.RemoveAllListeners();
        m_ReturnButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private async Task ResetStage()
    {
        if (!DontDestroyCanvas.Instance.StageIntroUI.FinishFadeOut) { return; }
        if (m_InputNameNow()) { return; }
        if (GameManager.IsClear) { return; }
        if (SceneLoadExtension.IsFading) { return; }

        GameManager.Source.Cancel();
        GameManager.InitPlayState(GameManager.CullentStage);

        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeIn);
        await SceneLoadExtension.StartFadeIn();
        EffectContainer.Instance.StopAllEffect();
        await SceneLoadExtension.FinishFadeIn(m_StageName);
        SoundManager.Instance.PlayMarimba(0);
        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeOut);
        await SceneLoadExtension.FadeOut();
    }
}
