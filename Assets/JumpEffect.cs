using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private ParticleSystem particle2;

    void Update()
    {
        //�X�y�[�X�L�[��������particle���Đ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            particle.Play();
            particle2.Play();
        }

    }
}
