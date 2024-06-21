using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fades : MonoBehaviour
{
    [SerializeField] Animator m_FadeAnim;
    private bool m_Loaded = false;

    void Start()
    {
        
    }


    void Update()
    {
        Fade();
    }

    private void Fade()  // ToDO : asyncŽg‚¤
    {
        if (m_Loaded) { return; }

        var info = m_FadeAnim.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1)
        {
            m_Loaded = true;
            LoadScene();
        }
    }

    private void LoadScene()
    {
        
        SceneManager.LoadScene("");
    }
}
