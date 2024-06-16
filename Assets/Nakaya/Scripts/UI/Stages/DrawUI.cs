using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TInputField = TMPro.TMP_InputField;
using Unity.VisualScripting;
using UnityEngine.EventSystems;


public class DrawUI : MonoBehaviour
{
    [SerializeField] private Button m_CreateButton;
    [SerializeField] private TInputField m_NameInput;
    [SerializeField] private CharacterDraw m_DrawArea;
    
    void Start()
    {
        Init();
    }

    private void Init()
    {
        m_CreateButton.onClick.RemoveAllListeners();
        m_CreateButton.onClick.AddListener(() =>
        {
            Debug.Log(1);
            if (CharacterCreator.CanCreateChara) { return; }
            Debug.Log(2);
            CharacterCreator.CreateOnStage();

            gameObject.SetActive(false);
            m_CreateButton.gameObject.SetActive(false);
        });
    }
}


