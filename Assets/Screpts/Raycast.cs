using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Raycast : MonoBehaviour
{
    //RaycastAllの引数
    private PointerEventData pointData;

    private void Start()
    {
        //RaycastAllの引数PointerEvenDataを作成
        pointData = new PointerEventData(EventSystem.current);
    }

    private void Update()
    {
        //RaycastAllの結果格納用のリスト作成
        List<RaycastResult> RayResult = new List<RaycastResult>();

        //PointerEvenDataに、マウスの位置をセット
        pointData.position = Input.mousePosition;
        //RayCast（スクリーン座標）
        EventSystem.current.RaycastAll(pointData, RayResult);

        foreach (RaycastResult result in RayResult)
        {
            Debug.Log(result);
        }
    }
}
