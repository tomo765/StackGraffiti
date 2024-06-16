using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class MainManager : MonoBehaviour
{
    [SerializeField] GameObject drawWindow;
    [SerializeField] GameObject lookButton;

    [SerializeField] TMP_Text sleepText;    // スコアテキスト
    [SerializeField] TMP_Text scoreText;    // スコアテキスト
    

    public  static int sleepCount = 0;
    public GameObject cleare;

    public AudioSource audioSource; // オーディオソース
    [SerializeField]
    public AudioClip[] audioClips;  // オーディオクリップ

    public string sceneName;    // シーンの名前

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sleepCount = 0;
        // スコアの最初の表示
        sleepText.SetText(string.Format("眠った回数:{0}", sleepCount));
        PlayerControll.gameState = "Drawing";
        GameManager.SetGameState(GameState.Drawing);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerControll.gameState == "Playing")
        {
            // Space押したら音鳴る
            if (Input.GetKeyDown(KeyCode.Space))
            {   // ジャンプ効果音
                audioSource.PlayOneShot(audioClips[0]);
            }
        }
        // Gキー押したら同じシーンの最初から
        if(Input.GetKeyDown(KeyCode.G)) 
        {
            SceneManager.LoadScene(sceneName);
        }
        // Escを押したらセレクトシーンに戻る
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(Config.SceneNames.StageSelect);
        }


    }

    public void SetActiveTrue()
    {
        drawWindow.SetActive(true);    // 描く場所出す
        lookButton.SetActive(true);    // 描く場所のボタンも出す
    }

    private void True()
    {
        drawWindow.SetActive(true);    // 描く場所出す
        lookButton.SetActive(true);    // 描く場所のボタンも出す
    }

    public void SetActiveFalse()
    {
        drawWindow.SetActive(false);    // 描く場所消す
        lookButton.SetActive(false);    // 描く場所のボタンも消す
    }

    public void SleepCount()
    {
        sleepCount += 1;

        // スコアの最初の表示
        sleepText.SetText(string.Format("眠った回数:{0}", sleepCount));
    }

    public void SetClearText()
    {
        scoreText.SetText(string.Format("眠った回数：{0}", sleepCount));
    }

}
