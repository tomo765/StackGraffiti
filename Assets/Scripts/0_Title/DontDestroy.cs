using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static bool isLoad = false;// ���g�����łɃ��[�h����Ă��邩�𔻒肷��t���O

    private void Awake()
    {
        if (isLoad)
        { // ���łɃ��[�h����Ă�����
            Destroy(this.gameObject); // �������g��j�����ďI��
            return;
        }

        isLoad = true; // ���[�h����Ă��Ȃ�������A�t���O�����[�h�ς݂ɐݒ肷��
        DontDestroyOnLoad(this.gameObject);
    }


}
