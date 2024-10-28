using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiEffect : MonoBehaviour, IContainEffectBase
{
    public bool IsActive => gameObject.activeSelf;

    public IContainEffectBase Create(Vector3 vec, Quaternion q, Transform parent)
    {
        return Instantiate(this, vec, q, parent);
    }

    public void Play(Vector2 vec)
    {
        gameObject.SetActive(true);
    }

    public void StopEffect()
    {
        gameObject.SetActive(false);
    }
}
