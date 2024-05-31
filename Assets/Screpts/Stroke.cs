using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stroke: MonoBehaviour
{
    //���̍ގ�
    [SerializeField] Material lineMaterial;
    //���̐F
    [SerializeField] Color lineColor;
    //���̑���
    [Range(0.1f, 0.5f)]
    [SerializeField] float lineWidth;

    // LineRdenerer�^�̃��X�g�錾
    List<LineRenderer> lineRenderers;

    // Start is called before the first frame update
    void Start()
    {
        //List�̏�����
        lineRenderers = new List<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //�ǉ� �N���b�N�������^�C�~���O
        if (Input.GetMouseButtonDown(0))
        {
            //lineObj�𐶐����A����������
            _addLineObject();
        }

        //�ǉ��@�N���b�N���i�X�g���[�N���j
        if (Input.GetMouseButton(0))
        {
            _addPositionDataToLineRendererList();
        }
    }
    //�ǉ��@�N���b�N�����甭��
    void _addLineObject()
    {
        //��̃Q�[���I�u�W�F�N�g�쐬
        GameObject lineObj = new GameObject();
        //�I�u�W�F�N�g�̖��O��Stroke�ɕύX
        lineObj.name = "Stroke";
        //lineObj��LineRendere�R���|�[�l���g�ǉ�
        lineObj.AddComponent<LineRenderer>();
        //lineRenderer���X�g��lineObj��ǉ�
        lineRenderers.Add(lineObj.GetComponent<LineRenderer>());
        //lineObj�����g�̎q�v�f�ɐݒ�
        lineObj.transform.SetParent(transform);
        //lineObj����������
        _initRenderers();
    }


    //lineObj����������
    void _initRenderers()
    {
        //�����Ȃ��_��0�ɏ�����
        lineRenderers.Last().positionCount = 0;
        //�}�e���A����������
        lineRenderers.Last().material = lineMaterial;
        //�F�̏�����
        lineRenderers.Last().material.color = lineColor;
        //�����̏�����
        lineRenderers.Last().startWidth = lineWidth;
        lineRenderers.Last().endWidth = lineWidth;
    }

    void _addPositionDataToLineRendererList()
    {
        //�}�E�X�|�C���^������X�N���[�����W���擾
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f);

        //�X�N���[�����W�����[���h���W�ɕϊ�
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //���[���h���W�����[�J�����W�ɕϊ�
        Vector3 localPosition = transform.InverseTransformPoint(worldPosition.x, worldPosition.y, -1.0f);

        //lineRenderers�̍Ō��lineObj�̃��[�J���|�W�V��������L�̃��[�J���|�W�V�����ɐݒ�
        lineRenderers.Last().transform.localPosition = localPosition;

        //lineObj�̐��Ɛ����Ȃ��_�̐����X�V
        lineRenderers.Last().positionCount += 1;

        //LineRenderer�R���|�[�l���g���X�g���X�V
        lineRenderers.Last().SetPosition(lineRenderers.Last().positionCount - 1, worldPosition);

        //���Ƃ���`����������ɗ���悤�ɒ���
        lineRenderers.Last().sortingOrder = lineRenderers.Count;
    }
}
