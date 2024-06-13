using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSwichAction : MonoBehaviour
{
    public GameObject targetMoveBlock;  // 移動ブロック
    public Sprite imageOn;              // ON時の画像
    public Sprite imageOff;             // OFF時の画像
    public bool on = false;             // スイッチONフラグ

    // Start is called before the first frame update
    void Start()
    {
        if (on)
        {
            // ONの画像
            GetComponent<SpriteRenderer>().sprite = imageOn;
        }
        else
        {
            // OFF時の画像
            GetComponent<SpriteRenderer>().sprite = imageOff;
        }
    }

    // 接触してる時
    private void OnTriggerStay2D(Collider2D collision)
    {
        // もしプレイヤーが触れていたら
        if (collision.gameObject.tag == "Player")
        {
                on = true;  // スイッチON
                GetComponent<SpriteRenderer>().sprite = imageOn;    // ON時の画像
                // 移動ブロックのスクリプトを取得
                MovingBlock movBlock = targetMoveBlock.GetComponent<MovingBlock>();
                movBlock.Move(); // 移動を開始
 
        }
    }

    // 接触が離れた時
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (on)
            {
                on = false; // スイッチOFF
                GetComponent<SpriteRenderer>().sprite = imageOff;   // OFF時の画像
                                                                    // 移動ブロックのスクリプトを取得
                MovingBlock movBlock = targetMoveBlock.GetComponent<MovingBlock>();
                movBlock.Stop(); // 移動を停止
            }
        }
    }
}