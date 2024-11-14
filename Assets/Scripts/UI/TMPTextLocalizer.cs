using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public sealed class TMPTextLocalizer : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI m_LocalizeTarget;
    private string m_PrevText = "";
    private string m_NextText = "";

    [SerializeField] private Config.TextID m_TextID;
    [SerializeField] private bool m_ChangeTextOnStart = true;  //Start処理時にこのクラスから文字を変更するか
    [SerializeField] private bool m_UpdateWhenLanguageChange;

    [Header("ローカライズ")]
    [SerializeField] private bool ChangeAsStatic = false;
    [SerializeField] private bool ChangeAsScene = false;

    private void Start()
    {
        m_LocalizeTarget = GetComponent<TextMeshProUGUI>();
        InitLocalize();

        if (m_ChangeTextOnStart) { SetLocalizeText(); }
        if(m_UpdateWhenLanguageChange) { GameManager.AddStaticAction(SetLocalizeText); }
    }

    private void InitLocalize()
    {
        if(!m_UpdateWhenLanguageChange) { return; }

        if (ChangeAsStatic) { GameManager.AddStaticAction(SetLocalizeText); }
        if(ChangeAsScene)   { GameManager.AddSceneLocalizeAction(SetLocalizeText); }
    }

    public void SetBackAndForthText(string back, string forth)
    {
        m_PrevText = back;
        m_NextText = forth;
    }

    public void SetLocalizeText() { m_LocalizeTarget.text = m_PrevText + GetLocalizeText() + m_NextText; }

    public string GetLocalizeText() => Localizeations.GetLocalizeText(m_TextID, GameManager.Language);
}