using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(Config.SceneNames.StageSelect);
        }
    }
}
