using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPlatfrom : MonoBehaviour
{
    public float bounceForce = 10f; // ���˂��

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (playerRb == null) { return; }
        // �v���C���[�ɒ��˂�͂�������
        playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
    }
}
