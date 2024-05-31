using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneButtonManager : MonoBehaviour
{
    public string sceneName;    // ƒV[ƒ“‚Ì–¼‘O

    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }

}
