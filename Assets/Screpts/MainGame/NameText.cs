using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameText : MonoBehaviour
{
    public Text displayText;

    public void OnEndEdit()
    {
        // プレイヤーが入力した名前を変数に代入
        string inputFieldText = GetComponent<InputField>().text;

        // キャラの頭の上に乗せる用テキストに代入
        displayText.text = inputFieldText;
    }


    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
