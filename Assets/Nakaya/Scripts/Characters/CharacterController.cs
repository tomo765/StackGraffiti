using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq.Expressions;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    private Rigidbody2D m_Rb2d;

    private Action m_OnSleep;
    private Action m_OnUnmovable;
    private Action m_OnDead;
    private Action m_OnClear;

    private bool m_CanJump = false;

    public void SetManagerMember(Rigidbody2D rb2d, Action onSleep, Action onUnmovable, Action onDesd, Action onClear)
    {
        m_Rb2d = rb2d;
        m_OnSleep = onSleep;
        m_OnUnmovable = onUnmovable;
        m_OnDead = onDesd;
        m_OnClear = onClear;
    }

    private void Update()
    {
        if(InputExtension.OnSleep)
        {
            m_OnSleep();
        }

        if(InputExtension.StartJump && m_CanJump)
        {
            m_Rb2d.velocity = new Vector2(m_Rb2d.velocity.x, 10);
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.JumpSE);
        }
    }

    private void FixedUpdate()
    {
        if(InputExtension.OnMove)
        {
            m_Rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 3, m_Rb2d.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ByTag(collision.transform.tag, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ByTag(collision.transform.tag, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ByTag(collision.transform.tag, true);
    }

    private void ByTag(string tag, bool isEnter)
    {
        switch (tag)
        {
            case "Ground":
            case "Player":
                m_CanJump = isEnter;
                break;
            case "Dead":
                m_OnDead();
                break;
            case "Needle":
                SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.TouchNeedleSE);  //ToDo : ニードルのサウンドに直す
                m_OnUnmovable();
                break;
            case "Goal":
                m_OnClear();
                break;
        }
    }
}
