using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary> 複数のシーンで使うUIを保持する </summary>
public class DontDestroyCanvas : MonoBehaviour
{
    private static DontDestroyCanvas instance;
    public static DontDestroyCanvas Instance => instance;

    private OptionUI m_OptionUI;
    private ResultUI m_ResultUI;
    private HowToPlayUI m_HowToPlayUI;
    private StageIntroUI m_StageIntroUI;
    private ResetStageUI m_ResetStageUI;

    private Canvas m_Canvas;

    public OptionUI OptionUI => m_OptionUI;
    public ResultUI ResultUI => m_ResultUI;
    public StageIntroUI StageIntroUI => m_StageIntroUI;
    public ResetStageUI ResetStageUI => m_ResetStageUI;

    public bool IsShowResetUI => m_ResetStageUI.isActiveAndEnabled;
    public bool IsShowResultUI => m_ResultUI.isActiveAndEnabled;
    public bool IsShowStageIntro => m_StageIntroUI.isActiveAndEnabled;
    public bool IsShowHowToPlay => m_HowToPlayUI.isActiveAndEnabled;

    private void Init()
    {
        DontDestroyOnLoad(gameObject);

        m_Canvas = GetComponent<Canvas>();
        m_Canvas.planeDistance = GeneralSettings.Instance.Priorities.DontDestroyCanvas;

        SetNewRenderCamera();
    }

    public static DontDestroyCanvas CreateCanvas()
    {
        DontDestroyCanvas canvas = Instantiate(GeneralSettings.Instance.Prehab.DontDestroyCanvas);
        instance = canvas;
        canvas.Init();
        return canvas;
    }

    public void CreateAllUI()
    {
        m_OptionUI = Instantiate(GeneralSettings.Instance.Prehab.OptionUI, transform);
        m_ResultUI = Instantiate(GeneralSettings.Instance.Prehab.ResultUI, transform);
        m_HowToPlayUI = Instantiate(GeneralSettings.Instance.Prehab.HowToPlayUI, transform);
        m_StageIntroUI = Instantiate(GeneralSettings.Instance.Prehab.StageIntroUI, transform);
        m_ResetStageUI = Instantiate(GeneralSettings.Instance.Prehab.ResetStageUI, transform);

        InvisibleAllUI();
    }

    /// <summary> シーン切り替え時に参照するカメラを再度設定 </summary>
    public void SetNewRenderCamera()
    {
        m_Canvas.worldCamera = Camera.main;
    }

    public void ChangeOptionUIVisible(bool b)
    {
        m_OptionUI.gameObject.SetActive(b);
    }

    public void ChangeResultUIVisible(bool b)
    {
        m_ResultUI.gameObject.SetActive(b);
    }

    public void ChangeHowToPlayUIVisible(bool b)
    {
        m_HowToPlayUI.gameObject.SetActive(b);
    }

    public void ChangeStageIntroUIVisible(bool b)
    {
        m_StageIntroUI.gameObject.SetActive(b);
        m_StageIntroUI.PlayIntro().FireAndForget();
    }

    public void ChangeResetStageUIVisible(bool b)
    {
        if (SceneLoadExtension.IsFading) { return; }
        if (m_StageIntroUI.gameObject.activeSelf) { return; }
        m_ResetStageUI.gameObject.SetActive(b);
    }

    public void InvisibleAllUI()
    {
        m_OptionUI.gameObject.SetActive(false);
        m_ResultUI.gameObject.SetActive(false);
        m_HowToPlayUI.gameObject.SetActive(false);
        m_StageIntroUI.gameObject.SetActive(false);
        m_ResetStageUI.gameObject.SetActive(false);
    }

    public void InitResultUI() => m_ResultUI.Init();
}
