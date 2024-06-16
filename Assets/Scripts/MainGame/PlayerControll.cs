using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    Rigidbody2D rigid2D;            
    public float jumpForce = 10.0f;   // ジャンプの高さ
    public bool isJumping = false;      // ジャンプしたかどうか
    public float walkSpeed = 2.0f;    // 移動速度
    private bool playerSleep = false; // プレイヤーが眠っているかどうか
    public bool sleeping = false;    // 二回死んだ判定が出ないようにするためのもの

    // スコア
    [SerializeField] GameObject scoreText;    // スコアテキスト
    public int stageScore = 0;  // ステージのスコア

    // ゲームの状態
    public static string gameState = "Drawing";

    

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false && playerSleep == false) // スペースキー押したら飛ぶ
        {
            // ジャンプ中
            isJumping = true;
            //float originalMass = rigid2D.mass;
            //this.rigid2D.mass = 0f;         // 質量を0にする
            this.rigid2D.velocity = new Vector3(rigid2D.velocity.x, jumpForce);
            //rigid2D.mass = originalMass;    //　飛んだら質量を戻す

        }

        // 眠る
        if (Input.GetKeyDown(KeyCode.E) && playerSleep == false)
        {
            // 眠った回数に追加

            sleeping = true;
            Sleep();

            // PayerタグのついたオブジェクトのOrder in Layerを変更
            HideThePlayer();

            // 目のスプライトを変更する
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("deatheye_2");

        }

    }

    // 物理演算で移動させるのでFixedUpdate使います。
    private void FixedUpdate()
    {
        float horizontalKey = Input.GetAxis("Horizontal");  // HoraizontalはInputManagerの中にある横方向に動かせる関数の名前

        // 右入力で右向きに動く
        if (horizontalKey > 0 && playerSleep == false)
        {
            rigid2D.velocity = new Vector2(walkSpeed, rigid2D.velocity.y);  // 前方向に進む力
        }

        // 左入力で左向きに動く
        if (horizontalKey < 0 && playerSleep == false)
        {
            rigid2D.velocity = new Vector2(-walkSpeed, rigid2D.velocity.y); // 後ろ方向に進む力
        }

    }

    // トリガーに触れたら
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            if(gameState == "Goal") { return; }
            // ゴールした時
            Goal();
            GameManager.StageClear();
        }
        else if(collision.gameObject.tag == "Dead")
        {
            // キャラ死亡
            Dead();
            gameState = "Drawing";
            // PayerタグのついたオブジェクトのOrder in Layerを変更
            HideThePlayer();
        }
        else if(collision.gameObject.tag == "Needle")
        {
            GameManager.AddSleepCount();

            rigid2D.isKinematic = true;
            rigid2D.velocity = new Vector2(0, 0);
            rigid2D.freezeRotation = true; // 回転も停止
            GameObject sleep = GameObject.Find("MainManager");
            sleep.GetComponent<MainManager>().SetActiveTrue();

            if (sleeping == false)
            {
                // 寝た回数カウント入れる
                sleep.GetComponent<MainManager>().SleepCount();
            }

            playerSleep = true;
            gameState = "Drawing";

            // PayerタグのついたオブジェクトのOrder in Layerを変更
            HideThePlayer();

            // 目のスプライトを変更する
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("deatheye_2");
        }
        else if (collision.gameObject.tag == "Sleep")
        {
            Sleep();
            gameState = "Drawing";
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag =="DrawWindow")
        {
            //this.gameObject.SetActive(false);
        }
    }

    // コリジョンに当たったら
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ジャンプ判定
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
        else if (collision.gameObject.tag == "Player")
        {
            isJumping = false;
        }
    }

    public void Goal()
    {
        gameState = "Goal";
        playerSleep = true;
    }

    // キャラ死
    public void Dead()
    {
        GameManager.AddSleepCount();

        // 死んだやつを消す
        Destroy(this.gameObject);

        if(playerSleep == false) 
        {
            GameObject sleep = GameObject.Find("MainManager");
            sleep.GetComponent<MainManager>().SetActiveTrue();
        }
        else
        {
            return;
        }
    }

    public void Sleep()
    {
        GameManager.AddSleepCount();

        GameObject sleep = GameObject.Find("MainManager");
        // 寝た回数カウント入れる
        sleep.GetComponent<MainManager>().SleepCount();
        // 描くエリア表示
        sleep.GetComponent<MainManager>().SetActiveTrue();


        // 眠ったキャラが動かないようにする
        playerSleep = true;
    }

    private void HideThePlayer()
    {
        // PayerタグのついたオブジェクトのOrder in Layerを変更
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<MeshRenderer>().sortingOrder = -1;
            // eyeのOrder in Layerを変更
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
    }

}
