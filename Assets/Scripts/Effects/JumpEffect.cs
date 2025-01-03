using UnityEngine;

/// <summary> トランポリンなどでジャンプした時のエフェクト </summary>
public class JumpEffect : MonoBehaviour, IContainableEffect
{
    [SerializeField] private float m_AnimSpeed = 1.4f;
    private Animator m_Anim;
    public bool IsActive => gameObject.activeSelf;

    private void Start()
    {
        m_Anim = GetComponent<Animator>();
        m_Anim.speed = m_AnimSpeed;
    }

    private void Update()
    {
        gameObject.SetActive(m_Anim.IsPlaying());
    }

    public void Play(Vector2 vec)
    {
        gameObject.SetActive(true);
        transform.position = vec;
    }

    public void StopEffect()
    {
        gameObject.SetActive(false);
    }

    public IContainableEffect Create(Vector3 vec, Quaternion q, Transform parent)
    {
        return Instantiate(this, vec, q, parent);
    }
}
