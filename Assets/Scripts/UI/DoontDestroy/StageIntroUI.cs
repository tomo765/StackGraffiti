using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using System.Threading;

/// <summary>
/// ステージ開始時の演出
/// </summary>
public class StageIntroUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_StageText;
    [SerializeField] private TextMeshProUGUI m_TitleTextEN;
    [SerializeField] private TextMeshProUGUI m_TitleTextJP;
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
    public void SetIntroText(string stageTxt, string TitleTextEN, string TitleTextJP)
    {
        m_StageText.text = stageTxt;
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
        await Task.Delay(300);
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
        await TaskExtension.WaitUntiil(() => StartShowStageText);
        while (m_StageText.color.a < 0.98f)
        {
            var newCol = m_StageText.color;
            newCol.a = Mathf.Lerp(newCol.a, 1, 0.25f);
            m_StageText.color = newCol;

            await Task.Delay(TaskExtension.FPS_60);
            m_Source.Token.ThrowIfCancellationRequested();
        }
    }
    private async Task ShowTitleText()
    {
        await TaskExtension.WaitUntiil(() => StartShowTitleText);
        while (m_TitleTextEN.color.a < 0.98f)
        {
            var newCol = m_TitleTextEN.color;
            newCol.a = Mathf.Lerp(newCol.a, 1, 0.25f);
            m_TitleTextEN.color = newCol;
            m_TitleTextJP.color = newCol;

            await Task.Delay(TaskExtension.FPS_60);
            m_Source.Token.ThrowIfCancellationRequested();
        }
    }


    private async Task FadeIn()
    {
        while (m_IntroImage.color.a < 0.98f)
        {
            var newCol = m_IntroImage.color;
            newCol.a = Mathf.Lerp(newCol.a, 1, 0.1f);
            m_IntroImage.color = newCol;

            await Task.Delay(TaskExtension.FPS_60);
            m_Source.Token.ThrowIfCancellationRequested();
        }
    }
    private async Task FadeOut()
    {
        Color newCol;
        while (m_IntroImage.color.a > 0.005f)
        {
            newCol = m_IntroImage.color;
            newCol.a = Mathf.Lerp(newCol.a, 0, 0.1f);
            m_IntroImage.color = newCol;

            newCol = m_BGImage.color;
            newCol.a = Mathf.Lerp(newCol.a, 0, 0.1f);
            m_BGImage.color = newCol;

            newCol = m_StageText.color;
            newCol.a = Mathf.Lerp(newCol.a, 0, 0.1f);
            m_StageText.color = newCol;
            m_TitleTextEN.color = newCol;
            m_TitleTextJP.color = newCol;

            await Task.Delay(TaskExtension.FPS_60);
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
