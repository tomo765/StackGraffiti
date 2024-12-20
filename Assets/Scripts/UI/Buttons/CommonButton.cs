using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;
using System;
using System.Threading.Tasks;
using System.Threading;
#if UNITY_EDITOR
using UnityEditor.UI;
#endif

/// <summary> 機能追加したButtonクラス </summary>
[RequireComponent(typeof(Image))]
public class CommonButton : Button
{
    [SerializeField] private Color m_EnterColor = Color.white;
    [SerializeField] private Color m_ExitColor = Color.white;
    [SerializeField] private bool m_ScallingAlways = false;
    [SerializeField] private bool m_IsScaling = true;
    [SerializeField] private bool m_IsStopScalling = true;
    [SerializeField] private bool m_ChangeColor = false;

    private bool m_PointerEntering = false;
    private Vector3 m_DefaultScale;

    private CancellationTokenSource m_Source;

#if UNITY_EDITOR

    [Obsolete("CustomEditor以外で使用しない")]
    public Color EnterColor
    {
        get { return m_EnterColor; }
        set { m_EnterColor = value; }
    }
    [Obsolete("CustomEditor以外で使用しない")]
    public Color ExitColor
    {
        get { return m_ExitColor; }
        set { m_ExitColor = value; }
    }
    [Obsolete("CustomEditor以外で使用しない")]
    public bool ScallingAlways
    {
        get { return m_ScallingAlways; }
        set { m_ScallingAlways = value; }
    }
    [Obsolete("CustomEditor以外で使用しない")]
    public bool IsScaling
    {
        get { return m_IsScaling; }
        set { m_IsScaling = value; }
    }
    [Obsolete("CustomEditor以外で使用しない")]
    public bool IsStopScalling
    {
        get { return m_IsStopScalling; }
        set { m_IsStopScalling = value; }
    }
    [Obsolete("CustomEditor以外で使用しない")]
    public bool ChangeColor
    {
        get { return m_ChangeColor; }
        set { m_ChangeColor = value; }
    }
#endif

    private Image m_Img;

    private new void Start()
    {
        m_DefaultScale = transform.localScale;
        m_Img = GetComponent<Image>();

#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying && m_ScallingAlways)
#else
        if (m_ScallingAlways)
#endif
        {
            m_Source = new CancellationTokenSource();
            PlayScaling().FireAndForget(); 
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if(!interactable) { return; }
        if(m_ScallingAlways) { return; }
        //if(m_Img == null) { SetImage(); }

        m_PointerEntering = true;
        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.HoverSE);

        if(m_IsScaling) { PlayScaling().FireAndForget(); }
        if(m_ChangeColor) { m_Img.color = m_EnterColor; }
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!interactable) { return; }
        if (m_ChangeColor) { m_Img.color = m_ExitColor; }
        if(m_ScallingAlways) { return; }

        m_PointerEntering = false;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!interactable) { return; }
        base.OnPointerUp(eventData);
        if(m_ScallingAlways) { return; }
        if(!m_IsStopScalling) { return; }
        if (m_ChangeColor) { m_Img.color = m_ExitColor; }

        m_PointerEntering = false;
    }

    private void SetImage() => m_Img = GetComponent<Image>();

    /// <summary> アニメーション </summary>
    private async Task PlayScaling()
    {
        float time = 0;
        float speed = 10f;
        float amplitude = 0.06f;
        float intercept = 1.06f;

        while (m_PointerEntering || m_ScallingAlways)
        {
            transform.localScale = m_DefaultScale * (-Mathf.Cos(time) * amplitude + intercept);
            time += TaskExtension.SixtyFrame / (float)TaskExtension.OneSec * speed;
            await Task.Delay(TaskExtension.SixtyFrame);
            m_Source?.Token.ThrowIfCancellationRequested();
        }
        transform.localScale = m_DefaultScale;
    }

    private new void OnDestroy()
    {
        m_Source?.Cancel();
    }
}


/// <summary> CommonButtonのエディター拡張 </summary>
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
        customButton.ScallingAlways = EditorGUILayout.Toggle("ScallingAlways", customButton.ScallingAlways);
        customButton.IsScaling = EditorGUILayout.Toggle("IsScaling", customButton.IsScaling);
        customButton.IsStopScalling = EditorGUILayout.Toggle("IsStopScalling", customButton.IsStopScalling);
        customButton.ChangeColor = EditorGUILayout.Toggle("ChangeColor", customButton.ChangeColor);
#pragma warning restore CS0618 // 型またはメンバーが旧型式です

        // 変更があった場合にオブジェクトをマーク
        if (GUI.changed)
        {
            EditorUtility.SetDirty(customButton);
        }
    }
}
#endif