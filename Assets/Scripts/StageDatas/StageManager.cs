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
        StageDataUtility.FindData();
    }

    private void Update()
    {
        if (InputExtension.EscapeStage)
        {
            FadeExtension.LoadScene(Config.SceneNames.StageSelect, GeneralSettings.Instance.Sound.SelectSE);
        }

        if (InputExtension.ResetStage)
        {
            if(!GameManager.IsPlaying) { return; }

            GameManager.StartStage(GameManager.CullentStage);
            FadeExtension.LoadScene(gameObject.scene.name, GeneralSettings.Instance.Sound.SelectSE);
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
