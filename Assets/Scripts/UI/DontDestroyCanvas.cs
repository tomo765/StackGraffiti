using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Canvas m_Canvas;


    private void Init()
    {
        DontDestroyOnLoad(gameObject);

        m_Canvas = GetComponent<Canvas>();
        m_Canvas.planeDistance = GeneralSettings.Instance.Priorities.DontDestroyCanvas;
        SetNewRenderCamera();
    }

    public void SetNewRenderCamera()
    {
        m_Canvas.worldCamera = Camera.main;
    }

    public void ChangeOptionUIVisible()
    {
        m_OptionUI ??= Instantiate(GeneralSettings.Instance.Prehab.OptionUI, transform);

        m_OptionUI.gameObject.SetActive(!m_OptionUI.gameObject.activeSelf);
    }

    public void ChangeResultUIVisible()
    {
        m_ResultUI ??= Instantiate(GeneralSettings.Instance.Prehab.ResultUI, transform);

        m_ResultUI.gameObject.SetActive(!m_ResultUI.gameObject.activeSelf);
    }
}
