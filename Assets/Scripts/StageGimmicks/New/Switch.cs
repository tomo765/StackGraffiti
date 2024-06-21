using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : GimmickSender
{
    [SerializeField] private GimmickReceiver m_TargetGimmick;
    //[SerializeField] private bool m_IsActivate = false;

    private SpriteRenderer m_Renderer;
    private readonly List<Collider2D> m_Touches = new List<Collider2D>(10);

    private void Start()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartGimmick(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        StopGimmick(collision);
    }

    public sealed override void StartGimmick(Collider2D col)
    {
        if(m_Touches.Count == 0)
        {
            m_TargetGimmick?.ChangeActivate();
            m_Renderer.sprite = GeneralSettings.Instance.Sprite.SwitchPush;
        }

        m_Touches.Add(col);
    }

    public sealed override void StopGimmick(Collider2D col)
    {
        m_Touches.Remove(col);

        if(m_Touches.Count == 0)
        {
            m_TargetGimmick?.ChangeActivate();
            m_Renderer.sprite = GeneralSettings.Instance.Sprite.SwitchIdle;
        }
    }
}
