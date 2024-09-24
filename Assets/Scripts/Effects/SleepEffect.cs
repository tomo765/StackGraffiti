using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepEffect : MonoBehaviour, IContainableEffect
{
    [SerializeField] float m_AnimSpeed = 1;

    private void Start()
    {
        var m_Anim = GetComponent<Animator>();
        m_Anim.speed = m_AnimSpeed;
    }

    public bool IsActive => gameObject.activeSelf;

    public IContainableEffect Create(Vector3 vec, Quaternion q, Transform parent)
    {
        return Instantiate(this, vec, q, parent);
    }

    public void Play(Vector2 vec)
    {
        gameObject.SetActive(true);
    }

    public void StopEffect()
    {
        gameObject.SetActive(false);
    }
}
