using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TInputField = TMPro.TMP_InputField;


public class DrawUI : MonoBehaviour
{
    [SerializeField] private RectTransform DrawArea;
    [SerializeField] private Button m_VisibleButton;
    [SerializeField] private TInputField m_NameInput;

    void Start()
    {
        
    }

    private void Init()
    {
        m_VisibleButton.onClick.RemoveAllListeners();
        m_VisibleButton.onClick.AddListener(() =>
        {
            
        });
    }


    void Update()
    {
        
    }
}


