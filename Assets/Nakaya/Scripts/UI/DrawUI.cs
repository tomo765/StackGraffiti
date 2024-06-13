using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TInputField = TMPro.TMP_InputField;


public class DrawUI : MonoBehaviour
{
    [SerializeField] private RectTransform DrawArea;
    [SerializeField] private Button m_CreateCharaButton;
    [SerializeField] private TInputField m_NameInput;

    void Start()
    {
        
    }

    private void Init()
    {
        m_CreateCharaButton.onClick.RemoveAllListeners();
        m_CreateCharaButton.onClick.AddListener(() =>
        {
            
        });
    }


    void Update()
    {
        
    }
}


