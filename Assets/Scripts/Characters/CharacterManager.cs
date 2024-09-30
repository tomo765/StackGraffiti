using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary> �L�����N�^�[�̊Ǘ� </summary>
public class CharacterManager : MonoBehaviour
{
    private const int OnSleepLayer = -1;

    [SerializeField] private SpriteRenderer m_EyeRender;
    [SerializeField] private MeshFilter m_MeshFilter;
    [SerializeField] private float m_ScaleOnStage = 0.3f;

    private Rigidbody2D m_Rb2d;
    private PolygonCollider2D m_PolygonCollider2D;
    private CharacterController m_Controller;
    private SleepEffect m_SleepEffect;
    private CharacterNameCanvas m_CharacterName;

    private bool m_IsDead = false;
    private Vector3 m_SleepEffectPos = new Vector3(1, 1, 0);

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
        m_Controller.SetManagerMember(m_Rb2d, OnSleep, OnDead);
    }

    private void FixedUpdate()
    {
        SetEffectPos();
        SetNamePos();
    }

    private void SetEffectPos()
    {
        if (m_SleepEffect == null) { return; }
        m_SleepEffect.transform.position = transform.position + m_SleepEffectPos;
    }

    private void SetNamePos()
    {
        if(m_CharacterName == null) { return;}
        m_CharacterName.transform.position = transform.position + Vector3.up * 1.3f;
    }

    /// <summary> �L�����������I���A </summary>
    public void CreateOnStage(CharacterNameCanvas canvas)
    {
        Poly2D.enabled = true;
        Debug.Log(canvas.name);
        m_EyeRender.gameObject.SetActive(true);
        m_Controller.enabled = true;

        transform.position = GameManager.SpawnArea.transform.position;
        Rb2D.bodyType = RigidbodyType2D.Dynamic;

        m_CharacterName = canvas;

        transform.localScale = Vector3.one * m_ScaleOnStage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneLoadExtension.IsFading) { return; }
        OnCharacterTouch(collision.tag);
    }

    private void OnCharacterTouch(string tag)
    {
        switch (tag)
        {
            case "Dead":
                OnDead(0).FireAndForget();
                break;
            case "Needle":
                if (m_IsDead) { break; }
                SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.TouchNeedleSE);
                OnUnmovable();
                break;
            case "Goal":
                OnClear();
                break;
        }
    }

    /// <summary> ����L�����𖰂点�鏈�� </summary>
    private async Task OnSleep()
    {
        if (m_IsDead) { return; }
        m_IsDead = true;

        if (GameManager.IsHowToPlay) { return; }
        if(SceneLoadExtension.IsFading) { return; }

        GameManager.SetGameState(GameState.Drawing);
        if (!GetComponent<Renderer>().isVisible) //��ʊO�ő���s�\�ɂȂ�����폜
        { 
            Destroy(gameObject);
        }
        else
        {
            m_SleepEffect = EffectContainer.Instance.GetEffect(GeneralSettings.Instance.Prehab.SleepEffect);
            m_SleepEffect.transform.position = transform.position;

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

        if (GameManager.IsClear) { return; }
        GameManager.SleepCharacter();
        SoundManager.Instance.PlayMarimba(0);
    }

    /// <summary>�g�Q�Ȃǂ̈ړ��s�̃M�~�b�N�ɓ����������̏��� </summary>
    private void OnUnmovable()
    {
        m_Rb2d.bodyType = RigidbodyType2D.Kinematic;
        Rb2D.velocity = Vector3.zero;
        Rb2D.angularVelocity = 0;

        if (m_IsDead) { return; }
        OnSleep().FireAndForget();
    }

    /// <summary> �L�������폜���鏈�� </summary>
    private async Task OnDead(int waitTime)
    {
        Destroy(gameObject);
        Destroy(m_CharacterName.gameObject);
        m_SleepEffect?.gameObject.SetActive(false);

        await Task.Delay(waitTime);

        if (m_IsDead) { return; }
        GameManager.SleepCharacter();
        SoundManager.Instance.PlayMarimba(0);
        m_IsDead = true;
    }

    /// <summary> �S�[���������̏��� </summary>
    private void OnClear()
    {
        if(GameManager.IsClear) { return; }

        EffectContainer.Instance.PlayEffect(GeneralSettings.Instance.Prehab.ConfettiEffect, new Vector3(0, 5.5f, 0));
        m_Rb2d.bodyType = RigidbodyType2D.Static;
        GameManager.StageClear();
        DontDestroyCanvas.Instance.InitResultUI();
    }
}
