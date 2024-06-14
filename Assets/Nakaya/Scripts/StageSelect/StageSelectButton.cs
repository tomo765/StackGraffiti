using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    private string m_StageName;
    public string StageName => m_StageName;

    [SerializeField] private Button m_TransitionButton;
    [SerializeField] private Image m_StageImg;
    [SerializeField] private TMPro.TextMeshPro m_StageLevelText;

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
