using UnityEngine;

public class AddPolygonColliderToChild : MonoBehaviour
{
    void Start()
    {
        // Characterという名前の子オブジェクトのみ処理
        Transform characterChild = transform.Find("Character");
        if (characterChild == null) return;

        MeshFilter meshFilter = characterChild.GetComponent<MeshFilter>();
        if (meshFilter == null) return;

        Mesh mesh = meshFilter.sharedMesh;
        if (mesh == null) return;

        PolygonCollider2D polygonCollider = characterChild.gameObject.AddComponent<PolygonCollider2D>();
        polygonCollider.points = ConvertToVector2Array(mesh.vertices);
    }

    Vector2[] ConvertToVector2Array(Vector3[] vertices)
    {
        Vector2[] points = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            points[i] = vertices[i];
        }
        return points;
    }
}
