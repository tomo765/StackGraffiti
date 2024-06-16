using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_EyeRender;
    [SerializeField] private TextMeshProUGUI m_NameText;

    [SerializeField] private float m_Weight = 2;
    [SerializeField] private float m_ScaleOnStage = 0.3f;

    private Rigidbody2D m_Rb2d;
    private MeshFilter m_MeshFilter;
    private PolygonCollider2D m_PolygonCollider2D;

    private CharacterController m_Controller;
    
    private void Start()
    {
        m_Rb2d = GetComponent<Rigidbody2D>();
        m_MeshFilter = GetComponent<MeshFilter>();
        m_PolygonCollider2D = GetComponent<PolygonCollider2D>();
        m_Controller = GetComponent<CharacterController>();

        m_Controller.SetManagerMember(m_Rb2d, OnSleep, OnUnmovable, OnClear);

        m_Controller.enabled = false;
        m_PolygonCollider2D.enabled = false;
    }

    public void CreateOnStage(string name, Vector2[] colliders, MeshFilter filter)
    {
        filter.name = "CharacterFilter";

        m_NameText.text = name;
        m_PolygonCollider2D.points = colliders;
        m_MeshFilter = filter;

        transform.localScale = Vector3.one * m_ScaleOnStage;

        m_Controller.enabled = true;
        m_PolygonCollider2D.enabled = true;
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
