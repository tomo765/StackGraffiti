using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPlatfrom : MonoBehaviour
{
    public float bounceForce = 10f; // ’µ‚Ë‚é—Í

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (playerRb == null) { return; }
        // ƒvƒŒƒCƒ„[‚É’µ‚Ë‚é—Í‚ğ‰Á‚¦‚é
        playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
    }
}
