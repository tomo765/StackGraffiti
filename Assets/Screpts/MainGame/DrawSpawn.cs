using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSpawn : MonoBehaviour
{
    public GameObject prefab;
    private Vector3 mousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition.z = 10.0f;
            Instantiate(prefab, Camera.main.ScreenToWorldPoint(mousePosition), Quaternion.identity);
        }
    }
}
