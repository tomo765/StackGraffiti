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
            SceneLoadExtension.LoadWithFade(Config.SceneNames.StageSelect, GeneralSettings.Instance.Sound.FadeSE);
        }

        if (InputExtension.ResetStage)
        {
            if(FindFirstObjectByType<GameCanvasUI>().IsInputNameNow) { return; }

            GameManager.StartStage(GameManager.CullentStage);
            SceneLoadExtension.LoadWithFade(gameObject.scene.name, GeneralSettings.Instance.Sound.FadeSE);
        }

        if(InputExtension.ShowHowToPlay)
        {
            if(!GameManager.IsPlaying && !GameManager.IsHowToPlay) { return; }

            if(GameManager.IsHowToPlay) 
            {
                GameManager.SetGameState(GameState.Playing);
                SceneManager.UnloadSceneAsync(Config.SceneNames.HowToPlay);
            }
            else
            {
                GameManager.SetGameState(GameState.HowToPlay);
                SceneManager.LoadScene(Config.SceneNames.HowToPlay, LoadSceneMode.Additive);
            }
        }
    }
}
