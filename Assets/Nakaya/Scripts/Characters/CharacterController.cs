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


    public void SetManagerMember(Rigidbody2D rb2d, Action onSleep, Action onUnmovable, Action onClear)
    {
        m_Rb2d = rb2d;
        m_OnSleep = onSleep;
        m_OnUnmovable = onUnmovable;
        m_OnClear = onClear;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.D))
        {
            m_Rb2d.AddForce(Vector2.right, ForceMode2D.Impulse);
        }
        if(Input.GetKey(KeyCode.A))
        {
            m_Rb2d.AddForce(-Vector2.right, ForceMode2D.Impulse);
        }
    }
}
