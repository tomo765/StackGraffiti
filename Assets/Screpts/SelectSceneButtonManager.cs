using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneButtonManager : MonoBehaviour
{
    public string sceneName;    // シーンの名前

    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }

}
