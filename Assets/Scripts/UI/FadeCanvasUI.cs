using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


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

    private AnimatorStateInfo m_Info => m_FadeAnim.GetCurrentAnimatorStateInfo(0);

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

    public void StartFadeIn()
    {
        gameObject.SetActive(true);
        m_FadeAnim.Play(FadeIn);
    }
    public void StartWait()
    {
        m_FadeAnim.Play(FadeWait);
    }
    public void StartFadeOut()
    {
        m_FadeAnim.Play(FadeOut);
    }
    public void FinishFadeOut()
    {
        gameObject.SetActive(false);
    }


    public async Task IsCompleteFadeIn() => await TaskExtension.WaitUntiil(() => m_Info.IsName(FadeIn) &&
                                                                                 m_Info.normalizedTime >= MaxNormalizedTime);
    public async Task WaitToFadeOut() => await Task.Delay(m_WaitMilliTime);

    public async Task IsCompleteFadeOut() => await TaskExtension.WaitUntiil(() => m_Info.IsName(FadeOut) &&
                                                                                  m_Info.normalizedTime >= MaxNormalizedTime);
}

public static class TaskExtension
{
    public const int OneSec = 1000;
    private const int FrameRate = 60;

    public static int FPS_60 => OneSec / FrameRate;
    public static async Task WaitUntiil(Func<bool> isCompleted)
    {
        while (!isCompleted())
        {
            await Task.Delay(FPS_60);
        }
    }

    /// <summary>
    /// 投げっぱなしにする場合は、これを呼ぶことでコンパイラの警告の抑制と、例外発生時のロギングを行います。
    /// </summary>
    public static void FireAndForget(this Task task)
    {
        task.ContinueWith(x =>
        {
            Debug.LogError("Task Errored");
        }, TaskContinuationOptions.OnlyOnFaulted);
    }
}