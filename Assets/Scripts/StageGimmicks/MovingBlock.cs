using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f;  // x移動距離
    public float moveY = 0.0f;  // y移動距離
    public float times = 0.0f;  // 移動時間
    public float wait = 0.0f;   // 待機時間
    public bool isMoveWhenOn = false;   // 乗ったら移動するかどうか
    public bool isCanMove = true;   // 移動可能かどうか

    private Vector3 startPos;           // 初期位置
    private Vector3 endPos;             // 終了位置
    private bool isReverse = false;     // 逆移動フラグ
    private float movep = 0;            // 移動補完値

    void Start()
    {
        startPos = transform.position;  // 初期位置保存
        endPos = new Vector2(startPos.x + moveX, startPos.y + moveY);   // 終了位置を計算
        if (isMoveWhenOn)
        {

            // 乗った時に動かすので最初は動かない
            isCanMove = false;
        }
    }

    void Update()
    {
        if (isCanMove)
        {
            // 移動可能ブロックの場合
            float distance = Vector2.Distance(startPos, endPos);    // 移動距離を計算
            float ds = distance / times;        // １秒間の移動距離を計算
            float df = ds * Time.deltaTime;     // 経過時間から移動距離を計算
            movep += df / distance;             // 移動距離を計算
            if (isReverse)
            {
                // 逆移動
                transform.position = Vector2.Lerp(endPos, startPos, movep);
            }
            else
            {
                // 正移動
                transform.position = Vector2.Lerp(startPos, endPos, movep);
            }
            if (movep >= 1.0f)
            {
                movep = 0.0f;
                isReverse = !isReverse;
                isCanMove = false;
                if (isMoveWhenOn == false)
                {
                    // 乗った時に動くフラグOFF
                    Invoke("Move", wait);   // 待機時間後に移動を再開(Invokeは時間差でメソッドを呼び出す)
                }
            }
        }
    }

    // 移動フラグを立てる
    public void Move()
    {
        isCanMove = true;
    }

    // 移動フラグを降ろす
    public void Stop()
    {
        isCanMove = false;
    }
    // 接触時：乗った時に動くフラグを立てる
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // 接触したのがプレイヤーの場合、移動床の子にする
            // 子は親と一緒に動く
            collision.transform.SetParent(transform);
            if (isMoveWhenOn)
            {
                // 乗った時に動くフラグONの場合
                isCanMove = true;   // 移動フラグON
            }
        }
    }

    // 接触から外れる時：乗った時に動くフラグを降ろす
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // 接触したのがプレイヤーの場合、移動床の子から外す 
            collision.transform.SetParent(null);
        }
    }

    // 移動範囲表示
    private void OnDrawGizmosSelected()
    {
        Vector2 fromPos;
        if (startPos == Vector3.zero)
        {
            fromPos = transform.position;
        }
        else
        {
            fromPos = startPos;
        }
        // 移動範囲を線で表示
        Gizmos.DrawLine(fromPos, new Vector2(fromPos.x + moveX, fromPos.y + moveY));
        // スプライトのサイズ
        Vector2 size = GetComponent<Transform>().localScale;
        // 初期位置
        Gizmos.DrawWireCube(fromPos, new Vector2(size.x, size.y));
        // 終了位置
        Vector2 toPos = new Vector3(fromPos.x + moveX, fromPos.y + moveY);
        Gizmos.DrawWireCube(toPos, new Vector2(size.x, size.y));
    }
}
