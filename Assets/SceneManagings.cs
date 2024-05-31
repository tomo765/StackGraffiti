using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagings : MonoBehaviour
{
    //[SerializeField] private Button returnButton;
    [SerializeField] private Button loadOption;

    private bool loadedOption = false;

    void Start()
    {
        //returnButton.onClick.AddListener(() =>
        //{       
        //    loadedOption = false;
        //    SceneManager.UnloadSceneAsync("OptionScene");
        //});

        loadOption.onClick.AddListener(() =>
        {
            if(loadedOption) { return; }
            loadedOption = true;
            SceneManager.LoadScene("OptionScene", LoadSceneMode.Additive);
        });
    }

    public void SetOptionState()
    {
        loadedOption = false;
    }
}
