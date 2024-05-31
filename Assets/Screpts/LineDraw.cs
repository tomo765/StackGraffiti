using UnityEngine;
public class LineDraw : MonoBehaviour
{
    [SerializeField] private LineRenderer _rend;
    [SerializeField] private Camera _cam;
    private int posCount = 0;
    private float interval = 0.1f;
    private void Update()
    {
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
            SetPosition(mousePos);
        else if (Input.GetMouseButtonUp(0))
            posCount = 0;

        // �J�[�\���ʒu���擾
        Vector3 mousePosition = Input.mousePosition;
        // �J�[�\���ʒu��z���W��10��
        mousePosition.z = 0;
        // �J�[�\���ʒu�����[���h���W�ɕϊ�
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);
        // GameObject��transform.position�ɃJ�[�\���ʒu(���[���h���W)����
        // transform.position = target;
    }



    private void SetPosition(Vector2 pos)
    {
        if (!PosCheck(pos)) return;
        posCount++;
        _rend.positionCount = posCount;
        _rend.SetPosition(posCount - 1, pos);
    }
    private bool PosCheck(Vector2 pos)
    {
        if (posCount == 0) return true;
        float distance = Vector2.Distance(_rend.GetPosition(posCount - 1), pos);
        if (distance > interval)
            return true;
        else
            return false;
    }

    // ���G�`���G���A�ŕ`���Ă��邩�ǂ����̔���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DrawArea")
        {
            this.gameObject.SetActive(true);
        }

        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
