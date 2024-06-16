using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterDraw : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
{
    [SerializeField] private DrawUI m_DrawUI;
    [SerializeField] private RectTransform m_RectT;

    private Mesh m_CharaMesh;
    private List<Vector2> m_MeshPoints;

    private bool m_OnDrawing = false;


    void Start()
    {
        m_RectT = GetComponent<RectTransform>();

        Vector3[] worldCorners = new Vector3[4];  // ����, ����, �E��, �E�� �̏�

        m_RectT.GetWorldCorners(worldCorners);
    }

    void FixedUpdate()
    {
        if (m_OnDrawing && Input.GetMouseButton(0))
        {
            CharacterCreator.OnHold(transform.position);
        }
        if(m_OnDrawing && Input.GetMouseButtonUp(0))
        {
            FinishWrite();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CharacterCreator.OnClick(transform.position);

        m_OnDrawing = true;
        m_MeshPoints = new List<Vector2>();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!m_OnDrawing) { return; }

        FinishWrite();
    }

    private void FinishWrite()
    {
        CharacterCreator.OnRelease();
        m_OnDrawing = false;
    }

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    m_OnDrawing = false;
    //}
}
