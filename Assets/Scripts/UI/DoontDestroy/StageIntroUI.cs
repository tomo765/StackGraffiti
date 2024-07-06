using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using System.Threading;

public class StageIntroUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_StageText;
    [SerializeField] private TextMeshProUGUI m_TitleText;
    [SerializeField] private float m_PlaySpeed;
    [SerializeField] private Animator m_Animator;

    private CancellationTokenSource m_Source = new CancellationTokenSource();
    private Image m_Image;
    private bool m_FinishAnimation => StateInfo.normalizedTime >= 1;
    private bool m_FinishFadeOut = false;
    
    public bool FinishFadeOut => m_FinishFadeOut;
    private AnimatorStateInfo StateInfo => m_Animator.GetCurrentAnimatorStateInfo(0);
    private float AnimationTime => StateInfo.length * StateInfo.normalizedTime;
    private bool StartShowStageText => AnimationTime >= 0.166f * m_PlaySpeed;  //finish 0.29f
    private bool StartShowTitleText => AnimationTime >= 0.917f * m_PlaySpeed;  //finish 1.41f

    void Start()
    {
        Init();
        PlayIntro();
    }

    public async void PlayIntro()
    {
        if (FadeCanvasUI.Instance != null)
        {
            await TaskExtension.WaitUntiil(() => !FadeCanvasUI.Instance.OnFade());
        }

        ShowIntro();
        ShowTexts();
    }

    private void Init()
    {
        m_Image = GetComponent<Image>();

        m_Image.color = ColorExtension.WhiteClearness;
        m_StageText.color = ColorExtension.BlackClearness;
        m_TitleText.color = ColorExtension.BlackClearness;
        m_Animator.speed = m_PlaySpeed;
    }

    private async void ShowIntro()
    {
        await FadeIn();
        await TaskExtension.WaitUntiil(() => m_FinishAnimation);
        await Task.Delay(300);
        await FadeOut();

        gameObject.SetActive(false);
    }
    private async void ShowTexts()
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

            await Task.Delay(16);
            m_Source.Token.ThrowIfCancellationRequested();
        }
    }
    private async Task ShowTitleText()
    {
        await TaskExtension.WaitUntiil(() => StartShowTitleText);
        while (m_TitleText.color.a < 0.98f)
        {
            var newCol = m_TitleText.color;
            newCol.a = Mathf.Lerp(newCol.a, 1, 0.25f);
            m_TitleText.color = newCol;

            await Task.Delay(16);
            m_Source.Token.ThrowIfCancellationRequested();
        }
    }


    private async Task FadeIn()
    {
        while (m_Image.color.a < 0.98f)
        {
            var newCol = m_Image.color;
            newCol.a = Mathf.Lerp(newCol.a, 1, 0.1f);
            m_Image.color = newCol;

            await Task.Delay(16);
            m_Source.Token.ThrowIfCancellationRequested();
        }
    }
    private async Task FadeOut()
    {
        Color newCol;
        while (m_Image.color.a > 0.005f)
        {
            newCol = m_Image.color;
            newCol.a = Mathf.Lerp(newCol.a, 0, 0.1f);
            m_Image.color = newCol;

            newCol = m_StageText.color;
            newCol.a = Mathf.Lerp(newCol.a, 0, 0.1f);
            m_StageText.color = newCol;
            m_TitleText.color = newCol;

            await Task.Delay(16);
            m_Source.Token.ThrowIfCancellationRequested();
        }
        m_FinishFadeOut = true;
    }

    private void ShowTime()
    {
        Debug.Log(AnimationTime);
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
