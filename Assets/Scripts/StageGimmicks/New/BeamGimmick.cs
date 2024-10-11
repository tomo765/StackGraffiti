using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BeamGimmick : MonoBehaviour
{
    private LineRenderer m_LineRenderer;
    private LayerMask m_ExcludeLayer;
    private BoxCollider2D m_BoxCollider;

    void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_ExcludeLayer = gameObject.layer;
    }

    void Update()
    {
        Vector3[] points = GetPoints();
        Vector3 center = points.GetDistance();
        center.y = m_LineRenderer.endWidth;

        m_LineRenderer.SetPositions(points);
        m_BoxCollider.size = center;
        m_BoxCollider.offset = new Vector2(m_BoxCollider.size.x / 2f, 0);
    }

    private Vector3[] GetPoints()
    {
        RaycastHit2D r = Physics2D.Raycast(transform.position, Vector2.right, int.MaxValue, m_ExcludeLayer);
        Vector3 hitPos;

        if (r.collider == null) { hitPos = Vector3.right * 20; }
        else { hitPos = r.point; }

        return new Vector3[]
        {
            Vector3.zero,
            hitPos - transform.position
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