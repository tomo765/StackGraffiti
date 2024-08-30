using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPlatfrom : MonoBehaviour
{
    [SerializeField] private float bounceForce = 10f; // ’µ‚Ë‚é—Í

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Rigidbody2D>(out var rb2d)) { return; }

        var effectPos = collision.transform.position + transform.position;
        effectPos /= 2;

        EffectContainer.Instance.PlayEffect(GeneralSettings.Instance.Prehab.BalloonEffect, effectPos);
        rb2d.velocity = new Vector2(rb2d.velocity.x, bounceForce);
    }
}
