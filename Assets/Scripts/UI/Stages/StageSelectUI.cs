using UnityEngine;
using UnityEngine.UI;

/// <summary> �X�e�[�W�Z���N�g��ʂ�UI�S�� </summary>
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

    /// <summary> �X�e�[�W�̃Z���N�g�{�^�����A�X�e�[�W�̐��Ԃ񐶐����� </summary>
    private void SetSelectButtons()
    {
        string[] sceneNames = Config.SceneNames.m_StageNames;
        m_SelectButtons = new StageSelectButton[sceneNames.Length];

        for (int i = 0; i < sceneNames.Length; i++)  //�X�e�[�W�̃N���A���x����0�łȂ������擾����
        {
            bool canSelect = StageDataUtility.IsSelectable(i);
            if(!canSelect) { break; }
            var btn = Instantiate(GeneralSettings.Instance.Prehab.StageSelectBtn, m_ViewContent.transform);
            btn.Init(sceneNames[i], i+1);
            m_SelectButtons[i] = btn;
        }
    }

    /// <summary> �^�C�g���ɖ߂�{�^���̏�����o�^ </summary>
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