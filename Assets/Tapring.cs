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
        // メインカメラを取得
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            // 画面をタップしたときの処理
            Vector2 tapPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            GameObject tap = Instantiate(effect);

            // ゲームオブジェクトの位置をtapPositionにする
            tap.transform.position = tapPosition;

        }
    }

}
