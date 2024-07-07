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
        m_StageSelectBtn.onClick.AddListener(() => OnPushResultButton(Config.SceneNames.StageSelect));
        m_ReturnTitleBtn.onClick.AddListener(() => OnPushResultButton(Config.SceneNames.Title));


        if(GameManager.IsLastStage)
        {
            m_NextStageButton.gameObject.SetActive(false);
            return;
        }
        m_NextStageButton.onClick.AddListener(() =>
        {
            OnPushResultButton(Config.SceneNames.m_StageNames[(int)GameManager.CullentStage]);
            GameManager.StartStage(GameManager.CullentStage + 1);
        });
    }

    private async void OnPushResultButton(string nextScene)
    {
        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeIn);
        await SceneLoadExtension.StartFadeIn();

        EffectContainer.Instance.StopEffect<ConfettiEffect>();
        GameManager.CheckStarLevel();
        DontDestroyCanvas.Instance.ChangeResultUIVisible(false);

        await SceneLoadExtension.StartFadeWait(nextScene);
        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeOut);
        await SceneLoadExtension.StartFadeOut();
    }

    public void SetStar()
    {
        int level = GeneralSettings.Instance.StageInfos.GetCullentLevel(GameManager.CullentStage, GameManager.SleepCount);
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