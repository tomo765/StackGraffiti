using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPlatfrom : MonoBehaviour
{
    public float bounceForce = 10f; // 跳ねる力

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (playerRb == null) { return; }
        // プレイヤーに跳ねる力を加える
        playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
    }
}
