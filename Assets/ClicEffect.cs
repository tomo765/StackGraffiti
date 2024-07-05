using System.Collections;
using UnityEngine;

public class ClicEffect : MonoBehaviour
{

    public float duration = 0.3f; // エフェクトの表示時間


    void Start()
    {
        Destroy(gameObject, duration);
    }



}

/*
 参考→https://zenn.dev/oclaocla/articles/87f9e61588c644
 */

