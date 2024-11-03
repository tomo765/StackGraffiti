using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeamGimmick : MonoBehaviour
{
    private LineRenderer m_LineRenderer;
    private BoxCollider2D m_BoxCollider;
    [SerializeField] private LayerMask m_ExcludeLayer;

    [SerializeField] private float m_MaxDistance = 20;

    private RaycastHit2D BeamRay => Physics2D.Raycast(transform.position, -transform.right, m_MaxDistance, ~m_ExcludeLayer);

    void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_BoxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        var rayHit = BeamRay;

        Vector3[] points = GetLocalPoints(rayHit);
        Vector3 center = points.GetDistance();

        bool isNegatoveX = center.x < 0;
        center.x = Mathf.Abs(center.x);
        center.y = m_LineRenderer.endWidth;

        m_LineRenderer.SetPositions(points);
        m_BoxCollider.size = center;
        m_BoxCollider.offset = new Vector2(m_BoxCollider.size.x / 2f * (isNegatoveX ? -1 : 1), 0);
    }

    private Vector3[] GetLocalPoints(RaycastHit2D hit)
    {
        float distance;

        if (hit.collider == null) { distance = m_MaxDistance; }
        else { distance = (hit.point - (Vector2)transform.position).magnitude; }

        return new Vector3[]
        {
            Vector3.zero,
            -Vector3.right * distance
        };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out CharacterManager manager))
        {
            EffectContainer.Instance.PlayEffect(GeneralSettings.Instance.Prehab.DeleteCharaEffect, manager.transform.position);
            manager.OnDead(TaskExtension.OneSec).FireAndForget();
        }
    }
}

public static class VectorExtension
{
    public static Vector3 GetDistance(this Vector3[] points)
    {
        return new Vector3
            (
                points[1].x - points[0].x,
                points[1].y - points[0].y,
                points[1].z - points[0].z
            );
    }
}