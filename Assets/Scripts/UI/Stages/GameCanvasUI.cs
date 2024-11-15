using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPText = TMPro.TextMeshProUGUI;

/// <summary> キャラを操作するステージのUI全般 </summary>
public class GameCanvasUI : MonoBehaviour
{
    [SerializeField] TMPText m_SleepCount;
    [SerializeField] DrawUI m_DrawUI;
    [SerializeField] private Button m_ViewStageBtn;
    [SerializeField] private Button m_ViewDrawUIBtn;

    private bool m_isVisible = false;

    public bool IsInputNameNow => m_DrawUI.IsInputNow;


    void Start()
    {
        SetViewStageButton();
        SetViewDrawUIButton();

        Init();
        m_DrawUI.Init(m_ViewDrawUIBtn);
        UpdateSleepText();
    }

    /// <summary> キャラを書くUIを非表示にするボタンを設定 </summary>
    private void SetViewStageButton()
    {
        m_ViewStageBtn.onClick.RemoveAllListeners();
        m_ViewStageBtn.onClick.AddListener(() =>
        {
            ChangeVisible();

            m_DrawUI.gameObject.SetActive(m_isVisible);
            m_ViewDrawUIBtn.gameObject.SetActive(!m_isVisible);
            CharacterCreator.SetCharacterTransform(GameManager.SpawnArea.transform.position, new Vector2(0.3f, 0.3f));
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
        });
    }

    /// <summary> キャラを書くUIを表示するボタンの設定 </summary>
    private void SetViewDrawUIButton()
    {
        m_ViewDrawUIBtn.onClick.RemoveAllListeners();
        m_ViewDrawUIBtn.onClick.AddListener(() =>
        {
            ChangeVisible();

            m_DrawUI.gameObject.SetActive(m_isVisible);
            m_ViewDrawUIBtn.gameObject.SetActive(!m_isVisible);

            Vector3 pos = m_DrawUI.DrawAreaPosition;
            pos.z = GeneralSettings.Instance.Priorities.CreateCharaLayer.z;
            CharacterCreator.SetCharacterTransform(pos, Vector3.one);
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
        });
    }

    private void Init()
    {
        m_DrawUI.gameObject.SetActive(m_isVisible);
        m_ViewDrawUIBtn.gameObject.SetActive(!m_isVisible);

        GameManager.SetUpdateSleepText(UpdateSleepText);
        GameManager.SetRestartDrawing(RestartDrawing);
        GetComponent<Canvas>().planeDistance = GeneralSettings.Instance.Priorities.StageCanvas;
    }

    /// <summary> 表示状態の切り替え </summary>
    private void ChangeVisible()
    {
        m_isVisible = !m_isVisible;
    }

    /// <summary> 現在の睡眠回数を表示 </summary>
    private void UpdateSleepText()
    {
        m_SleepCount.text = ": " + GameManager.SleepCount.ToString();
    }

    /// <summary> 再度キャラを描き始める時の処理 </summary>
    private void RestartDrawing()
    {
        m_DrawUI.gameObject.SetActive(true);
    }
}
