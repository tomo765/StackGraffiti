using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TInputField = TMPro.TMP_InputField;
using Unity.VisualScripting;
using UnityEngine.EventSystems;


public class DrawUI : MonoBehaviour  //Ques : キャラを生成したら名前インプットを消す?
{
    [SerializeField] private Button m_CreateButton;
    [SerializeField] private Button m_RenameButton;
    [SerializeField] private Button m_ReturnButton;

    [SerializeField] private TInputField m_NameInput;
    [SerializeField] private CharacterDraw m_DrawArea;

    public bool IsInputNow => m_NameInput.isFocused;

    private Button m_ViewDrawUIBtn;

    public void Init(GameCanvasUI gameCanvasUI, Button viewDrawUIBtn)
    {
        m_ViewDrawUIBtn = viewDrawUIBtn;

        m_CreateButton.onClick.RemoveAllListeners();
        m_CreateButton.onClick.AddListener(() =>
        {
            if (!CharacterCreator.CanCreateChara) { return; }
            CharacterCreator.CreateOnStage(m_NameInput.text);

            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
            gameObject.SetActive(false);
            m_ViewDrawUIBtn.gameObject.SetActive(false);

        });

        m_RenameButton.onClick.RemoveAllListeners();
        m_RenameButton.onClick.AddListener(() =>
        {
            m_NameInput.text = GeneralSettings.Instance.PlayerSetting.GetRandomName();
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.SelectSE);
        });


        m_ReturnButton.onClick.RemoveAllListeners();
        m_ReturnButton.onClick.AddListener(async() =>
        {
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeIn);
            await SceneLoadExtension.StartFadeIn();
            EffectContainer.Instance.StopAllEffect();
            await SceneLoadExtension.StartFadeWait(Config.SceneNames.StageSelect);
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeOut);
            await SceneLoadExtension.StartFadeOut();
        });
    }
}