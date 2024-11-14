using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using System.Threading;

/// <summary> ステージ開始時の演出 </summary>
public class StageIntroUI : MonoBehaviour
{
    private const float TextLerpValue = 0.25f;
    private const float FadeLerpValue = 0.1f;

    [SerializeField] private TextMeshProUGUI m_StageText;
    [SerializeField] private TextMeshProUGUI m_TitleTextEN;
    [SerializeField] private TextMeshProUGUI m_TitleTextJP;
    [SerializeField] private TextMeshProUGUI m_StartText;
    [SerializeField] private float m_PlaySpeed;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private Image m_IntroImage;

    private Image m_BGImage;
    private CancellationTokenSource m_Source = new CancellationTokenSource();
    private bool m_FinishAnimation => StateInfo.normalizedTime >= 1;
    private bool m_FinishFadeOut = false;

    public bool FinishFadeOut => m_FinishFadeOut;
    private AnimatorStateInfo StateInfo => m_Animator.GetCurrentAnimatorStateInfo(0);
    private float AnimationTime => StateInfo.length * StateInfo.normalizedTime;
    private bool StartShowStageText => AnimationTime >= 0.166f * m_PlaySpeed;
    private bool StartShowTitleText => AnimationTime >= 0.917f * m_PlaySpeed;

    void Start()
    {
        Init();
        PlayIntro().FireAndForget();
    }

    public async Task PlayIntro()
    {
        m_FinishFadeOut = false;

        await TaskExtension.WaitUntiil(() => m_BGImage != null);
        m_BGImage.color = Color.black * 0.24f;

        if (FadeCanvasUI.Instance != null)
        {
            await TaskExtension.WaitUntiil(() => !SceneLoadExtension.IsFading);
        }

        ShowIntro().FireAndForget();
        ShowTexts().FireAndForget();
    }

    public void SetIntroText(int stageLevel, string TitleTextEN, string TitleTextJP)
    {
        m_StageText.text = m_StageText.GetComponent<TMPTextLocalizer>().GetLocalizeText() + stageLevel.ToString();
        m_TitleTextEN.text = TitleTextEN;
        m_TitleTextJP.text = TitleTextJP;
    }

    private void Init()
    {
        m_BGImage = GetComponent<Image>();

        m_IntroImage.color = ColorExtension.WhiteClearness;
        m_StageText.color = ColorExtension.BlackClearness;
        m_TitleTextEN.color = ColorExtension.BlackClearness;
        m_TitleTextJP.color = ColorExtension.BlackClearness;
        m_Animator.speed = m_PlaySpeed;
    }

    private async Task ShowIntro()
    {
        await FadeIn();
        await TaskExtension.WaitUntiil(() => m_FinishAnimation);
        await ShowStartText();
        await FadeOut();

        gameObject.SetActive(false);
    }
    private async Task ShowTexts()
    {
        await ShowStageText();
        await ShowTitleText();
    }
    private async Task ShowStageText()
    {
        float finishFade = 0.98f;
        await TaskExtension.WaitUntiil(() => StartShowStageText);
        while (m_StageText.color.a < finishFade)
        {
            var newCol = m_StageText.color;
            newCol.a = Mathf.Lerp(newCol.a, 1, TextLerpValue);
            m_StageText.color = newCol;

            await Task.Delay(TaskExtension.SixtyFrame);
            m_Source.Token.ThrowIfCancellationRequested();
        }
    }
    private async Task ShowTitleText()
    {
        float finishFade = 0.98f;
        await TaskExtension.WaitUntiil(() => StartShowTitleText);
        while (m_TitleTextEN.color.a < finishFade)
        {
            var newCol = m_TitleTextEN.color;
            newCol.a = Mathf.Lerp(newCol.a, 1, TextLerpValue);
            m_TitleTextEN.color = newCol;
            m_TitleTextJP.color = newCol;

            await Task.Delay(TaskExtension.SixtyFrame);
            m_Source.Token.ThrowIfCancellationRequested();
        }
    }

    private async Task ShowStartText()
    {
        int displayTime = 0;
        int fadeCycleTime = TaskExtension.OneSec / 2 * 3;
        while (!InputExtension.MouseLeftPush)
        {
            displayTime += TaskExtension.SixtyFrame;
            m_StartText.gameObject.SetActive(displayTime <= TaskExtension.OneSec);
            if (displayTime >= fadeCycleTime)
            {
                displayTime -= fadeCycleTime;
            }

            await Task.Delay(TaskExtension.SixtyFrame);
        }
        m_StartText.gameObject.SetActive(false);
    }


    private async Task FadeIn()
    {
        float finishFade = 0.98f;
        while (m_IntroImage.color.a < finishFade)
        {
            var newCol = m_IntroImage.color;
            newCol.a = Mathf.Lerp(newCol.a, 1, FadeLerpValue);
            m_IntroImage.color = newCol;

            await Task.Delay(TaskExtension.SixtyFrame);
            m_Source.Token.ThrowIfCancellationRequested();
        }
    }
    private async Task FadeOut()
    {
        float finishFade = 0.005f;
        Color newCol;
        while (m_IntroImage.color.a > finishFade)
        {
            newCol = m_IntroImage.color;
            newCol.a = Mathf.Lerp(newCol.a, 0, FadeLerpValue);
            m_IntroImage.color = newCol;

            newCol = m_BGImage.color;
            newCol.a = Mathf.Lerp(newCol.a, 0, FadeLerpValue);
            m_BGImage.color = newCol;

            newCol = m_StageText.color;
            newCol.a = Mathf.Lerp(newCol.a, 0, FadeLerpValue);
            m_StageText.color = newCol;
            m_TitleTextEN.color = newCol;
            m_TitleTextJP.color = newCol;

            await Task.Delay(TaskExtension.SixtyFrame);
            m_Source.Token.ThrowIfCancellationRequested();
        }
        m_FinishFadeOut = true;
    }

    private void OnDestroy()
    {
        m_Source.Cancel();
    }
}

public static class ColorExtension
{
    public static Color WhiteClearness => new Color(1, 1, 1, 0);
    public static Color BlackClearness => new Color(0, 0, 0, 0);
}
