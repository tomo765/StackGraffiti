using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 生成ボタンを押したら描いたキャラクターをステージに出現させるためのスクリプト
 */
public class CreateCharacter : MonoBehaviour
{
    private GameObject character;    // キャラ呼び出し
    public Transform spawnPoints;   // キャラのスポーンする場所
    [SerializeField] GameObject drawWindow;
    [SerializeField] GameObject lookButton;

    [SerializeField] public GameObject eyesPrefab;
    [SerializeField] public GameObject playerPrefab;
    private string layerName = "Ground";

    // 名前
    public TMP_InputField inputField;   // インプットフィールド展開
    public GameObject canvasPrefab;       // キャンバスプレファブ
    public TMP_Text textPrefab;          // 表示するテキストオブジェクト

    public void OncreateButton()
    {
        GameManager.SetGameState(GameState.Playing);

        character = GameObject.Find("new Object");  // キャラクター探してくる
        if (character == null )
        { 
            return;
        }
        character.name = "Character";   // おったら名前をCharacterにする
        character.tag = "Player";            // タグをPlayerにする。
        
        // 目を付ける
        Vector3 charaPos = character.transform.position;
        GameObject newEyes = Instantiate(eyesPrefab, Vector3.zero, Quaternion.identity);
        newEyes.transform.SetParent(character.transform);
        newEyes.transform.localPosition =new Vector3(charaPos.x - 3,charaPos.y,charaPos.z);


        Vector3 pos = spawnPoints.transform.position;
        character.transform.position = new Vector3(pos.x + 1,pos.y,pos.z);    // リス地に飛ばす

        character.transform.localScale = Vector3.one / 3;   // 描いたキャラ小さくする
        character.layer = LayerMask.NameToLayer(layerName);

        // Rigidbodyを付ける
        var rigibody = character.AddComponent<Rigidbody2D>();   
        rigibody.useAutoMass = true;
        
        // 重力
        rigibody.gravityScale = 3.0f;

        rigibody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // 摩擦とか跳ね返りの設定
        PhysicsMaterial2D material = new PhysicsMaterial2D();
        material.friction = 0f;
        material.bounciness = 0f;

        var controll = character.AddComponent<PlayerControll>();   // プレイヤーコントロールのスクリプトを付ける


        GameObject.Find("MainManager");
        GetComponent<MainManager>().SetActiveFalse();

        // PayerタグのついたオブジェクトのOrder in Layerを変更
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<MeshRenderer>().sortingOrder = 0;
        }

    }

    public void DisplayTextPlayerHead()
    {
        string inputText = inputField.text; // インプットフィールドから入力された文字列を取得

        if (!string.IsNullOrEmpty(inputText))   // 文字を入力したとき
        {
            //// テキストオブジェクトが存在する場合、破棄する
            //if (textObject != null)
            //{
            //    Destroy(textObject);
            //}

            // テキストコンポーネントを取得して、入力された文字列を表示する
            textPrefab.text = inputText;
        }
        else // 文字なし
        {
            textPrefab.text = "きむち";
        }
        // テキストのプレハブから新しいテキストオブジェクトを作成し、プレイヤーの頭に配置する
        Vector3 charaPos = character.transform.position;
        GameObject newNameCanvas = Instantiate(canvasPrefab, Vector3.zero, Quaternion.identity);
        newNameCanvas.transform.SetParent(character.transform);
        newNameCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector3(charaPos.x + 3, charaPos.y + 5, charaPos.z); // 位置合わせ

        // キャンバスの下にテキストを入れる
        var newNameText = Instantiate(textPrefab);
        newNameText.transform.SetParent(newNameCanvas.transform);
        newNameText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0); // 位置合わせ

    }
}
