using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContainEffectBase
{
    bool IsActive { get; }
    void Play(Vector2 vec);
    void StopEffect();

    IContainEffectBase Create(Vector3 vec, Quaternion q, Transform parent);
}

public static class EffectExtension
{
    public static bool IsPlaying(this Animator anim) => anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1;
}