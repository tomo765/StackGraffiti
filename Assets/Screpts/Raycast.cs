using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Raycast : MonoBehaviour
{
    //RaycastAll�̈���
    private PointerEventData pointData;

    private void Start()
    {
        //RaycastAll�̈���PointerEvenData���쐬
        pointData = new PointerEventData(EventSystem.current);
    }

    private void Update()
    {
        //RaycastAll�̌��ʊi�[�p�̃��X�g�쐬
        List<RaycastResult> RayResult = new List<RaycastResult>();

        //PointerEvenData�ɁA�}�E�X�̈ʒu���Z�b�g
        pointData.position = Input.mousePosition;
        //RayCast�i�X�N���[�����W�j
        EventSystem.current.RaycastAll(pointData, RayResult);

        foreach (RaycastResult result in RayResult)
        {
            Debug.Log(result);
        }
    }
}
