using UnityEngine;

public class BalloonEffect : MonoBehaviour, IContainEffectBase
{
    [SerializeField] private float m_AnimSpeed = 1;
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

    public void PlayEffect(Vector2 vec)
    {
        gameObject.SetActive(true);
        transform.position = vec;
    }

    public IContainEffectBase CreateEffect(Vector2 vec, Quaternion q, Transform parent)
    {
        return Instantiate(this, vec, q, parent);
    }
}
