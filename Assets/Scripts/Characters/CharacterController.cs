using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

/// <summary> ÉLÉÉÉâÇÃëÄçÏíÜÇÃêßå‰ÇÇ∑ÇÈ </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    private Rigidbody2D m_Rb2d;

    private const float MaxMoveSpeed = 2.5f;
    private const float AirMoveSpeed = 0.5f;

    private Func<Task> m_OnSleep;
    private Action m_OnDead;

    private bool m_OnGround = false;

    public void SetManagerMember(Rigidbody2D rb2d, Func<Task> onSleep, Action onDead)
    {
        m_Rb2d = rb2d;
        m_OnSleep = onSleep;
        m_OnDead = onDead;
    }

    private void Update()
    {
        if (InputExtension.OnSleep)
        {
            m_OnSleep().FireAndForget();
        }

        if (InputExtension.OnDeleteColtrolChara)
        {
            m_OnDead();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        SoundManager.Instance.PlayMarimba(m_Rb2d.velocity.x / MaxMoveSpeed);

        if (SceneLoadExtension.IsFading) { return; }
        if (!InputExtension.OnMove) { return; }
        if (!GameManager.IsPlaying) { return; }
        if (DontDestroyCanvas.Instance.IsViewInStage) { return; }

        if (m_OnGround)
        {
            m_Rb2d.velocity = InputExtension.MoveByKey(MaxMoveSpeed) + Vector2.up * m_Rb2d.velocity.y;
        }
        else
        {
            var newVec = m_Rb2d.velocity + InputExtension.MoveByKey(AirMoveSpeed);
            newVec.x = Mathf.Clamp(newVec.x, -MaxMoveSpeed, MaxMoveSpeed);
            m_Rb2d.velocity = newVec;
        }
    }
}