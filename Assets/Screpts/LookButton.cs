using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LookButton : MonoBehaviour
{
    public GameObject drawWindow;   // UnityでDrawWindowを入れる場所
    public Button lookButton;    // UnityでLookButtonを入れる場所
    public TMP_Text buttonText;     // テキストの内容を切り替える

    void Start()
    {   // 最初はキャラを描くウィンドウを表示させておく
        bool isActive = false;
        
        // ボタンをクリックしたらウィンドウの表示・非表示を切り替える
        lookButton.onClick.AddListener(() =>
        {
            isActive = !isActive;
            drawWindow.SetActive(isActive);
            buttonText.text = (buttonText.text == "ステージを見る") ? "キャラクターを作る" : "ステージを見る";

        });  

    }
}
