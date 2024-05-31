using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    [SerializeField] private AudioSource BGM;
    [SerializeField] private AudioSource SE;


    public AudioSource m_BGM => BGM;
    public AudioSource m_SE => SE;
    public float m_BgmVol => BGM.volume;
    public float m_SeVol => SE.volume;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetBGMVol(float vol)
    {
        if(BGM == null) 
        { 
            Debug.LogError("‚È‚¢BGM");
            return;
        }
        BGM.volume = vol;
    }

    public void SetSEVol(float vol)
    {
        if (SE == null)
        {
            Debug.LogError("‚È‚¢SE");
            return;
        }
        SE.volume = vol;
    }
}
