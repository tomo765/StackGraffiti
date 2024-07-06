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

    private bool m_OnFade = false;

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

    public void StartFade()
    {
        m_OnFade = true;
        gameObject.SetActive(true);
    }
    public void FinishFade()
    {
        m_OnFade = false;
        gameObject.SetActive(false);
    }


    public async Task IsCompleteFadeIn() => await TaskExtension.WaitUntiil(() => m_Info.IsName("Fout"));  //ToDo : マジックナンバー

    public async Task IsCompleteFadeOut() => await TaskExtension.WaitUntiil(() => m_Info.IsName("Fout") &&
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