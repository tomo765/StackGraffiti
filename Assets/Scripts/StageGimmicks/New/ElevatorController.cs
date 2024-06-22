using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ElevatorController : GimmickReceiver
{
    [SerializeField] private Vector2 m_MoveVec;
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private bool m_OnActivate = false;
    [SerializeField] private bool m_Reversible = true;

    private const int MinTime = 0;
    private const int MaxTime = 1;

    private Vector2 m_StartPos;
    private Vector2 m_EndPos;
    private float m_ArrivalTime;      // 0�`1�ŕϓ� �����ʒu = 0, �ړ��� = 1�@�Ƃ��Ĉړ�������
    private bool m_OnReverse = false;  //true : ���H, false : ���H

    public bool OnActivate => m_OnActivate;

    private float GetAddTime() => Time.fixedDeltaTime * (m_OnReverse && m_Reversible ? -1 : 1) * m_MoveSpeed;

    private void Start()
    {
        m_StartPos = transform.position;
        m_EndPos = m_StartPos + m_MoveVec;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!m_OnActivate) { return; }

        m_ArrivalTime += GetAddTime();
        transform.position = Vector2.Lerp(m_StartPos, m_EndPos, m_ArrivalTime);

        if (m_ArrivalTime >= MaxTime || m_ArrivalTime <= MinTime)
        {
            m_OnReverse = !m_OnReverse;
            m_ArrivalTime = Mathf.Clamp(m_ArrivalTime, MinTime, MaxTime);
        }
    }

    public override void ChangeActivate() => m_OnActivate = !m_OnActivate;
}

#if UNITY_EDITOR
public partial class ElevatorController : GimmickReceiver
{
    // �ړ��͈͕\��
    private void OnDrawGizmosSelected()
    {
        Vector2 f;  //�ړ��O�ʒu
        Vector2 t;  //�ړ���ʒu
        if (UnityEditor.EditorApplication.isPlaying)
        {
            f = m_StartPos;
            t = m_EndPos;
        }
        else
        {
            f = transform.position;
            t = f + m_MoveVec;
        }


        // �ړ��͈͂���ŕ\��
        Gizmos.DrawLine(f, t);

        // �[��Box
        Gizmos.DrawWireCube(f, Vector2.one * 0.2f);
        Gizmos.DrawWireCube(t, Vector2.one * 0.2f);
    }
}
#endif