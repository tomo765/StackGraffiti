using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // ボタン
    [SerializeField]
    Button[] comandButton;
    // ボタン押したときのUIに変える
    [SerializeField]
    Sprite[] buttonImage;
    // ボタン元に戻す
    [SerializeField]
    Sprite[] buttonImageDefault;


    // ボタンにカーソルを合わせたら
    public void OnpointEnter(int i)
    {
        // 色変える
        comandButton[i].image.color = new Color(0.5f, 0.5f, 0.5f, 1);
    }

    // ボタンからマウスカーソルを離したら
    public void OnpointExit(int i) 
    {
        // 色戻す
        comandButton[i].image.color = Color.white;
        // 元の画像に戻す
        comandButton[i].image.sprite = buttonImageDefault[i];

    }

    public void OnClick(int i)
    {
        // ボタン押したUIに変える
        comandButton[i].image.sprite = buttonImage[i];
    }
}
