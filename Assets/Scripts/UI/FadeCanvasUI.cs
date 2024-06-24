using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


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


    public async Task IsCompleteFadeIn() => await WaitUntiil(() => m_Info.IsName("Fout"));  //ToDo : マジックナンバー

    public async Task IsCompleteFadeOut() => await WaitUntiil(() => m_Info.IsName("Fout") &&
                                                                    m_Info.normalizedTime >= 1f);

    public static async Task WaitUntiil(Func<bool> isCompleted)
    {
        while (!isCompleted())
        {
            await Task.Delay((int)(Time.fixedDeltaTime * 1000));
        }
    }
}