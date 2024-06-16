using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TInputField = TMPro.TMP_InputField;


public class DrawUI : MonoBehaviour
{
    [SerializeField] private RectTransform DrawArea;
    [SerializeField] private Button m_CreateButton;
    [SerializeField] private TInputField m_NameInput;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        m_CreateButton.onClick.RemoveAllListeners();
        m_CreateButton.onClick.AddListener(() =>
        {
            
        });
    }


    void Update()
    {
        
    }
}


