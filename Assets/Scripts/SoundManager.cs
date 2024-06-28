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

    [SerializeField] private AudioSource m_MainBGM;
    [SerializeField] private AudioSource m_BassBGM;
    [SerializeField] private AudioSource m_CodeBGM;
    [SerializeField] private AudioSource m_SE
        ;
    [SerializeField] private float m_BassScale = 0.5f;
    [SerializeField] private float m_CodeScale = 0.7f;

    [SerializeField] private float m_SubBGMVolume = 0;

    public float BGMVol => m_MainBGM.volume;
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

    public void SetBGMVol(float vol)
    {
        m_MainBGM.volume = vol;
        m_BassBGM.volume = vol * m_BassScale * m_SubBGMVolume;
        m_CodeBGM.volume = vol * m_CodeScale * m_SubBGMVolume;
    }

    public void PlayBass(bool isPlay)
    {
        if(m_BassBGM.isPlaying && isPlay) { return; }

        m_BassBGM.Play();
        m_BassBGM.volume = m_MainBGM.volume * m_BassScale * m_SubBGMVolume;
        m_BassBGM.time = m_MainBGM.time;
    }
    public void PlayCode(bool isPlay)
    {
        if (m_CodeBGM.isPlaying && isPlay) { return; }

        m_CodeBGM.Play();
        m_CodeBGM.volume = m_MainBGM.volume * m_CodeScale * m_SubBGMVolume;
        m_CodeBGM.time = m_MainBGM.time;
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

    public void SetSubBGMVolume(float vol)
    {
        m_SubBGMVolume = vol;
        SetBGMVol(m_MainBGM.volume);
    }
}