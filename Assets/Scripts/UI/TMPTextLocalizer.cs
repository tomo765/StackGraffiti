using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public sealed class TMPTextLocalizer : MonoBehaviour
{
    private TextMeshProUGUI m_LocalizeTarget;
    [SerializeField] private Config.TextID m_TextID;
    [SerializeField] private bool m_ChangeTextOnStart = true;  //Startˆ—‚É‚±‚ÌƒNƒ‰ƒX‚©‚ç•¶š‚ğ•ÏX‚·‚é‚©
    [SerializeField] private bool m_UpdateWhenLanguageChange;

    private void Start()
    {
        m_LocalizeTarget = GetComponent<TextMeshProUGUI>();

        if (m_ChangeTextOnStart) { SetLocalizeText(); }
        if(m_UpdateWhenLanguageChange) { GameManager.AddLocalizeAction(SetLocalizeText); }
    }

    public void SetLocalizeText() { m_LocalizeTarget.text = GetLocalizeText(); }

    public string GetLocalizeText() => Localizeations.GetLocalizeText(m_TextID, GameManager.Language);
}