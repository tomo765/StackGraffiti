using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
public class LineCollider : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private PolygonCollider2D _polygonCollider;

    void Start()
    {
        // LineRendererとPolygonCollider2Dのコンポーネントを取得
        _lineRenderer = GetComponent<LineRenderer>();
        _polygonCollider = GetComponent<PolygonCollider2D>();

        // 必要なコンポーネントが取得できない場合はエラーメッセージを出力して終了
        if (_lineRenderer == null || _polygonCollider == null)
        {
            Debug.LogError("LineColliderにはLineRendererとPolygonCollider2Dのコンポーネントが必要です。");
            return;
        }

        // 初期のコライダーを設定
        UpdateCollider();
    }

    void Update()
    {
        // LineRendererのポイントが複数あり、コライダーのポイント数が異なる場合はコライダーを更新
        if (_lineRenderer.positionCount > 1 && _lineRenderer.positionCount != _polygonCollider.points.Length)
        {
            UpdateCollider();
        }
    }

    void UpdateCollider()
    {
        // LineRendererから太さを考慮したコライダーポイントを取得し、PolygonCollider2Dに設定
        Vector2[] colliderPoints = GetLineRendererPointsWithThickness();
        _polygonCollider.points = colliderPoints;
    }

    Vector2[] GetLineRendererPointsWithThickness()
    {
        int pointCount = _lineRenderer.positionCount;  // LineRendererのポイント数を取得
        Vector2[] colliderPoints = new Vector2[pointCount * 2];  // コライダーポイントの配列を初期化

        for (int i = 0; i < pointCount; i++)
        {
            // LineRendererの各ポイントに対して、太さを考慮したコライダーポイントを計算

            Vector3 point = _lineRenderer.GetPosition(i);  // i番目のポイントのワールド座標を取得
            Vector3 perpendicular = Vector3.zero;  // 法線ベクトルの初期化

            if (i < pointCount - 1)
            {
                // 各ポイントが最後のポイントでない場合

                Vector3 nextPoint = _lineRenderer.GetPosition(i + 1);  // 次のポイントのワールド座標を取得
                Vector3 lineDirection = (nextPoint - point).normalized;  // 線分の方向ベクトルを計算
                perpendicular = new Vector3(-lineDirection.y, lineDirection.x, 0).normalized;  // 線分の法線ベクトルを計算
            }
            else
            {
                // 各ポイントが最後のポイントの場合

                Vector3 firstPoint = _lineRenderer.GetPosition(0);  // 最初のポイントのワールド座標を取得
                Vector3 lineDirection = (firstPoint - point).normalized;  // 線分の方向ベクトルを計算
                perpendicular = new Vector3(-lineDirection.y, lineDirection.x, 0).normalized;  // 線分の法線ベクトルを計算
            }

            // コライダーのエッジポイントを計算
            Vector3 edgePoint1 = point + perpendicular * _lineRenderer.startWidth / 2f;  // 線分の太さの半分だけ法線方向にずらす
            Vector3 edgePoint2 = point - perpendicular * _lineRenderer.startWidth / 2f;  // 線分の太さの半分だけ法線方向にずらす

            // ローカル座標に変換してコライダーポイントに追加
            // 偶数
            colliderPoints[i * 2] = _polygonCollider.transform.InverseTransformPoint(edgePoint1);  // ローカル座標に変換して格納
            // 奇数
            colliderPoints[i * 2 + 1] = _polygonCollider.transform.InverseTransformPoint(edgePoint2);  // ローカル座標に変換して格納
        }

        // 偶数を小さい順に
        // 奇数を大きい順に

        return colliderPoints;  // 太さを考慮したコライダーポイントの配列を返す
    }
}