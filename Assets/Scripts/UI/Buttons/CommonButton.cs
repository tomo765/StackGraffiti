using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;
using System;
using System.Threading.Tasks;


#if UNITY_EDITOR
using UnityEditor.UI;
#endif

[RequireComponent(typeof(Image))]
public class CommonButton : Button
{
    [SerializeField] private Color m_EnterColor = Color.white;
    [SerializeField] private Color m_ExitColor = Color.white;
    [SerializeField] private bool m_IsScaling = true;
    [SerializeField] private bool m_IsStopScalling = true;

    private bool m_PointerEntering = false;
    private Vector3 m_DefaultScale;

#if UNITY_EDITOR

    [Obsolete("CustomEditor以外で使用しない")]
    public Color EnterColor
    {
        get
        {
            return m_EnterColor;
        }
        set
        {
            m_EnterColor = value;
        }
    }
    [Obsolete("CustomEditor以外で使用しない")]
    public Color ExitColor
    {
        get
        {
            return m_ExitColor;
        }
        set
        {
            m_ExitColor = value;
        }
    }
    [Obsolete("CustomEditor以外で使用しない")]
    public bool IsScaling
    {
        get
        {
            return m_IsScaling;
        }
        set
        {
            m_IsScaling = value;
        }
    }
    [Obsolete("CustomEditor以外で使用しない")]
    public bool IsStopScalling
    {
        get
        {
            return m_IsStopScalling;
        }
        set
        {
            m_IsStopScalling = value;
        }
    }
#endif

    private Image m_Img;

    private new void Start()
    {
        m_DefaultScale = transform.localScale;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if(m_Img == null) { SetImage(); }

        m_PointerEntering = true;
        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.HoverSE);

        if(m_IsScaling) { PlayScaling(); }
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (m_Img == null) { SetImage(); }

        m_PointerEntering = false;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if(!m_IsStopScalling) { return; }
        m_PointerEntering = false;
    }

    private void SetImage() => m_Img = GetComponent<Image>();

    private async void PlayScaling()
    {
        float time = 0;
        int waitTime = 16;   //60フレーム
        float speed = 10f;

        while(m_PointerEntering)
        {
            transform.localScale = m_DefaultScale * (-Mathf.Cos(time) * 0.06f + 1.06f);
            time += waitTime / 1000f * speed;
            await Task.Delay(waitTime);
        }
        transform.localScale = m_DefaultScale;
    }
}



#if UNITY_EDITOR
[CustomEditor(typeof(CommonButton))]
public class CommonSEButtonEditor : ButtonEditor
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CommonButton customButton = (CommonButton)target;

#pragma warning disable CS0618 // 型またはメンバーが旧型式です
        customButton.EnterColor = EditorGUILayout.ColorField("EnterColor", customButton.EnterColor);
        customButton.ExitColor = EditorGUILayout.ColorField("ExitColor", customButton.ExitColor);
        customButton.IsScaling = EditorGUILayout.Toggle("IsScaling", customButton.IsScaling);
        customButton.IsStopScalling = EditorGUILayout.Toggle("IsStopScalling", customButton.IsStopScalling);
#pragma warning restore CS0618 // 型またはメンバーが旧型式です

        // 変更があった場合にオブジェクトをマーク
        if (GUI.changed)
        {
            EditorUtility.SetDirty(customButton);
        }
    }
}
#endif