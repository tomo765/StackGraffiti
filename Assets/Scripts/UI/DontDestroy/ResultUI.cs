using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary> ステージクリア後のリザルト画面 </summary>
public class ResultUI : MonoBehaviour
{
    [SerializeField] private Image[] m_Stars;


    [SerializeField] private Button m_StageSelectBtn;
    [SerializeField] private Button m_ReturnTitleBtn;
    [SerializeField] private Button m_NextStageButton;

    public void Init()  //ToDo : クレジットを流した後にセレクト画面を押していてもたいとるに戻る
    {
        //セレクト画面に戻るボタンの処理登録
        m_StageSelectBtn.onClick.AddListener(() =>
        {
            string loadScene = Config.SceneNames.StageSelect;

            if (SceneLoadExtension.IsFading) { return; }

            var playCredit = GameManager.CheckPlayCredit();
            if (playCredit) { loadScene = Config.SceneNames.Credit; }
            else { GameManager.PlayBGMs(); }

            OnPushResultButton(loadScene).FireAndForget();
        });

        //タイトルに戻るボタンの処理登録
        m_ReturnTitleBtn.onClick.AddListener(() =>
        {
            string loadScene = Config.SceneNames.Title;

            if (SceneLoadExtension.IsFading) { return; }

            var playCredit = GameManager.CheckPlayCredit();
            if (playCredit) { loadScene = Config.SceneNames.Credit; }
            else { GameManager.PlayBGMs(); }

            OnPushResultButton(loadScene).FireAndForget();
        });

        //次のステージに進むボタンの処理登録
        if(GameManager.IsLastStage)
        {
            m_NextStageButton.gameObject.SetActive(false);
        }
        else
        {
            m_NextStageButton.gameObject.SetActive(true);
            m_NextStageButton.onClick.AddListener(async () =>
            {
                StageLevel nextStage = GameManager.CullentStage + 1;
                string loadScene = Config.SceneNames.m_StageNames[(int)nextStage - 1];

                if (SceneLoadExtension.IsFading) { return; }
                var playCredit = GameManager.CheckPlayCredit();
                if (playCredit) { loadScene = Config.SceneNames.Credit; }
                else { GameManager.PlayBGMs(); }

                await OnPushResultButton(loadScene);
                if (playCredit) { return; }

                GameManager.InitPlayState(nextStage);
                DontDestroyCanvas.Instance.ChangeStageIntroUIVisible(true);  //ToDo : StageSelectUI でも似た処理してるからメソッドにする？
                DontDestroyCanvas.Instance.StageIntroUI.SetIntroText(GameManager.CullentStage.ToString(),
                                                                     GeneralSettings.Instance.StageInfos.GetStageTextEN((int)GameManager.CullentStage),
                                                                     GeneralSettings.Instance.StageInfos.GetStageTextJP((int)GameManager.CullentStage)
                                                                    );
            });
        }
    }

    /// <summary> リザルト画面のボタンを押したときの処理 </summary>
    private async Task OnPushResultButton(string nextScene)
    {
        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeIn);
        await SceneLoadExtension.StartFadeIn();

        EffectContainer.Instance.StopAllEffect();
        DontDestroyCanvas.Instance.ChangeResultUIVisible(false);

        await SceneLoadExtension.FinishFadeIn(nextScene);
        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeOut);
        await SceneLoadExtension.FadeOut();
    }

    public void SetStarLevel()
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