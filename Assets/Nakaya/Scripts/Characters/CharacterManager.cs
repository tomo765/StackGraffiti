using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_EyeRender;
    [SerializeField] private TextMeshProUGUI m_NameText;
    [SerializeField] private MeshFilter m_MeshFilter;

    [SerializeField] private float m_Weight = 2;
    [SerializeField] private float m_ScaleOnStage = 0.3f;

    private Rigidbody2D m_Rb2d;
    private PolygonCollider2D m_PolygonCollider2D;

    private CharacterController m_Controller;

    public SpriteRenderer EyeRenderer => m_EyeRender;
    public Rigidbody2D Rb2D => m_Rb2d;
    public MeshFilter MeshFilter => m_MeshFilter;
    public PolygonCollider2D Poly2D => m_PolygonCollider2D;

    private void Start()
    {
        m_Rb2d = GetComponent<Rigidbody2D>();
        m_PolygonCollider2D = GetComponent<PolygonCollider2D>();
        m_Controller = GetComponent<CharacterController>();

        m_Controller.SetManagerMember(m_Rb2d, OnSleep, OnUnmovable, OnClear);
    }

    public void CreateOnStage(string playerName)
    {
        m_EyeRender.gameObject.SetActive(true);
        m_NameText.gameObject.SetActive(true);

        transform.position = GameManager.SpawnPos;
        transform.localScale = Vector3.one * 0.3f;
        Rb2D.bodyType = RigidbodyType2D.Dynamic;

        m_NameText.text = playerName;

        transform.localScale = Vector3.one * m_ScaleOnStage;
    }

    private void OnSleep()
    {
        m_EyeRender.sprite = GeneralSettings.Instance.Sprite.DeathEye;
        m_Rb2d.mass *= m_Weight;

        GetComponent<MeshRenderer>().sortingOrder = -1;
        m_EyeRender.sortingOrder = -1;

        GameManager.AddSleepCount();
        GameManager.SetGameState(GameState.Drawing);

        Destroy(m_Controller);
        Destroy(this);
    }

    private void OnUnmovable()
    {
        m_Rb2d.bodyType = RigidbodyType2D.Static;
        OnSleep();
    }

    private void OnClear()
    {
        GameManager.SetGameState(GameState.Goal);
    }
}
