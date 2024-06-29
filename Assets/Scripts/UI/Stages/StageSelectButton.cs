using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StageSelectButton : MonoBehaviour
{

    [SerializeField] private Button m_TransitionButton;
    [SerializeField] private TextMeshProUGUI m_StageLevelText;
    [SerializeField] private Image[] m_Stars;

    private string m_StageName;
    private int m_StageLevel;
    public string StageName => m_StageName;

    public void Init(string name, int stageNum)
    {
        m_StageName = name;
        m_StageLevel = stageNum;
        m_StageLevelText.text = "ステージ" + stageNum.ToString();
        m_TransitionButton.onClick.AddListener(() =>
        {
            SceneLoadExtension.LoadWithFade(m_StageName, GeneralSettings.Instance.Sound.FadeSE);
            GameManager.StartStage((StageType)stageNum);
        });

        for (int i = 0; i < StageDataUtility.StageDatas.StageScores[stageNum - 1].StarLevel; i++)
        {
            m_Stars[i].sprite = GeneralSettings.Instance.Sprite.ClearStar;
        }
    }
}
