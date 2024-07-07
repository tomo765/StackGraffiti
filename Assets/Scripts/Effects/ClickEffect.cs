using UnityEngine;

public class ClickEffect : MonoBehaviour, IContainEffectBase
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

    public void StopEffect()
    {
        gameObject.SetActive(false);
    }

    public IContainEffectBase CreateEffect(Vector3 vec, Quaternion q, Transform parent)
    {
        return Instantiate(this, vec, q, parent);
    }
}

//éQçlÅ®https://zenn.dev/oclaocla/articles/87f9e61588c644