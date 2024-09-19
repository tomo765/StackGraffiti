using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary> �X�e�[�W�̃��x�� </summary>
public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject m_SpawnArea;
    [SerializeField] private GameCanvasUI m_GameCanvasUI;

    void Start()
    {
        GameManager.SetSpawnPos(m_SpawnArea);
        DontDestroyCanvas.Instance.ResetStageUI.SetInputName(() => m_GameCanvasUI.IsInputNameNow);
        DontDestroyCanvas.Instance.ResetStageUI.SetStageName(gameObject.scene.name);

        StageDataUtility.LoadData();
    }

    private void Update()
    {
        if (InputExtension.Escape)
        {
            EscapeStage().FireAndForget();
        }

        if (InputExtension.ResetStage)
        {
            if (m_GameCanvasUI.IsInputNameNow) { return; }
            DontDestroyCanvas.Instance.ChangeResetStageUIVisible(true);
            //ResetStage().FireAndForget();
        }
    }

    /// <summary> �X�e�[�W�̃v���C���I�����A�X�e�[�W�I����ʂֈړ����� </summary>
    private async Task EscapeStage()
    {
        if (!DontDestroyCanvas.Instance.StageIntroUI.FinishFadeOut) { return; }
        if (GameManager.IsClear) { return; }
        if (SceneLoadExtension.IsFading) { return; }

        GameManager.Source.Cancel();
        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeIn);
        await SceneLoadExtension.StartFadeIn();
        EffectContainer.Instance.StopAllEffect();
        await SceneLoadExtension.FinishFadeIn(Config.SceneNames.StageSelect);
        SoundManager.Instance.PlayMarimba(0);
        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeOut);
        await SceneLoadExtension.FadeOut();
    }

    /// <summary> �X�e�[�W���ŏ������蒼�� </summary>
    private async Task ResetStage()
    {
        if (!DontDestroyCanvas.Instance.StageIntroUI.FinishFadeOut) { return; }
        if (m_GameCanvasUI.IsInputNameNow) { return; }
        if (GameManager.IsClear) { return; }
        if (SceneLoadExtension.IsFading) { return; }

        GameManager.Source.Cancel();
        GameManager.InitPlayState(GameManager.CullentStage);

        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeIn);
        await SceneLoadExtension.StartFadeIn();
        EffectContainer.Instance.StopAllEffect();
        await SceneLoadExtension.FinishFadeIn(gameObject.scene.name);
        SoundManager.Instance.PlayMarimba(0);
        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeOut);
        await SceneLoadExtension.FadeOut();
    }
}
