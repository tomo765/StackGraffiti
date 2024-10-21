using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeamGimmick : MonoBehaviour
{
    private LineRenderer m_LineRenderer;
    private LayerMask m_ExcludeLayer;
    private BoxCollider2D m_BoxCollider;

    private Vector3 angle => transform.eulerAngles;

    void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_ExcludeLayer = gameObject.layer;

        Debug.Log(m_LineRenderer.endWidth);
    }

    void Update()
    {
        Vector3[] points = GetLocalPoints();
        Vector3 center = points.GetDistance();

        bool reverseOffsetX = center.x < 0;
        center.y = m_LineRenderer.endWidth;
        center.x = Mathf.Abs(center.x);

        m_LineRenderer.SetPositions(points);
        m_BoxCollider.size = center;
        m_BoxCollider.offset = new Vector2(m_BoxCollider.size.x / 2f * (reverseOffsetX ? -1 : 1), 0);
    }

    private Vector3[] GetLocalPoints()
    {
        RaycastHit2D r = Physics2D.Raycast(transform.position, -transform.right, int.MaxValue, m_ExcludeLayer);
        Vector3 hitPos;

        if (r.collider == null) { hitPos = -Vector3.right * 20; }
        else
        {
            var mag = (r.point - (Vector2)transform.position).magnitude;
            hitPos = -Vector3.right * mag;
        }

        return new Vector3[]
        {
            Vector3.zero,
            hitPos
        };
    }
}

public static class VectorExtension
{
    public static Vector3 GetAvelage(this Vector3[] points)
    {
        return new Vector3
            (
                points.Select (p => p.x).Average(),
                points.Select (p => p.y).Average(),
                points.Select (p => p.z).Average()
            );
    }

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