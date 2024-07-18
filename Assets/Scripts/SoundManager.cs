using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public const int PlayBassScore = 10;
    public const int PlayCodeScore = 15;
    public const int PlayMaxVolScore = 20;
    public const float VolumeOnMax = 0.8f;
    public const float VolumeOnCode = 0.5f;
    public const float VolumeOnBass = 0.3f;


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
    public void PlayMarimba(float val)
    {
        m_MarimbaBGM.volume = Mathf.Clamp(Mathf.Abs(val), 0, 1);
        if (m_MarimbaBGM.isPlaying) { return; }

        m_MarimbaBGM.Play();
        m_MarimbaBGM.time = m_MainBGM.time;
    }
    public void StopAllBGM()
    {
        m_MainBGM.Stop();
        m_BassBGM.Stop();
        m_CodeBGM.Stop();
        m_MarimbaBGM.Stop();
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

    public void SetSubBGMVolume(float starLevel)  //ToDo : 
    {
        m_SubBGMVolume = starLevel >= PlayMaxVolScore ? 0.8f :
                         starLevel >= PlayCodeScore ? 0.5f :
                         starLevel >= PlayBassScore ? 0.3f : 0;
        SetBGMVol(m_MainBGM.volume);
    }
}