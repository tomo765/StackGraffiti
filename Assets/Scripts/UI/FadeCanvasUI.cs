using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary> シーン切り替え時のフェードをするためのクラス </summary>
/// <remarks> 処理の実行は<see cref="SceneLoadExtension"/>からのみする </remarks>
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

    /// <summary> 現在のアニメーションの状態を取得する </summary>
    /// <remarks> フェードインの終了とフェードアウトの終了を取得している </remarks>
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

    /// <summary> フェードイン開始 </summary>
    public void StartFadeIn()
    {
        gameObject.SetActive(true);
        m_FadeAnim.Play(FadeIn);
    }
    /// <summary> フェードイン終了 </summary>
    public void FinishFadeIn()
    {
        m_FadeAnim.Play(FadeWait);
    }
    /// <summary> フェードアウト開始 </summary>
    public void StartFadeOut()
    {
        m_FadeAnim.Play(FadeOut);
    }
    /// <summary> フェードアウト終了 </summary>
    public void FinishFadeOut()
    {
        gameObject.SetActive(false);
    }

    /// <summary> フェードインのアニメーションが終了するまで待機 </summary>
    public async Task IsCompleteFadeIn() => await TaskExtension.WaitUntiil(() => StateInfo.IsName(FadeIn) &&
                                                                                 StateInfo.normalizedTime >= MaxNormalizedTime);
    /// <summary> フェードアウトのアニメーションを開始する前に指定の秒数待機 </summary>
    public async Task WaitToFadeOut() => await Task.Delay(m_WaitMilliTime);

    /// <summary> フェードアウトのアニメーションが終了するまで待機 </summary>
    public async Task IsCompleteFadeOut() => await TaskExtension.WaitUntiil(() => StateInfo.IsName(FadeOut) &&
                                                                                  StateInfo.normalizedTime >= MaxNormalizedTime);
}

public static class TaskExtension
{
    /// <summary> 1秒 </summary>
    public const int OneSec = 1000;

    /// <summary> 60FPS </summary>
    public static int SixtyFrame => OneSec / 60;

    /// <summary> 任意のタイミングまで処理を中断する </summary>
    /// <param name="isCompleted">処理の中断を終了するための条件</param>
    public static async Task WaitUntiil(Func<bool> isCompleted)
    {
        while (!isCompleted())
        {
            await Task.Delay(SixtyFrame);
        }
    }

    /// <summary>
    /// 投げっぱなしにする場合は、これを呼ぶことでコンパイラの警告の抑制と、例外発生時のロギングを行います。
    /// 参考元 : https://neue.cc/2013/10/10_429.html
    /// </summary>
    public static void FireAndForget(this Task task)
    {
        task.ContinueWith(x =>
        {
            Debug.LogException(new TaskCanceledException());
        }, TaskContinuationOptions.OnlyOnFaulted);
    }
}