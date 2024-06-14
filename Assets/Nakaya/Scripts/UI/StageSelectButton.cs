using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class StageSelectButton : MonoBehaviour
{

    [SerializeField] private Button m_TransitionButton;
    [SerializeField] private TextMeshProUGUI m_StageLevelText;
    [SerializeField] private Image[] m_Stars;

    private string m_StageName;
    private int m_StageLevel;
    public string StageName => m_StageName;

    public void Init(string name, int stageLevel)
    {
        m_StageName = name;
        m_StageLevel = stageLevel;
        m_StageLevelText.text = "ステージ" + stageLevel.ToString();
        m_TransitionButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(m_StageName);
            GameManager.SetCullentStage((StageState)stageLevel);
            SoundManager.Instance?.PlayNewBGM(GeneralSettings.Instance.Sound.SelectSE);  //ToDo : サウンド変更
        });

        for (int i = 0; i < GeneralSettings.Instance.StageEval.StageScores[(StageState)stageLevel].CullentEval; i++)
        {
            m_Stars[i].sprite = GeneralSettings.Instance.Sprite.ClearStar;
        }
    }
}
