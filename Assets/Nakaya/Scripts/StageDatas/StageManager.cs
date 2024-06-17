using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] Transform m_CharacterSpawnPos;
    //[SerializeField] private GameObject m_GoalFlag;

    void Start()
    {
        GameManager.SetSpawnPos(m_CharacterSpawnPos.position);
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
