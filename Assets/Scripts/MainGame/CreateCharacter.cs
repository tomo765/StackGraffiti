using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 �����{�^������������`�����L�����N�^�[���X�e�[�W�ɏo�������邽�߂̃X�N���v�g
 */
public class CreateCharacter : MonoBehaviour
{
    private GameObject character;    // �L�����Ăяo��
    public Transform spawnPoints;   // �L�����̃X�|�[������ꏊ
    [SerializeField] GameObject drawWindow;
    [SerializeField] GameObject lookButton;

    [SerializeField] public GameObject eyesPrefab;
    [SerializeField] public GameObject playerPrefab;
    private string layerName = "Ground";

    // ���O
    public TMP_InputField inputField;   // �C���v�b�g�t�B�[���h�W�J
    public GameObject canvasPrefab;       // �L�����o�X�v���t�@�u
    public TMP_Text textPrefab;          // �\������e�L�X�g�I�u�W�F�N�g

    public void OncreateButton()
    {
        PlayerControll.gameState = "Playing";

        character = GameObject.Find("new Object");  // �L�����N�^�[�T���Ă���
        if (character == null )
        { 
            return;
        }
        character.name = "Character";   // �������疼�O��Character�ɂ���
        character.tag = "Player";            // �^�O��Player�ɂ���B
        
        // �ڂ�t����
        Vector3 charaPos = character.transform.position;
        GameObject newEyes = Instantiate(eyesPrefab, Vector3.zero, Quaternion.identity);
        newEyes.transform.SetParent(character.transform);
        newEyes.transform.localPosition =new Vector3(charaPos.x - 3,charaPos.y,charaPos.z);


        Vector3 pos = spawnPoints.transform.position;
        character.transform.position = new Vector3(pos.x + 1,pos.y,pos.z);    // ���X�n�ɔ�΂�

        character.transform.localScale = Vector3.one / 3;   // �`�����L��������������
        character.layer = LayerMask.NameToLayer(layerName);

        // Rigidbody��t����
        var rigibody = character.AddComponent<Rigidbody2D>();   
        rigibody.useAutoMass = true;
        
        // �d��
        rigibody.gravityScale = 3.0f;

        rigibody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // ���C�Ƃ����˕Ԃ�̐ݒ�
        PhysicsMaterial2D material = new PhysicsMaterial2D();
        material.friction = 0f;
        material.bounciness = 0f;

        var controll = character.AddComponent<PlayerControll>();   // �v���C���[�R���g���[���̃X�N���v�g��t����


        GameObject.Find("MainManager");
        GetComponent<MainManager>().SetActiveFalse();

        // Payer�^�O�̂����I�u�W�F�N�g��Order in Layer��ύX
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<MeshRenderer>().sortingOrder = 0;
        }

    }

    public void DisplayTextPlayerHead()
    {
        string inputText = inputField.text; // �C���v�b�g�t�B�[���h������͂��ꂽ��������擾

        if (!string.IsNullOrEmpty(inputText))   // ��������͂����Ƃ�
        {
            //// �e�L�X�g�I�u�W�F�N�g�����݂���ꍇ�A�j������
            //if (textObject != null)
            //{
            //    Destroy(textObject);
            //}

            // �e�L�X�g�R���|�[�l���g���擾���āA���͂��ꂽ�������\������
            textPrefab.text = inputText;
        }
        else // �����Ȃ�
        {
            textPrefab.text = "���ނ�";
        }
        // �e�L�X�g�̃v���n�u����V�����e�L�X�g�I�u�W�F�N�g���쐬���A�v���C���[�̓��ɔz�u����
        Vector3 charaPos = character.transform.position;
        GameObject newNameCanvas = Instantiate(canvasPrefab, Vector3.zero, Quaternion.identity);
        newNameCanvas.transform.SetParent(character.transform);
        newNameCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector3(charaPos.x + 3, charaPos.y + 5, charaPos.z); // �ʒu���킹

        // �L�����o�X�̉��Ƀe�L�X�g������
        var newNameText = Instantiate(textPrefab);
        newNameText.transform.SetParent(newNameCanvas.transform);
        newNameText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0); // �ʒu���킹

    }
}
