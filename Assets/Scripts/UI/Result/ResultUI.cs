using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private Image[] m_Stars;


    [SerializeField] private Button m_StageSelectBtn;
    [SerializeField] private Button m_ReturnTitleBtn;
    [SerializeField] private Button m_NextStageButton;

    void Start()
    {
        Init();
        SetStar();
    }

    private void Init()
    {
        m_StageSelectBtn.onClick.AddListener(() =>
        {
            SceneLoadExtension.LoadWithFade(Config.SceneNames.StageSelect, GeneralSettings.Instance.Sound.FadeSE);
            DontDestroyCanvas.Instance.ChangeResultUIVisible();
        });
        m_ReturnTitleBtn.onClick.AddListener(() =>
        {
            SceneLoadExtension.LoadWithFade(Config.SceneNames.Title, GeneralSettings.Instance.Sound.FadeSE);
            DontDestroyCanvas.Instance.ChangeResultUIVisible();
        });
        m_NextStageButton.onClick.AddListener(() =>
        {
            SceneLoadExtension.LoadWithFade(Config.SceneNames.m_StageNames[(int)GameManager.CullentStage], GeneralSettings.Instance.Sound.FadeSE);
            GameManager.StartStage(GameManager.CullentStage + 1);
            DontDestroyCanvas.Instance.ChangeResultUIVisible();
        });
    }

    public void SetStar()
    {
        int level = GeneralSettings.Instance.StageEvals.GetCullentLevel(GameManager.CullentStage, GameManager.SleepCount);
        for (int i = 0; i < level; i++)
        {
            m_Stars[i].sprite = GeneralSettings.Instance.Sprite.ClearStar;
        }
        for(int i = m_Stars.Length - 1; i >= level; i--)
        {
            m_Stars[i].sprite = GeneralSettings.Instance.Sprite.UnclearStar;
        }
    }
}