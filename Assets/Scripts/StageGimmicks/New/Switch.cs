using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Switch : GimmickSender
{
    [SerializeField] private GimmickReceiver m_TargetGimmick;

    private SpriteRenderer m_Renderer;
    private readonly List<Collider2D> m_Touches = new List<Collider2D>(10);
    private bool IsPushSwitch => m_Touches.Count != 0;

    private void Start()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser")) { return; }
        StartGimmick(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser")) { return; }
        StopGimmick(collision);
    }

    public sealed override void StartGimmick(Collider2D col)
    {
        if(!IsPushSwitch)
        {
            m_TargetGimmick?.ChangeActivate();
            m_Renderer.sprite = GeneralSettings.Instance.Sprite.SwitchPush;
        }

        m_Touches.Add(col);
    }

    public sealed override void StopGimmick(Collider2D col)
    {
        m_Touches.Remove(col);

        if(!IsPushSwitch)
        {
            m_TargetGimmick?.ChangeActivate();
            m_Renderer.sprite = GeneralSettings.Instance.Sprite.SwitchIdle;
        }
    }
}


#if UNITY_EDITOR
public partial class Switch : GimmickSender
{
    [Space(15), SerializeField] private Color m_GizmosCol = Color.red;

    private void OnDrawGizmosSelected()
    {
        if(m_TargetGimmick == null) { return; }

        Gizmos.color = m_GizmosCol;
        Gizmos.DrawLine(transform.position, m_TargetGimmick.transform.position);  //作動させるギミックを線でつなぐ
    }
}
#endif