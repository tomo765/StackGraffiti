using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonManager : MonoBehaviour
{
    // スタートボタンを押したらステージセレクト画面に行く
    public void StartButton()
    {
        SceneManager.LoadScene("1_StageSelect");
        // "1_StageSelect"はUnityで付けたステージセレクト用シーンの名前
    }


    // ゲーム終了ボタンを押したらゲームを終わらせるスクリプト
    public void EndButton()
    {
// Unityの中でゲームを終了させる為のスクリプト
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
// ビルドしたやつからゲームを終了させる為のスクリプト
#else
    Application.Quit();
#endif
    }
}
