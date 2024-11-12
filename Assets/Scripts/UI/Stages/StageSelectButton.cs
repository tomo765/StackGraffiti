using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary> ステージ選択ボタンの一般クラス </summary>
public class StageSelectButton : MonoBehaviour
{
    [SerializeField] private Button m_TransitionButton;  //アタッチされえているオブジェクトのボタン
    [SerializeField] private TextMeshProUGUI m_StageLevelText;
    [SerializeField] private Image[] m_Stars;

    private string m_StageName;  //ステージのシーン名
    private int m_StageLevel;

    public void Init(string name, int stageNum)
    {
        m_StageName = name;
        m_StageLevel = stageNum;

        var stageText = m_StageLevelText.GetComponent<TMPTextLocalizer>().GetLocalizeText() + " " + stageNum.ToString();
        m_StageLevelText.text = stageText;

        m_TransitionButton.onClick.AddListener(async () =>
        {
            GameManager.InitPlayState((StageLevel)stageNum);

            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeIn);
            await SceneLoadExtension.StartFadeIn();
            await SceneLoadExtension.FinishFadeIn(m_StageName);
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeOut);
            await SceneLoadExtension.FadeOut();

            DontDestroyCanvas.Instance.ChangeStageIntroUIVisible(true);
            DontDestroyCanvas.Instance.StageIntroUI.SetIntroText(m_StageLevel, 
                                                                 GeneralSettings.Instance.StageInfos.GetStageTextEN(stageNum),
                                                                 GeneralSettings.Instance.StageInfos.GetStageTextJP(stageNum));
        });

        for (int i = 0; i < StageDataUtility.StageDatas.StageScores[stageNum - 1].StarLevel; i++)
        {
            m_Stars[i].sprite = GeneralSettings.Instance.Sprite.ClearStar;
        }
    }
}
