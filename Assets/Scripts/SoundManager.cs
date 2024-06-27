using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = Instantiate(GeneralSettings.Instance.Prehab.SoundManager);
            }
            return instance;
        }
    }

    [SerializeField] private AudioSource m_BGM;
    [SerializeField] private AudioSource m_SE;

    public float BGMVol => m_BGM.volume;
    public float SEVol => m_SE.volume;

    public void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayNewBGM(AudioClip newClip)
    {
        if (m_SE == null) { return; }
        m_BGM.clip = newClip;
        m_BGM.Play();
    }
    public void SetBGMVol(float vol)
    {
        m_BGM.volume = vol;
    }


    public void PlayNewSE(AudioClip newClip)
    {
        if(m_SE == null) { return; }
        m_SE.clip = newClip;
        m_SE.PlayOneShot(newClip);
    }
    public void SetSEVol(float vol)
    {
        m_SE.volume = vol;
    }
}
