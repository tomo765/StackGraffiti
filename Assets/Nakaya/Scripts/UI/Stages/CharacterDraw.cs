using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterDraw : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private DrawUI m_DrawUI;
    [SerializeField] private RectTransform m_RectT;

    private Mesh m_CharaMesh;
    private List<Vector2> m_MeshPoints;

    private bool m_OnDrawing = false;
    private bool m_IsInArea = false;


    void Start()
    {
        m_RectT = GetComponent<RectTransform>();

        Vector3[] worldCorners = new Vector3[4];  // 左下, 左上, 右上, 右下 の順

        m_RectT.GetWorldCorners(worldCorners);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("asfadfasdasd");
            FinishWrite();
            m_OnDrawing = false;
        }
    }

    void FixedUpdate()  // ToDo : 書ける範囲からはみ出したあとにクリックした状態で戻ってきたら書き続けれるようにする
    {
        if (m_OnDrawing && m_IsInArea && Input.GetMouseButton(0))
        {
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_IsInArea = false;
    }

    private void FinishWrite()
    {
        CharacterCreator.OnRelease();
        m_OnDrawing = false;
    }
}
