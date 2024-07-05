using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPText = TMPro.TextMeshProUGUI;

public class GameCanvasUI : MonoBehaviour
{
    [SerializeField] TMPText m_SleepCount;
    [SerializeField] DrawUI m_DrawUI;
    [SerializeField] private Button m_LookButton;
    [SerializeField] private TMPText m_LookButtonText;

    private bool m_isVisible = false;

    private const string ViewStage = "Look Stage";
    private const string CreateCharacter = "To Create Character";

    public bool IsInputNameNow => m_DrawUI.IsInputNow;


    void Start()
    {
        Init();
        UpdateSleepText();
    }

    private void Init()
    {
        m_LookButton.onClick.RemoveAllListeners();
        m_LookButton.onClick.AddListener(() =>
        {
            ChangeVisible();
            SetLookBtnText();

            m_DrawUI.gameObject.SetActive(m_isVisible);
            CharacterCreator.SetCreatingCharaVisible(m_isVisible);
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
        });
        m_DrawUI.gameObject.SetActive(m_isVisible);
        SetLookBtnText();

        GameManager.SetUpdateSleepText(UpdateSleepText);
        GameManager.SetRestartDrawing(RestartDrawing);

        GetComponent<Canvas>().planeDistance = GeneralSettings.Instance.Priorities.StageCanvas;
    }

    private void ChangeVisible()
    {
        m_isVisible = !m_isVisible;
    }
    private void SetLookBtnText() => m_LookButtonText.text = m_isVisible ? ViewStage : CreateCharacter;

    private void UpdateSleepText()
    {
        m_SleepCount.text = "Sleep Count : " + GameManager.SleepCount.ToString();
    }

    private void RestartDrawing()
    {
        m_DrawUI.gameObject.SetActive(true);
        m_LookButton.gameObject.SetActive(true);
    }
}
