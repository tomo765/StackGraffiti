using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPlatfrom : MonoBehaviour
{
    public float bounceForce = 10f; // ���˂��

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // �v���C���[�ɒ��˂�͂�������
                playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
            }
        //}
    }
}
