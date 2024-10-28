using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCharaEffect : MonoBehaviour, IContainableEffect
{
    [SerializeField] private float m_AnimSpeed = 1;
    private Animator m_Anim;
    public bool IsActive => gameObject.activeSelf;


    void Start()
    {
        m_Anim = GetComponent<Animator>();
        m_Anim.speed = m_AnimSpeed;
    }

    void Update()
    {
        gameObject.SetActive(m_Anim.IsPlaying());
    }


    public IContainableEffect Create(Vector3 vec, Quaternion q, Transform parent)
    {
        return Instantiate(GeneralSettings.Instance.Prehab.DeleteCharaEffect, vec, q, parent);
    }

    public void Play(Vector2 vec)
    {
        gameObject.SetActive(true);
        transform.position = vec;
    }

    public void StopEffect()
    {
        gameObject.SetActive(false);
    }
}
