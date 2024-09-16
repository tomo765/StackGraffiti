using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �����̃V�[���Ŏg��UI��ێ����� </summary>
public class DontDestroyCanvas : MonoBehaviour
{
    private static DontDestroyCanvas instance;
    public static DontDestroyCanvas Instance
    {
        get
        {
            if(instance == null)
            {
                instance = Instantiate(GeneralSettings.Instance.Prehab.DontDestroyCanvas);
                instance.Init();
            }

            return instance;
        }
    }

    private OptionUI m_OptionUI;
    private ResultUI m_ResultUI;
    private StageIntroUI m_StageIntroUI;

    private Canvas m_Canvas;

    public ResultUI ResultUI => m_ResultUI;
    public StageIntroUI StageIntroUI => m_StageIntroUI;

    private void Init()
    {
        DontDestroyOnLoad(gameObject);

        m_Canvas = GetComponent<Canvas>();
        m_Canvas.planeDistance = GeneralSettings.Instance.Priorities.DontDestroyCanvas;
        SetNewRenderCamera();
    }

    /// <summary> �V�[���؂�ւ����ɎQ�Ƃ���J�������ēx�ݒ� </summary>
    public void SetNewRenderCamera()
    {
        m_Canvas.worldCamera = Camera.main;
    }

    public void ChangeOptionUIVisible(bool b)
    {
        m_OptionUI ??= Instantiate(GeneralSettings.Instance.Prehab.OptionUI, transform);

        m_OptionUI.gameObject.SetActive(b);
    }

    public void ChangeResultUIVisible(bool b)
    {
        m_ResultUI ??= Instantiate(GeneralSettings.Instance.Prehab.ResultUI, transform);

        m_ResultUI.gameObject.SetActive(b);
    }

    public void ChangeStageIntroUIVisible(bool b)
    {
        m_StageIntroUI ??= Instantiate(GeneralSettings.Instance.Prehab.StageIntroUI, transform);

        m_StageIntroUI.gameObject.SetActive(b);
        m_StageIntroUI.PlayIntro().FireAndForget();
    }

    public void InitResultUI() => m_ResultUI.Init();
}
