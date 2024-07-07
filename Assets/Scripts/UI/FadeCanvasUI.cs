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

    private bool m_OnFade = false;

    private const string FadeIn = "Fin";
    private const string FadeWait = "Fwait";
    private const string FadeOut = "Fout";


    public bool OnFade() => m_OnFade;
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
        m_OnFade = true;
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
        m_OnFade = false;
        gameObject.SetActive(false);
    }


    public async Task IsCompleteFadeIn() => await TaskExtension.WaitUntiil(() => m_Info.IsName(FadeIn) &&
                                                                                 m_Info.normalizedTime >= 1f);
    public async Task WaitToFadeOut() => await Task.Delay(m_WaitMilliTime);

    public async Task IsCompleteFadeOut() => await TaskExtension.WaitUntiil(() => m_Info.IsName(FadeOut) &&
                                                                                  m_Info.normalizedTime >= 1f);
}

public static class TaskExtension
{
    public static async Task WaitUntiil(Func<bool> isCompleted)
    {
        while (!isCompleted())
        {
            await Task.Delay((int)(Time.fixedDeltaTime * 1000));
        }
    }
}