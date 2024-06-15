using Config;
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
    private int cullentPage = 1;

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
                                                 .ToArray();

        List<StageSelectButton> selectButtons = new List<StageSelectButton>(sceneNames.Length);

        int j = 1;
        for (int i = 0; i < sceneNames.Length; i++)
        {
            if (!sceneNames[i].Contains("Main_Stage")) { continue; }

            var btn = Instantiate(GeneralSettings.Instance.Prehab.StageSelectBtn, Vector3.zero, Quaternion.identity, m_ViewContent.transform);
            btn.Init(sceneNames[i], j);
            selectButtons.Add(btn);
            j++;
        }

        m_SelectButtons = new StageSelectButton[j-1];
        for(int i = 0; i < j-1; i++)
        {
            m_SelectButtons[i] = selectButtons[i];
        }
    }

    private void SetReturnTitleBtn()
    {
        m_ReturnTitleBtn.onClick.RemoveAllListeners();
        m_ReturnTitleBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(Config.SceneNames.Title);
            SoundManager.Instance.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
        });
    }
}