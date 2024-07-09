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

        GameManager.Source.Cancel();
        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeIn);
        await SceneLoadExtension.StartFadeIn();

        await SceneLoadExtension.StartFadeWait(Config.SceneNames.StageSelect);
        SoundManager.Instance.PlayMarimba(0);

        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeOut);
        await SceneLoadExtension.StartFadeOut();
    }

    private async void ResetStage()
    {
        if (!DontDestroyCanvas.Instance.StageIntroUI.FinishFadeOut) { return; }
        if (FindFirstObjectByType<GameCanvasUI>().IsInputNameNow) { return; }
        if (SceneLoadExtension.IsFading) { return; }

        GameManager.Source.Cancel();
        GameManager.StartStage(GameManager.CullentStage);

        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeIn);
        await SceneLoadExtension.StartFadeIn();
        await SceneLoadExtension.StartFadeWait(gameObject.scene.name);
        SoundManager.Instance.PlayMarimba(0);
        SoundManager.Instance?.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeOut);
        await SceneLoadExtension.StartFadeOut();
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
