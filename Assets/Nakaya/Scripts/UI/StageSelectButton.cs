using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class StageSelectButton : MonoBehaviour
{
    private string m_StageName;
    public string StageName => m_StageName;

    [SerializeField] private Button m_TransitionButton;
    [SerializeField] private Image m_StageImg;
    [SerializeField] private TextMeshProUGUI m_StageLevelText;


    public void Init(string name, int stageLevel)
    {
        m_StageName = name;
        m_StageImg.sprite = GeneralSettings.Instance.Sprite.StageCuts[stageLevel];
        m_StageLevelText.text = stageLevel.ToString();
        m_TransitionButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(m_StageName);
            GameManager.SetCullentStage(StageState.Stage1);
            SoundManager.Instance?.PlayNewBGM(GeneralSettings.Instance.Sound.SelectSE);
        });


        if(stageLevel > 5) { gameObject.SetActive(false); }
    }
}
