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

    private StageSelectButton[] m_SelectButtons;

    void Start()
    {
        StageDataUtility.FindData();

        SetSelectButtons();
        SetReturnTitleBtn();
    }

    private void SetSelectButtons()
    {
        string[] sceneNames = EditorBuildSettings.scenes
                                                 .Where(scene => scene.enabled)
                                                 .Select(scene => Path.GetFileNameWithoutExtension(scene.path))
                                                 .Where(name => name.Contains("Main_Stage"))
                                                 .ToArray();

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
            SceneManager.LoadScene(Config.SceneNames.Title);
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
        });
    }
}