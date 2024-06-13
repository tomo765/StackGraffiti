using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawUIController : MonoBehaviour
{
    public GameObject drawWindow;   // UnityでDrawWindowを入れる場所
    public Button lookButton;    // UnityでLookButtonを入れる場所
    public TMP_Text buttonText;     // テキストの内容を切り替える

    private const string LookStage = "ステージを見る";
    private const string CreateCharacter = "キャラクターを作る";

    private bool m_IsActive = false;

    //void Start()
    //{
    //    // ボタンをクリックしたらウィンドウの表示・非表示を切り替える
    //    lookButton.onClick.AddListener(() =>
    //    {
    //        m_IsActive = !m_IsActive;
    //        drawWindow.SetActive(m_IsActive);
    //        buttonText.text = m_IsActive ? LookStage : CreateCharacter;
    //    });
    //}
}
