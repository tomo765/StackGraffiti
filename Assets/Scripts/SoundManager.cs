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
    [SerializeField] private AudioSource m_MarimbaBGM;
    [SerializeField] private AudioSource m_SE;
    [SerializeField] private float m_BassScale = 0.5f;  //ToDo : MaxVolume
    [SerializeField] private float m_CodeScale = 0.7f;

    private float m_SubBGMVolume = 0;

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

    public void PlayMainBGM()
    {
        if(m_MainBGM.isPlaying) { return; }
        m_MainBGM.Play();
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

    public void PlayMarimba(bool isPlay)
    {
        if (isPlay)
        {
            m_MarimbaBGM.volume = Mathf.Lerp(m_MarimbaBGM.volume, 1, 0.05f);  //ToDo : マジックナンバー
        }

        if (!isPlay) 
        {
            m_MarimbaBGM.volume = Mathf.Lerp(m_MarimbaBGM.volume, 0, 0.05f);
            if (m_MarimbaBGM.volume == 0)
            {
                m_MarimbaBGM.Stop();
            }
            return; 
        }

        if(m_MarimbaBGM.isPlaying) { return; }

        m_MarimbaBGM.Play();
        m_MarimbaBGM.time = m_MainBGM.time;
    }

    public void PlayMarimba(float val)
    {
        m_MarimbaBGM.volume = Mathf.Clamp(Mathf.Abs(val), 0, 1);
        if (m_MarimbaBGM.isPlaying) { return; }

        m_MarimbaBGM.Play();
        m_MarimbaBGM.time = m_MainBGM.time;
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