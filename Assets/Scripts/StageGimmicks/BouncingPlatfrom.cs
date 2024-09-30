using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPlatfrom : MonoBehaviour
{
    private const string DefaultAnim = "BalloonDefault";
    private const string BoundAnim = "BalloonBound";

    [SerializeField] private float bounceForce = 10f; // ’µ‚Ë‚é—Í
    [SerializeField] private Animator m_Animator;

    private void Start()
    {
        m_Animator.Play(DefaultAnim);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Rigidbody2D>(out var rb2d)) { return; }

        var effectPos = (collision.transform.position + transform.position) / 2;

        EffectContainer.Instance.PlayEffect(GeneralSettings.Instance.Prehab.BalloonEffect, effectPos);
        SoundManager.Instance.PlayNewSE(GeneralSettings.Instance.Sound.JumpSE);
        rb2d.velocity = new Vector2(rb2d.velocity.x, bounceForce);

        m_Animator.Play(BoundAnim);
    }


}
