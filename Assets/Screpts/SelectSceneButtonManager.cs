using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneButtonManager : MonoBehaviour
{
    public string sceneName;    // �V�[���̖��O

    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }

}
