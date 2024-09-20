using UnityEngine;
using UnityEngine.UI;

/// <summary> ステージセレクト画面のUI全般 </summary>
public class StageSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject m_ViewContent;
    [SerializeField] private Button m_ReturnTitleBtn;
    [SerializeField] private Scrollbar m_VerticalBar;

    private StageSelectButton[] m_SelectButtons;

    void Start()
    {
        StageDataUtility.LoadData();

        SetSelectButtons();
        SetReturnTitleBtn();

        m_VerticalBar.value = 1;
    }

    /// <summary> ステージのセレクトボタンを、ステージの数ぶん生成する </summary>
    private void SetSelectButtons()
    {
        string[] sceneNames = Config.SceneNames.m_StageNames;
        m_SelectButtons = new StageSelectButton[sceneNames.Length];

        for (int i = 0; i < sceneNames.Length; i++)  //ステージのクリアレベルが0でないかを取得する
        {
            bool canSelect = StageDataUtility.IsSelectable(i);
            if(!canSelect) { break; }
            var btn = Instantiate(GeneralSettings.Instance.Prehab.StageSelectBtn, m_ViewContent.transform);
            btn.Init(sceneNames[i], i+1);
            m_SelectButtons[i] = btn;
        }
    }

    /// <summary> タイトルに戻るボタンの処理を登録 </summary>
    private void SetReturnTitleBtn()
    {
        m_ReturnTitleBtn.onClick.RemoveAllListeners();
        m_ReturnTitleBtn.onClick.AddListener(async() =>
        {
            if(SceneLoadExtension.IsFading) { return; }

            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeIn);
            await SceneLoadExtension.StartFadeIn();
            await SceneLoadExtension.FinishFadeIn(Config.SceneNames.Title);
            SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeOut);
            await SceneLoadExtension.FadeOut();
        });
    }
}