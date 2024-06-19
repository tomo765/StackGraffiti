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
            SceneManager.LoadScene(Config.SceneNames.StageSelect);
        }

        if (InputExtension.ResetStage)
        {
            if(GameManager.IsDrawing || GameManager.IsClear) { return; }

            GameManager.StartStage(GameManager.CullentStage);
            SceneManager.LoadScene(gameObject.scene.name);
        }
    }
}
