using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
public class LineCollider : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private PolygonCollider2D _polygonCollider;

    void Start()
    {
        // LineRenderer��PolygonCollider2D�̃R���|�[�l���g���擾
        _lineRenderer = GetComponent<LineRenderer>();
        _polygonCollider = GetComponent<PolygonCollider2D>();

        // �K�v�ȃR���|�[�l���g���擾�ł��Ȃ��ꍇ�̓G���[���b�Z�[�W���o�͂��ďI��
        if (_lineRenderer == null || _polygonCollider == null)
        {
            Debug.LogError("LineCollider�ɂ�LineRenderer��PolygonCollider2D�̃R���|�[�l���g���K�v�ł��B");
            return;
        }

        // �����̃R���C�_�[��ݒ�
        UpdateCollider();
    }

    void Update()
    {
        // LineRenderer�̃|�C���g����������A�R���C�_�[�̃|�C���g�����قȂ�ꍇ�̓R���C�_�[���X�V
        if (_lineRenderer.positionCount > 1 && _lineRenderer.positionCount != _polygonCollider.points.Length)
        {
            UpdateCollider();
        }
    }

    void UpdateCollider()
    {
        // LineRenderer���瑾�����l�������R���C�_�[�|�C���g���擾���APolygonCollider2D�ɐݒ�
        Vector2[] colliderPoints = GetLineRendererPointsWithThickness();
        _polygonCollider.points = colliderPoints;
    }

    Vector2[] GetLineRendererPointsWithThickness()
    {
        int pointCount = _lineRenderer.positionCount;  // LineRenderer�̃|�C���g�����擾
        Vector2[] colliderPoints = new Vector2[pointCount * 2];  // �R���C�_�[�|�C���g�̔z���������

        for (int i = 0; i < pointCount; i++)
        {
            // LineRenderer�̊e�|�C���g�ɑ΂��āA�������l�������R���C�_�[�|�C���g���v�Z

            Vector3 point = _lineRenderer.GetPosition(i);  // i�Ԗڂ̃|�C���g�̃��[���h���W���擾
            Vector3 perpendicular = Vector3.zero;  // �@���x�N�g���̏�����

            if (i < pointCount - 1)
            {
                // �e�|�C���g���Ō�̃|�C���g�łȂ��ꍇ

                Vector3 nextPoint = _lineRenderer.GetPosition(i + 1);  // ���̃|�C���g�̃��[���h���W���擾
                Vector3 lineDirection = (nextPoint - point).normalized;  // �����̕����x�N�g�����v�Z
                perpendicular = new Vector3(-lineDirection.y, lineDirection.x, 0).normalized;  // �����̖@���x�N�g�����v�Z
            }
            else
            {
                // �e�|�C���g���Ō�̃|�C���g�̏ꍇ

                Vector3 firstPoint = _lineRenderer.GetPosition(0);  // �ŏ��̃|�C���g�̃��[���h���W���擾
                Vector3 lineDirection = (firstPoint - point).normalized;  // �����̕����x�N�g�����v�Z
                perpendicular = new Vector3(-lineDirection.y, lineDirection.x, 0).normalized;  // �����̖@���x�N�g�����v�Z
            }

            // �R���C�_�[�̃G�b�W�|�C���g���v�Z
            Vector3 edgePoint1 = point + perpendicular * _lineRenderer.startWidth / 2f;  // �����̑����̔��������@�������ɂ��炷
            Vector3 edgePoint2 = point - perpendicular * _lineRenderer.startWidth / 2f;  // �����̑����̔��������@�������ɂ��炷

            // ���[�J�����W�ɕϊ����ăR���C�_�[�|�C���g�ɒǉ�
            // ����
            colliderPoints[i * 2] = _polygonCollider.transform.InverseTransformPoint(edgePoint1);  // ���[�J�����W�ɕϊ����Ċi�[
            // �
            colliderPoints[i * 2 + 1] = _polygonCollider.transform.InverseTransformPoint(edgePoint2);  // ���[�J�����W�ɕϊ����Ċi�[
        }

        // ����������������
        // ���傫������

        return colliderPoints;  // �������l�������R���C�_�[�|�C���g�̔z���Ԃ�
    }
}