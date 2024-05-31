using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 押すブロックが落ちたら消えるスクリプト
 */
public class MoveBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "UnderDead")
        {
            Destroy(this.gameObject);
        }
    }
}
