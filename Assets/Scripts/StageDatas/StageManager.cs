using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject m_SpawnArea;
    //[SerializeField] private GameObject m_GoalFlag;

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
            GameManager.StartStage(GameManager.CullentStage);
            SceneManager.LoadScene(gameObject.scene.name);
        }
    }
}
