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
    private Action m_OnClear;

    private bool m_CanJump = false;

    public void SetManagerMember(Rigidbody2D rb2d, Action onSleep, Action onUnmovable, Action onClear)
    {
        m_Rb2d = rb2d;
        m_OnSleep = onSleep;
        m_OnUnmovable = onUnmovable;
        m_OnClear = onClear;
    }

    private void Update()
    {
        if(InputExtension.OnSleep)
        {
            m_OnSleep();
            GameManager.SleepCharacter();
        }

        if(InputExtension.StartJump && m_CanJump)
        {
            m_Rb2d.AddForce(new Vector2(0, 1.5f), ForceMode2D.Impulse);
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
        switch (collision.transform.tag)
        {
            case "Ground":
                m_CanJump = true;
                break;
            case "Dead":
                break;
            case "Needle":
                break;
            case "Goal":
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            m_CanJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
