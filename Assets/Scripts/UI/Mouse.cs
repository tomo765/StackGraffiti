using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    // ���̃X�N���v�g��DrawArea�ɕt���Ă܂��B
    // �摜������ꏊ
    public Texture2D cursor;
    private Vector2 hotspot = new Vector2(10, 4);

    // �}�E�X���d�Ȃ�����cursor�̉摜��\��������
    public void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, hotspot, CursorMode.ForceSoftware);
    }

    // �}�E�X�����ꂽ�猳�̃J�[�\���ɖ߂�
    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }

    // �Q�l��https://youtu.be/yjhH70oiEvk?si=9HnkInvDzsmFMlPk
}
