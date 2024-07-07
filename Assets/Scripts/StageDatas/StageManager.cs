using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject m_SpawnArea;
    

    void Start()
    {
        GameManager.SetSpawnPos(m_SpawnArea);
        StageDataUtility.LoadData();
    }

    private void Update()
    {
        if (InputExtension.EscapeStage)
        {
            EscapeStage();
        }

        if (InputExtension.ResetStage)
        {
            ResetStage();
        }

        if(InputExtension.ShowHowToPlay)
        {
            PreviewHowTiPlay();
        }
    }

    private async void EscapeStage()
    {
        if (!DontDestroyCanvas.Instance.StageIntroUI.FinishFadeOut) { return; }
        if (SceneLoadExtension.IsFading) { return; }

        SoundManager.Instance.PlayMarimba(0);
        await SceneLoadExtension.StartFadeIn(GeneralSettings.Instance.Sound.Fade1.FadeIn);
        await SceneLoadExtension.StartFadeWait(Config.SceneNames.StageSelect);
        await SceneLoadExtension.StartFadeOut(GeneralSettings.Instance.Sound.Fade1.FadeOut);
    }

    private async void ResetStage()
    {
        if (!DontDestroyCanvas.Instance.StageIntroUI.FinishFadeOut) { return; }
        if (FindFirstObjectByType<GameCanvasUI>().IsInputNameNow) { return; }
        if (SceneLoadExtension.IsFading) { return; }

        GameManager.StartStage(GameManager.CullentStage);
        await SceneLoadExtension.StartFadeIn(GeneralSettings.Instance.Sound.Fade1.FadeIn);
        await SceneLoadExtension.StartFadeWait(gameObject.scene.name);
        await SceneLoadExtension.StartFadeOut(GeneralSettings.Instance.Sound.Fade1.FadeOut);
        SoundManager.Instance.PlayMarimba(0);
    }

    private async void PreviewHowTiPlay()
    {
        if (!DontDestroyCanvas.Instance.StageIntroUI.FinishFadeOut) { return; }
        if (!GameManager.IsPlaying && !GameManager.IsHowToPlay) { return; }
        if (SceneLoadExtension.IsFading) { return; }

        if (GameManager.IsHowToPlay)
        {
            GameManager.SetGameState(GameState.Playing);
            await SceneManager.UnloadSceneAsync(Config.SceneNames.HowToPlay);
        }
        else
        {
            GameManager.SetGameState(GameState.HowToPlay);
            SceneManager.LoadScene(Config.SceneNames.HowToPlay, LoadSceneMode.Additive);
        }
    }
}
