using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 �����u���b�N���������������X�N���v�g
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
