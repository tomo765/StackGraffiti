using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSwichAction : MonoBehaviour
{
    public GameObject targetMoveBlock;  // �ړ��u���b�N
    public Sprite imageOn;              // ON���̉摜
    public Sprite imageOff;             // OFF���̉摜
    public bool on = false;             // �X�C�b�`ON�t���O

    // Start is called before the first frame update
    void Start()
    {
        if (on)
        {
            // ON�̉摜
            GetComponent<SpriteRenderer>().sprite = imageOn;
        }
        else
        {
            // OFF���̉摜
            GetComponent<SpriteRenderer>().sprite = imageOff;
        }
    }

    // �ڐG���Ă鎞
    private void OnTriggerStay2D(Collider2D collision)
    {
        // �����v���C���[���G��Ă�����
        if (collision.gameObject.tag == "Player")
        {
                on = true;  // �X�C�b�`ON
                GetComponent<SpriteRenderer>().sprite = imageOn;    // ON���̉摜
                // �ړ��u���b�N�̃X�N���v�g���擾
                MovingBlock movBlock = targetMoveBlock.GetComponent<MovingBlock>();
                movBlock.Move(); // �ړ����J�n
 
        }
    }

    // �ڐG�����ꂽ��
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (on)
            {
                on = false; // �X�C�b�`OFF
                GetComponent<SpriteRenderer>().sprite = imageOff;   // OFF���̉摜
                                                                    // �ړ��u���b�N�̃X�N���v�g���擾
                MovingBlock movBlock = targetMoveBlock.GetComponent<MovingBlock>();
                movBlock.Stop(); // �ړ����~
            }
        }
    }
}