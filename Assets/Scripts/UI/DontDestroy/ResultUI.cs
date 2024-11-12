using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary> �X�e�[�W�N���A��̃��U���g��� </summary>
public class ResultUI : MonoBehaviour
{
    [SerializeField] private Image[] m_Stars;


    [SerializeField] private Button m_StageSelectBtn;
    [SerializeField] private Button m_ReturnTitleBtn;
    [SerializeField] private Button m_NextStageButton;
    [SerializeField] private Button m_CreditButton;

    public void Init()  //ToDo : �N���W�b�g�𗬂�����ɃZ���N�g��ʂ������Ă��Ă������Ƃ�ɖ߂�
    {
        //�Z���N�g��ʂɖ߂�{�^���̏����o�^
        m_StageSelectBtn.onClick.RemoveAllListeners();
        m_StageSelectBtn.onClick.AddListener(() =>
        {
            if (SceneLoadExtension.IsFading) { return; }

            OnPushResultButton(Config.SceneNames.StageSelect).FireAndForget();
            GameManager.PlayBGMs();
        });

        //�^�C�g���ɖ߂�{�^���̏����o�^
        m_ReturnTitleBtn.onClick.RemoveAllListeners();
        m_ReturnTitleBtn.onClick.AddListener(() =>
        {
            if (SceneLoadExtension.IsFading) { return; }

            OnPushResultButton(Config.SceneNames.Title).FireAndForget();
            GameManager.PlayBGMs();
        });

        //���̃X�e�[�W�ɐi�ރ{�^���̏����o�^
        m_NextStageButton.gameObject.SetActive(!GameManager.IsLastStage);
        if (!GameManager.IsLastStage)
        {
            m_NextStageButton.onClick.RemoveAllListeners();
            m_NextStageButton.onClick.AddListener(async() =>
            {
                StageLevel nextStage = GameManager.CullentStage + 1;

                if (SceneLoadExtension.IsFading) { return; }

                GameManager.InitPlayState(nextStage);
                await OnPushResultButton(Config.SceneNames.m_StageNames[(int)nextStage - 1]);
                GameManager.PlayBGMs();

                DontDestroyCanvas.Instance.ChangeStageIntroUIVisible(true);  //ToDo : StageSelectUI �ł������������Ă邩�烁�\�b�h�ɂ���H
                DontDestroyCanvas.Instance.StageIntroUI.SetIntroText((int)GameManager.CullentStage,
                                                                     GeneralSettings.Instance.StageInfos.GetStageTextEN((int)GameManager.CullentStage),
                                                                     GeneralSettings.Instance.StageInfos.GetStageTextJP((int)GameManager.CullentStage)
                                                                    );
            });
        }

        m_CreditButton.gameObject.SetActive(StageDataUtility.IsAllStageClear());
        if (StageDataUtility.IsAllStageClear())
        {
            m_CreditButton.onClick.RemoveAllListeners();
            m_CreditButton.onClick.AddListener(() =>
            {
                OnPushResultButton(Config.SceneNames.Credit).FireAndForget();
            });
        }
    }

    /// <summary> ���U���g��ʂ̃{�^�����������Ƃ��̏��� </summary>
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