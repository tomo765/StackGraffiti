using UnityEngine;

// �Q�[���̏��
public enum GameState
{
    Playing, 
    Drawing,
    Goal
}

public class PlayerControll : MonoBehaviour
{
    Rigidbody2D rigid2D;            
    public float jumpForce = 10.0f;   // �W�����v�̍���
    public bool isJumping = false;      // �W�����v�������ǂ���
    public float walkSpeed = 2.0f;    // �ړ����x
    private bool playerSleep = false; // �v���C���[�������Ă��邩�ǂ���
    public bool sleeping = false;    // ��񎀂񂾔��肪�o�Ȃ��悤�ɂ��邽�߂̂���

    // �X�R�A
    [SerializeField] GameObject scoreText;    // �X�R�A�e�L�X�g
    public int stageScore = 0;  // �X�e�[�W�̃X�R�A

    // �Q�[���̏��
    public static string gameState = "Drawing";

    

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // �W�����v
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false && playerSleep == false) // �X�y�[�X�L�[����������
        {
            // �W�����v��
            isJumping = true;
            float originalMass = rigid2D.mass;
            this.rigid2D.mass = 0f;         // ���ʂ�0�ɂ���
            this.rigid2D.velocity = new Vector3(rigid2D.velocity.x, jumpForce);
            rigid2D.mass = originalMass;    //�@��񂾂玿�ʂ�߂�

        }

        // ����
        if (Input.GetKeyDown(KeyCode.E) && playerSleep == false)
        {
            // �������񐔂ɒǉ�

            sleeping = true;
            Sleep();

            // Payer�^�O�̂����I�u�W�F�N�g��Order in Layer��ύX
            HideThePlayer();

            // �ڂ̃X�v���C�g��ύX����
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("deatheye_2");

        }

    }

    // �������Z�ňړ�������̂�FixedUpdate�g���܂��B
    private void FixedUpdate()
    {
        float horizontalKey = Input.GetAxis("Horizontal");  // Horaizontal��InputManager�̒��ɂ��鉡�����ɓ�������֐��̖��O

        // �E���͂ŉE�����ɓ���
        if (horizontalKey > 0 && playerSleep == false)
        {
            rigid2D.velocity = new Vector2(walkSpeed, rigid2D.velocity.y);  // �O�����ɐi�ޗ�
        }

        // �����͂ō������ɓ���
        if (horizontalKey < 0 && playerSleep == false)
        {
            rigid2D.velocity = new Vector2(-walkSpeed, rigid2D.velocity.y); // �������ɐi�ޗ�
        }

    }

    // �g���K�[�ɐG�ꂽ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            // �S�[��������
            Goal();
            FindObjectOfType<MainManager>().SetClearText();
        }
        else if(collision.gameObject.tag == "Dead")
        {
            // �L�������S
            Dead();
            gameState = "Drawing";
            // Payer�^�O�̂����I�u�W�F�N�g��Order in Layer��ύX
            HideThePlayer();
        }
        else if(collision.gameObject.tag == "Needle")
        {
            rigid2D.isKinematic = true;
            rigid2D.velocity = new Vector2(0, 0);
            rigid2D.freezeRotation = true; // ��]����~
            GameObject sleep = GameObject.Find("MainManager");
            sleep.GetComponent<MainManager>().SetActiveTrue();

            if (sleeping == false)
            {
                // �Q���񐔃J�E���g�����
                sleep.GetComponent<MainManager>().SleepCount();
            }

            playerSleep = true;
            gameState = "Drawing";

            // Payer�^�O�̂����I�u�W�F�N�g��Order in Layer��ύX
            HideThePlayer();

            // �ڂ̃X�v���C�g��ύX����
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

    // �R���W�����ɓ���������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �W�����v����
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

    // �L������
    public void Dead()
    {
        // ���񂾂������
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
        GameObject sleep = GameObject.Find("MainManager");
        // �Q���񐔃J�E���g�����
        sleep.GetComponent<MainManager>().SleepCount();
        // �`���G���A�\��
        sleep.GetComponent<MainManager>().SetActiveTrue();


        // �������L�����������Ȃ��悤�ɂ���
        playerSleep = true;
    }

    private void HideThePlayer()
    {
        // Payer�^�O�̂����I�u�W�F�N�g��Order in Layer��ύX
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<MeshRenderer>().sortingOrder = -1;
            // eye��Order in Layer��ύX
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
    }

}
