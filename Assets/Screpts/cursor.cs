using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �J�[�\���ʒu���擾
        Vector3 mousePosition = Input.mousePosition;
        // �J�[�\���ʒu��z���W��10��
        mousePosition.z = 10;
        // �J�[�\���ʒu�����[���h���W�ɕϊ�
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);
        // GameObject��transform.position�ɃJ�[�\���ʒu(���[���h���W)����
        transform.position = target;
    }
}
