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

    void Start()
    {
        Init();
        SetStar();
    }

    private void Init()
    {
        m_StageSelectBtn.onClick.AddListener(() =>
        {
            FadeExtension.LoadScene(Config.SceneNames.StageSelect, GeneralSettings.Instance.Sound.SelectSE);
            //SceneManager.LoadScene(Config.SceneNames.StageSelect);
            //SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
        });
        m_ReturnTitleBtn.onClick.AddListener(() =>
        {
            FadeExtension.LoadScene(Config.SceneNames.Title, GeneralSettings.Instance.Sound.SelectSE);
            //SceneManager.LoadScene(Config.SceneNames.Title);
            //SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
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