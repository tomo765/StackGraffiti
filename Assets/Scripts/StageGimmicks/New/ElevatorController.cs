using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary> 移動床のコントローラー </summary>
public partial class ElevatorController : GimmickReceiver
{
    [SerializeField] private Vector2 m_MoveVec;
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private float m_WaitTime = 0;
    [SerializeField] private bool m_OnActivate = false;
    [SerializeField] private bool m_Reversible = true;

    private const int MinTime = 0;
    private const int MaxTime = 1;

    private Vector2 m_StartPos;
    private Vector2 m_EndPos;
    private float m_ArrivalTime;      // 0～1で変動 初期位置 = 0, 移動後 = 1　として移動させる
    private bool m_OnReverse = false;  //true : 復路, false : 往路
    private bool isInactive = false;
    private float m_CullentWaitTime = 0;

    public bool OnActivate => m_OnActivate;

    private float GetAddTime() => Time.fixedDeltaTime * (m_OnReverse && m_Reversible ? -1 : 1) * m_MoveSpeed;

    private void Start()
    {
        m_StartPos = transform.position;
        m_EndPos = m_StartPos + m_MoveVec;
        m_CullentWaitTime = m_WaitTime;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(GameManager.IsClear) { return; }
        if (!m_OnActivate) { return; }

        if(m_CullentWaitTime >= m_WaitTime) { DoMove(); }
        else { DoWait(); }

        void DoMove()
        {
            m_ArrivalTime += GetAddTime();
            transform.position = Vector2.Lerp(m_StartPos, m_EndPos, m_ArrivalTime);

            if (m_ArrivalTime >= MaxTime || m_ArrivalTime <= MinTime)
            {
                m_CullentWaitTime = 0;
                m_OnReverse = !m_OnReverse;
                m_ArrivalTime = Mathf.Clamp(m_ArrivalTime, MinTime, MaxTime);
            }
        }

        void DoWait() { m_CullentWaitTime += Time.fixedDeltaTime; }
    }

    public override void ChangeActivate() => m_OnActivate = !m_OnActivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.TryGetComponent(out CharacterManager manager)) { return; }
        if (manager.IsDead) { return; }

        manager.transform.SetParent(transform);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out CharacterManager manager)) { return; }
        if (manager.IsDead) { return; }
        if(isInactive) { return; }

        manager.transform.SetParent(null);
    }

    private void OnDisable()
    {
        isInactive = true;
    }
}

#if UNITY_EDITOR

//編集中はエレベーターの中心から移動後の位置まで線が伸びる
//再生中はエレベーターの移動前の位置から移動後の位置まで線が伸びる

public partial class ElevatorController : GimmickReceiver
{
    [Space(15), SerializeField] private Color m_GizmosCol = Color.red;

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

        Gizmos.color = m_GizmosCol;
        // 移動範囲を線で表示
        Gizmos.DrawLine(f, t);

        // 端にBox
        Gizmos.DrawWireCube(f, Vector2.one * 0.2f);
        Gizmos.DrawWireCube(t, Vector2.one * 0.2f);
    }
}
#endif