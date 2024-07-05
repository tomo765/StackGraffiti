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

    private async void Update()
    {
        if (InputExtension.EscapeStage)
        {
            await SceneLoadExtension.LoadWithFade(Config.SceneNames.StageSelect, GeneralSettings.Instance.Sound.FadeSE);
            SoundManager.Instance.PlayMarimba(0);
        }

        if (InputExtension.ResetStage)
        {
            if(FindFirstObjectByType<GameCanvasUI>().IsInputNameNow) { return; }

            GameManager.StartStage(GameManager.CullentStage);
            await SceneLoadExtension.LoadWithFade(gameObject.scene.name, GeneralSettings.Instance.Sound.FadeSE);
            SoundManager.Instance.PlayMarimba(0);
        }

        if(InputExtension.ShowHowToPlay)
        {
            if(!GameManager.IsPlaying && !GameManager.IsHowToPlay) { return; }

            if(GameManager.IsHowToPlay) 
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
}
