using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private Image[] m_Stars;


    [SerializeField] private Button m_StageSelectBtn;
    [SerializeField] private Button m_ReturnTitleBtn;
    [SerializeField] private Button m_NextStageButton;

    public void Init()
    {
        m_StageSelectBtn.onClick.AddListener(async() =>
        {
            if (SceneLoadExtension.IsFading) { return; }
            var playCredit = await GameManager.CheckPlayCredit();
            if (playCredit) { return; }

            OnPushResultButton(Config.SceneNames.StageSelect).FireAndForget();
        });
        m_ReturnTitleBtn.onClick.AddListener(async() =>
        {
            if (SceneLoadExtension.IsFading) { return; }
            var playCredit = await GameManager.CheckPlayCredit();
            if (playCredit) { return; }

            OnPushResultButton(Config.SceneNames.Title).FireAndForget();
        });

        if(GameManager.IsLastStage)
        {
            m_NextStageButton.gameObject.SetActive(false);
            return;
        }
        m_NextStageButton.onClick.AddListener(async() =>
        {
            if (SceneLoadExtension.IsFading) { return; }
            var nextStage = GameManager.CullentStage;
            GameManager.StartStage(nextStage + 1);

            var playCredit = await GameManager.CheckPlayCredit();
            if (playCredit) { return; }
            await OnPushResultButton(Config.SceneNames.m_StageNames[(int)nextStage]);  //StageNamesの要素が StageType-1と同期している

            DontDestroyCanvas.Instance.ChangeStageIntroUIVisible(true);  //ToDo : StageSelectUI でも似た処理してるからメソッドにする
            DontDestroyCanvas.Instance.StageIntroUI.SetIntroText(GameManager.CullentStage.ToString(),
                                                                 GeneralSettings.Instance.StageInfos.GetStageTextEN((int)GameManager.CullentStage),
                                                                 GeneralSettings.Instance.StageInfos.GetStageTextJP((int)GameManager.CullentStage)
                                                                );
        });
    }

    private async Task OnPushResultButton(string nextScene)
    {
        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeIn);
        await SceneLoadExtension.StartFadeIn();

        EffectContainer.Instance.StopAllEffect();
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