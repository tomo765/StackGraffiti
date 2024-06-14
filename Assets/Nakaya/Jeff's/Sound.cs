using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string name;
    [HideInInspector]public AudioSource source;
    public AudioClip clip;
    public float volume;
    public float pitch;
    public bool loop;
}
