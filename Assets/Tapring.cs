using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tapring : MonoBehaviour
{
    private Camera mainCamera;
    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        // ���C���J�������擾
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            // ��ʂ��^�b�v�����Ƃ��̏���
            Vector2 tapPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            GameObject tap = Instantiate(effect);

            // �Q�[���I�u�W�F�N�g�̈ʒu��tapPosition�ɂ���
            tap.transform.position = tapPosition;

        }
    }

}
