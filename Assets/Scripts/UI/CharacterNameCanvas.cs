using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNameCanvas : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI m_NameText;

    public void SetCharacterName(string name)
    {
        m_NameText.text = name;
    }
}
