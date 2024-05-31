using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class MainManager : MonoBehaviour
{
    [SerializeField] GameObject drawWindow;
    [SerializeField] GameObject lookButton;

    [SerializeField] TMP_Text sleepText;    // �X�R�A�e�L�X�g
    [SerializeField] TMP_Text scoreText;    // �X�R�A�e�L�X�g
    

    public  static int sleepCount = 0;
    public GameObject cleare;

    public AudioSource audioSource; // �I�[�f�B�I�\�[�X
    [SerializeField]
    public AudioClip[] audioClips;  // �I�[�f�B�I�N���b�v

    public string sceneName;    // �V�[���̖��O

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sleepCount = 0;
        // �X�R�A�̍ŏ��̕\��
        sleepText.SetText(string.Format("��������:{0}", sleepCount));
        PlayerControll.gameState = "Drawing";

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerControll.gameState == "Goal")
        {
            cleare.SetActive(true);
        }
        if(PlayerControll.gameState == "Playing")
        {
            // Space�������特��
            if (Input.GetKeyDown(KeyCode.Space))
            {   // �W�����v���ʉ�
                audioSource.PlayOneShot(audioClips[0]);
            }
        }
        // G�L�[�������瓯���V�[���̍ŏ�����
        if(Input.GetKeyDown(KeyCode.G)) 
        {
            SceneManager.LoadScene(sceneName);
        }
        // Esc����������Z���N�g�V�[���ɖ߂�
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("1_StageSelect");
        }


    }

    public void SetActiveTrue()
    {
        //// 3�b��ɃE�B���h�E�o��
        //StartCoroutine(DelayCoroutine(3.0f, () => {
        //    True();
        //}));

            drawWindow.SetActive(true);    // �`���ꏊ�o��
            lookButton.SetActive(true);    // �`���ꏊ�̃{�^�����o��
       
    }

    private void True()
    {
        drawWindow.SetActive(true);    // �`���ꏊ�o��
        lookButton.SetActive(true);    // �`���ꏊ�̃{�^�����o��
    }

    public void SetActiveFalse()
    {
        drawWindow.SetActive(false);    // �`���ꏊ����
        lookButton.SetActive(false);    // �`���ꏊ�̃{�^��������
    }

    public void SleepCount()
    {
        sleepCount += 1;

        // �X�R�A�̍ŏ��̕\��
        sleepText.SetText(string.Format("��������:{0}", sleepCount));
    }

    public void SetClearText()
    {
        scoreText.SetText(string.Format("�������񐔁F{0}", sleepCount));
    }

}
