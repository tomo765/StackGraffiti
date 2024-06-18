using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;
using System;

[RequireComponent(typeof(Image))]
public class CommonButton : Button
{
    [SerializeField] private Color m_EnterColor;
    [SerializeField] private Color m_ExitColor;

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
#endif

    private Image m_Img;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if(m_Img == null) { SetImage(); }

        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.HoverSE);
        m_Img.color = m_EnterColor;
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (m_Img == null) { SetImage(); }

        m_Img.color = m_ExitColor;
    }

    private void SetImage() => m_Img = GetComponent<Image>();
}




[CustomEditor(typeof(CommonButton))]
public class CommonSEButtonEditor : ButtonEditor
{
    SerializedProperty myCustomColorProperty;

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
#pragma warning restore CS0618 // 型またはメンバーが旧型式です

        // 変更があった場合にオブジェクトをマーク
        if (GUI.changed)
        {
            EditorUtility.SetDirty(customButton);
        }
    }
}
