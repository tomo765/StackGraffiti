using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FadeCanvasUI : MonoBehaviour
{
    public static FadeCanvasUI Instance;

    [SerializeField] Animator m_FadeAnim;

    private bool m_OnFade = false;

    public bool OnFade => m_OnFade;
    private AnimatorStateInfo m_Info => m_FadeAnim.GetCurrentAnimatorStateInfo(0);

    private void Awake()
    {
        if(Instance != null) 
        { 
            Destroy(gameObject);
            return; 
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartFade()
    {
        m_OnFade = true;
        gameObject.SetActive(true);
        m_FadeAnim.Play("Fin");
    }
    public void FinishFade()
    {
        m_OnFade = false;
        gameObject.SetActive(false);
    }


    public async Task IsCompleteFadeIn()
    {
        await WaitUntiil(() => m_Info.IsName("Fout"));
    }

    public async Task IsCompleteFadeOut()
    {
        await WaitUntiil(() =>
        m_Info.IsName("Fout") && m_Info.normalizedTime >= 1f);
    }

    public static async Task WaitUntiil(Func<bool> isCompleted)
    {
        while (!isCompleted())
        {
            await Task.Delay((int)(Time.fixedDeltaTime * 1000));
        }
    }
}

public static class FadeExtension
{
    public static async void LoadScene(AudioClip playClp, string load)
    {

        if(FadeCanvasUI.Instance == null) 
        { 
            MonoBehaviour.Instantiate(GeneralSettings.Instance.Prehab.FadeCanvasUI);
            await FadeCanvasUI.WaitUntiil(() => FadeCanvasUI.Instance != null);
        }

        if (FadeCanvasUI.Instance.OnFade) { return; }
        SoundManager.Instance?.PlayNewSE(playClp);
        
        FadeCanvasUI.Instance.StartFade();

        await FadeCanvasUI.Instance.IsCompleteFadeIn();
        SceneManager.LoadScene(load);

        await FadeCanvasUI.Instance.IsCompleteFadeOut();
        FadeCanvasUI.Instance.FinishFade();
    }
}