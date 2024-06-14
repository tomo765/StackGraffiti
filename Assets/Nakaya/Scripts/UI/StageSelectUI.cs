using Config;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject m_ViewContant;
    [SerializeField] private Button m_NextPageBtn;
    [SerializeField] private Button m_BeforePageBtn;

    private StageSelectButton[] m_SelectButtons;
    private int cullentPage = 1;

    void Start()
    {
        SetSelectButtons();
        SetPageButtons();
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

            var btn = Instantiate(GeneralSettings.Instance.Prehab.StageSelectBtn, Vector3.zero, Quaternion.identity, m_ViewContant.transform);
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

    private void SetPageButtons()
    {
        m_NextPageBtn.interactable = (m_SelectButtons.Length / 5f > 1);
        m_NextPageBtn.onClick.AddListener(() => 
        {
            if(cullentPage == 1)
            {
                m_BeforePageBtn.interactable = true;
            }
            cullentPage++;

            if (cullentPage == m_SelectButtons.Length / 5)
            {
                m_NextPageBtn.interactable = false;
            }
            ChangeVisibleStaeg();
        });

        m_BeforePageBtn.interactable = false;
        m_BeforePageBtn.onClick.AddListener(() =>
        {
            if (cullentPage == m_SelectButtons.Length / 5)
            {
                m_NextPageBtn.interactable = true;
            }
            cullentPage--;

            if (cullentPage == 1)
            {
                m_BeforePageBtn.interactable = false;
            }
            ChangeVisibleStaeg();
        });
    }

    private void ChangeVisibleStaeg()
    {
        for (int i = 1; i <= m_SelectButtons.Length; i++)
        {
            Debug.Log(m_SelectButtons[i-1]);
            bool isActive = i >= (5 * cullentPage - 4) && i <= (5 * cullentPage);  //1`5, 6`10...‚Å•\Ž¦A”ñ•\Ž¦‚ð•ª‚¯‚é
            m_SelectButtons[i-1].gameObject.SetActive(isActive);
        }
    }
}
