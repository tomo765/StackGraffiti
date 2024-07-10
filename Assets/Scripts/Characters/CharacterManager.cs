using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    private const int OnSleepLayer = -1;

    [SerializeField] private SpriteRenderer m_EyeRender;
    [SerializeField] private TextMeshProUGUI m_NameText;
    [SerializeField] private MeshFilter m_MeshFilter;
    [SerializeField] private float m_ScaleOnStage = 0.3f;

    private Rigidbody2D m_Rb2d;
    private PolygonCollider2D m_PolygonCollider2D;
    private CharacterController m_Controller;

    private bool m_IsDead = false;

    public SpriteRenderer EyeRenderer => m_EyeRender;
    public Rigidbody2D Rb2D => m_Rb2d;
    public MeshFilter MeshFilter => m_MeshFilter;
    public PolygonCollider2D Poly2D => m_PolygonCollider2D;

    private void Start()
    {
        m_Rb2d = GetComponent<Rigidbody2D>();
        m_PolygonCollider2D = GetComponent<PolygonCollider2D>();
        m_Controller = GetComponent<CharacterController>();

        Poly2D.enabled = false;
        m_Controller.SetManagerMember(m_Rb2d, OnSleep);
    }

    public void CreateOnStage(string playerName)
    {
        Poly2D.enabled = true;

        m_EyeRender.gameObject.SetActive(true);
        m_NameText.gameObject.SetActive(true);
        m_Controller.enabled = true;

        transform.position = GameManager.SpawnArea.transform.position;
        Rb2D.bodyType = RigidbodyType2D.Dynamic;

        m_NameText.text = playerName;

        transform.localScale = Vector3.one * m_ScaleOnStage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCharacterTouch(collision.tag);
        if (collision.CompareTag("Goal"))
        {
            OnClear();
        }
    }

    private void OnCharacterTouch(string tag)
    {
        switch (tag)
        {
            case "Dead":
                OnDead().FireAndForget();
                break;
            case "Needle":
                SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.TouchNeedleSE);
                OnUnmovable();
                break;
        }
    }

    private async Task OnSleep()
    {
        if (!GetComponent<Renderer>().isVisible) //画面外で操作不能になったら削除
        { 
            Destroy(gameObject);
            return;
        }
        else
        {
            m_IsDead = true;

            m_Rb2d.useAutoMass = false;
            m_Rb2d.mass *= 1.2f;
            m_Rb2d.gravityScale = 3;
            m_EyeRender.sprite = GeneralSettings.Instance.Sprite.DeathEye;
            m_Rb2d.sharedMaterial = GeneralSettings.Instance.PlayerSetting.PhysicsOnDead;

            GetComponent<MeshRenderer>().sortingOrder = OnSleepLayer;
            m_EyeRender.sortingOrder = OnSleepLayer;

            Destroy(m_Controller);
        }
        await Task.Delay(TaskExtension.OneSec, GameManager.Source.Token);
        GameManager.SleepCharacter();
    }

    private void OnUnmovable()
    {
        m_Rb2d.bodyType = RigidbodyType2D.Static;
        OnSleep().FireAndForget();
    }

    private async Task OnDead()
    {
        await Task.Delay(TaskExtension.OneSec, GameManager.Source.Token);

        Destroy(gameObject);
        if(m_IsDead) { return; }
        
        GameManager.SleepCharacter();
    }

    private void OnClear()
    {
        if(GameManager.IsClear) { return; }

        EffectContainer.Instance.PlayEffect(GeneralSettings.Instance.Prehab.ConfettiEffect, new Vector3(0, 5.5f, 0));
        m_Rb2d.bodyType = RigidbodyType2D.Static;
        GameManager.StageClear();
    }
}
