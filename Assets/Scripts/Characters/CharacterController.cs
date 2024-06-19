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

    public delegate void OnTouchHandler(string tag);
    private OnTouchHandler m_OnTouch;
    private Action m_OnSleep;

    private bool m_CanJump = false;

    public void SetManagerMember(Rigidbody2D rb2d, OnTouchHandler onTouch, Action onSleep)
    {
        m_Rb2d = rb2d;
        m_OnTouch = onTouch;
        m_OnSleep = onSleep;
    }

    private void Update()
    {
        if (InputExtension.OnSleep)
        {
            m_OnSleep();
        }

        if (InputExtension.StartJump && m_CanJump)
        {
            m_Rb2d.velocity = new Vector2(m_Rb2d.velocity.x, 10);
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.JumpSE);
        }
    }

    private void FixedUpdate()
    {
        if (InputExtension.OnMove)
        {
            m_Rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 3, m_Rb2d.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCharacterTouch(collision.transform.tag, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnCharacterTouch(collision.transform.tag, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCharacterTouch(collision.transform.tag, true);
    }

    private void OnCharacterTouch(string tag, bool isEnter)
    {
        switch (tag)
        {
            case "Ground":
            case "Player":
                m_CanJump = isEnter;
                break;
            default:
                m_OnTouch(tag);
                break;
        }
    }
}
