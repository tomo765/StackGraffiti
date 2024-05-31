using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f;  // x�ړ�����
    public float moveY = 0.0f;  // y�ړ�����
    public float times = 0.0f;  // �ړ�����
    public float wait = 0.0f;   // �ҋ@����
    public bool isMoveWhenOn = false;   // �������ړ����邩�ǂ���
    public bool isCanMove = true;   // �ړ��\���ǂ���

    private Vector3 startPos;           // �����ʒu
    private Vector3 endPos;             // �I���ʒu
    private bool isReverse = false;     // �t�ړ��t���O
    private float movep = 0;            // �ړ��⊮�l

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;  // �����ʒu�ۑ�
        endPos = new Vector2(startPos.x + moveX, startPos.y + moveY);   // �I���ʒu���v�Z
        if (isMoveWhenOn)
        {

            // ��������ɓ������̂ōŏ��͓����Ȃ�
            isCanMove = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCanMove)
        {
            // �ړ��\�u���b�N�̏ꍇ
            float distance = Vector2.Distance(startPos, endPos);    // �ړ��������v�Z
            float ds = distance / times;        // �P�b�Ԃ̈ړ��������v�Z
            float df = ds * Time.deltaTime;     // �o�ߎ��Ԃ���ړ��������v�Z
            movep += df / distance;             // �ړ��������v�Z
            if (isReverse)
            {
                // �t�ړ�
                transform.position = Vector2.Lerp(endPos, startPos, movep);
            }
            else
            {
                // ���ړ�
                transform.position = Vector2.Lerp(startPos, endPos, movep);
            }
            if (movep >= 1.0f)
            {
                movep = 0.0f;
                isReverse = !isReverse;
                isCanMove = false;
                if (isMoveWhenOn == false)
                {
                    // ��������ɓ����t���OOFF
                    Invoke("Move", wait);   // �ҋ@���Ԍ�Ɉړ����ĊJ(Invoke�͎��ԍ��Ń��\�b�h���Ăяo��)
                }
            }
        }
    }

    // �ړ��t���O�𗧂Ă�
    public void Move()
    {
        isCanMove = true;
    }

    // �ړ��t���O���~�낷
    public void Stop()
    {
        isCanMove = false;
    }
    // �ڐG���F��������ɓ����t���O�𗧂Ă�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // �ڐG�����̂��v���C���[�̏ꍇ�A�ړ����̎q�ɂ���
            // �q�͐e�ƈꏏ�ɓ���
            collision.transform.SetParent(transform);
            if (isMoveWhenOn)
            {
                // ��������ɓ����t���OON�̏ꍇ
                isCanMove = true;   // �ړ��t���OON
            }
        }
    }

    // �ڐG����O��鎞�F��������ɓ����t���O���~�낷
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // �ڐG�����̂��v���C���[�̏ꍇ�A�ړ����̎q����O�� 
            collision.transform.SetParent(null);
        }
    }

    // �ړ��͈͕\��
    private void OnDrawGizmosSelected()
    {
        Vector2 fromPos;
        if (startPos == Vector3.zero)
        {
            fromPos = transform.position;
        }
        else
        {
            fromPos = startPos;
        }
        // �ړ��͈͂���ŕ\��
        Gizmos.DrawLine(fromPos, new Vector2(fromPos.x + moveX, fromPos.y + moveY));
        // �X�v���C�g�̃T�C�Y
        Vector2 size = GetComponent<Transform>().localScale;
        // �����ʒu
        Gizmos.DrawWireCube(fromPos, new Vector2(size.x, size.y));
        // �I���ʒu
        Vector2 toPos = new Vector3(fromPos.x + moveX, fromPos.y + moveY);
        Gizmos.DrawWireCube(toPos, new Vector2(size.x, size.y));
    }
}
