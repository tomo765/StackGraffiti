using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance => instance;

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
        m_BGM.clip = newClip;
        m_BGM.Play();
    }
    public void SetBGMVol(float vol)
    {
        m_BGM.volume = vol;
    }


    public void PlayNewSE(AudioClip newClip)
    {
        m_SE.clip = newClip;
        m_SE.Play();
    }
    public void SetSEVol(float vol)
    {
        m_SE.volume = vol;
    }
}
