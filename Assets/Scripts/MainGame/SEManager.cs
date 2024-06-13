using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public AudioClip seAudioClip;
    public AudioSource seAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        seAudioSource = FindObjectOfType<AudioSource>();
    }

    private void OnCollitionEnter2D(Collider2D col)
    {
        // ‰¹–Â‚ç‚·
        if (col.gameObject.tag == "Player")
        {
            seAudioSource.PlayOneShot(seAudioClip);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // ‰¹–Â‚ç‚·
        if (col.gameObject.tag == "Player")
        {
            seAudioSource.PlayOneShot(seAudioClip);
        }
    }
}
