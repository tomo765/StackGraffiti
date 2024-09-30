using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary> �V�[���؂�ւ����̃t�F�[�h�����邽�߂̃N���X </summary>
/// <remarks> �����̎��s��<see cref="SceneLoadExtension"/>����݂̂��� </remarks>
public class FadeCanvasUI : MonoBehaviour
{
    private static FadeCanvasUI instance;
    public static FadeCanvasUI Instance => instance;

    [SerializeField] Animator m_FadeAnim;
    [SerializeField] private int m_WaitMilliTime = 300;


    private const string FadeIn = "Fin";
    private const string FadeWait = "Fwait";
    private const string FadeOut = "Fout";
    private const float MaxNormalizedTime = 1f;

    /// <summary> ���݂̃A�j���[�V�����̏�Ԃ��擾���� </summary>
    /// <remarks> �t�F�[�h�C���̏I���ƃt�F�[�h�A�E�g�̏I�����擾���Ă��� </remarks>
    private AnimatorStateInfo StateInfo => m_FadeAnim.GetCurrentAnimatorStateInfo(0);

    private void Awake()
    {
        if(instance != null) 
        { 
            Destroy(gameObject);
            return; 
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary> �t�F�[�h�C���J�n </summary>
    public void StartFadeIn()
    {
        gameObject.SetActive(true);
        m_FadeAnim.Play(FadeIn);
    }
    /// <summary> �t�F�[�h�C���I�� </summary>
    public void FinishFadeIn()
    {
        m_FadeAnim.Play(FadeWait);
    }
    /// <summary> �t�F�[�h�A�E�g�J�n </summary>
    public void StartFadeOut()
    {
        m_FadeAnim.Play(FadeOut);
    }
    /// <summary> �t�F�[�h�A�E�g�I�� </summary>
    public void FinishFadeOut()
    {
        gameObject.SetActive(false);
    }

    /// <summary> �t�F�[�h�C���̃A�j���[�V�������I������܂őҋ@ </summary>
    public async Task IsCompleteFadeIn() => await TaskExtension.WaitUntiil(() => StateInfo.IsName(FadeIn) &&
                                                                                 StateInfo.normalizedTime >= MaxNormalizedTime);
    /// <summary> �t�F�[�h�A�E�g�̃A�j���[�V�������J�n����O�Ɏw��̕b���ҋ@ </summary>
    public async Task WaitToFadeOut() => await Task.Delay(m_WaitMilliTime);

    /// <summary> �t�F�[�h�A�E�g�̃A�j���[�V�������I������܂őҋ@ </summary>
    public async Task IsCompleteFadeOut() => await TaskExtension.WaitUntiil(() => StateInfo.IsName(FadeOut) &&
                                                                                  StateInfo.normalizedTime >= MaxNormalizedTime);
}

public static class TaskExtension
{
    /// <summary> 1�b </summary>
    public const int OneSec = 1000;

    /// <summary> 60FPS </summary>
    public static int SixtyFrame => OneSec / 60;

    /// <summary> �C�ӂ̃^�C�~���O�܂ŏ����𒆒f���� </summary>
    /// <param name="isCompleted">�����̒��f���I�����邽�߂̏���</param>
    public static async Task WaitUntiil(Func<bool> isCompleted)
    {
        while (!isCompleted())
        {
            await Task.Delay(SixtyFrame);
        }
    }

    /// <summary>
    /// �������ςȂ��ɂ���ꍇ�́A������ĂԂ��ƂŃR���p�C���̌x���̗}���ƁA��O�������̃��M���O���s���܂��B
    /// �Q�l�� : https://neue.cc/2013/10/10_429.html
    /// </summary>
    public static void FireAndForget(this Task task)
    {
        task.ContinueWith(x =>
        {
            Debug.LogException(new TaskCanceledException());
        }, TaskContinuationOptions.OnlyOnFaulted);
    }
}