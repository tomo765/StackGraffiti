using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterDraw : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private DrawUI m_DrawUI;
    private RectTransform m_Rect;

    private Mesh m_CharaMesh;
    private List<Vector2> m_MeshPoints;

    Vector3[] m_CornersInWor;  // ç∂â∫, ç∂è„, âEè„, âEâ∫ ÇÃèáÇ≈äiî[Ç≥ÇÍÇÈ

    private bool m_OnDrawing = false;
    private bool m_IsInArea = false;


    void Start()
    {
        m_Rect = GetComponent<RectTransform>();

        m_CornersInWor = new Vector3[4];
        m_Rect.GetWorldCorners(m_CornersInWor);
    }

    void Update()
    {
        if (InputExtension.MouseLeftUp)
        {
            FinishWrite();
            m_OnDrawing = false;
        }
    }

    void FixedUpdate()
    {
        if (m_OnDrawing && m_IsInArea && InputExtension.MouseLeftPush)
        {
            if (!CheckCursorPosOnArea()) { return; }

            CharacterCreator.OnHold(transform.position);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CharacterCreator.OnClick(transform.position);

        m_OnDrawing = true;
        m_MeshPoints = new List<Vector2>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_IsInArea = true;
        var newCursor = GeneralSettings.Instance.Cursor.DrawPen;
        Cursor.SetCursor(newCursor.Item1, Vector2.one * newCursor.Item2, CursorMode.ForceSoftware);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_IsInArea = false;
        var newCursor = GeneralSettings.Instance.Cursor.Default;

        Cursor.SetCursor(newCursor.Item1, Vector2.one * newCursor.Item2, CursorMode.ForceSoftware);
    }

    private void FinishWrite()
    {
        CharacterCreator.OnRelease();
        m_OnDrawing = false;
    }

    private bool CheckCursorPosOnArea()
    {
        return InputExtension.WorldMousePos.x >= m_CornersInWor[0].x &&
               InputExtension.WorldMousePos.x <= m_CornersInWor[2].x &&
               InputExtension.WorldMousePos.y >= m_CornersInWor[0].y &&
               InputExtension.WorldMousePos.y <= m_CornersInWor[2].y;
    }
}
