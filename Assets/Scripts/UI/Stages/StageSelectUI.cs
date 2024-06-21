using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject m_ViewContent;
    [SerializeField] private Button m_ReturnTitleBtn;
    [SerializeField] private Scrollbar m_VerticalBar;

    private StageSelectButton[] m_SelectButtons;

    void Start()
    {
        StageDataUtility.FindData();

        SetSelectButtons();
        SetReturnTitleBtn();

        m_VerticalBar.value = 1;
    }

    private void SetSelectButtons()
    {
        string[] sceneNames = Config.SceneNames.m_StageNames;

        m_SelectButtons = new StageSelectButton[sceneNames.Length];

        for (int i = 0; i < sceneNames.Length; i++)
        {
            var btn = Instantiate(GeneralSettings.Instance.Prehab.StageSelectBtn, Vector3.zero, Quaternion.identity, m_ViewContent.transform);
            btn.Init(sceneNames[i], i+1);
            m_SelectButtons[i] = btn;
        }
    }

    private void SetReturnTitleBtn()
    {
        m_ReturnTitleBtn.onClick.RemoveAllListeners();
        m_ReturnTitleBtn.onClick.AddListener(() =>
        {
            FadeExtension.LoadScene(GeneralSettings.Instance.Sound.SelectSE, Config.SceneNames.Title);
            //SceneManager.LoadScene(Config.SceneNames.Title);
            //SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
        });
    }
}