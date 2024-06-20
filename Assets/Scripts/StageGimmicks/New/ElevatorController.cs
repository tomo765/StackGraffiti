using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ElevatorController : GimmickReceiver
{
    [SerializeField] private Vector2 m_MoveVec;
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private bool m_OnActivate = false;
    [SerializeField] private bool m_CanReverse = true;

    private Vector2 m_StartPos;
    private Vector2 m_EndPos;
    private float m_ArrivalTime;      // 0～1で変動 初期位置 = 0, 移動後 = 1　として移動させる
    private bool m_IsReturn = false;  //true : 復路, false : 往路

    public bool OnActivate => m_OnActivate;

    private float GetAddTime() => Time.fixedDeltaTime * (m_IsReturn && m_CanReverse ? -1 : 1) * m_MoveSpeed;

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

        if (m_ArrivalTime >= 1f || m_ArrivalTime <= 0f)
        {
            m_IsReturn = !m_IsReturn;
            m_ArrivalTime = Mathf.Clamp(m_ArrivalTime, 0, 1);
        }
    }

    public override void ChangeActivate() => m_OnActivate = !m_OnActivate;
    public override void SetActivate(bool b) => m_OnActivate = b;
}

#if UNITY_EDITOR
public partial class ElevatorController : GimmickReceiver
{
    // 移動範囲表示
    private void OnDrawGizmosSelected()
    {
        Vector2 f;  //移動前位置
        Vector2 t;  //移動後位置
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


        // 移動範囲を線で表示
        Gizmos.DrawLine(f, t);

        // 端にBox
        Gizmos.DrawWireCube(f, Vector2.one * 0.2f);
        Gizmos.DrawWireCube(t, Vector2.one * 0.2f);
    }
}
#endif