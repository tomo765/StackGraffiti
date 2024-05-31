using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LookButton : MonoBehaviour
{
    public GameObject drawWindow;   // Unity��DrawWindow������ꏊ
    public Button lookButton;    // Unity��LookButton������ꏊ
    public TMP_Text buttonText;     // �e�L�X�g�̓��e��؂�ւ���

    void Start()
    {   // �ŏ��̓L������`���E�B���h�E��\�������Ă���
        bool isActive = false;
        
        // �{�^�����N���b�N������E�B���h�E�̕\���E��\����؂�ւ���
        lookButton.onClick.AddListener(() =>
        {
            isActive = !isActive;
            drawWindow.SetActive(isActive);
            buttonText.text = (buttonText.text == "�X�e�[�W������") ? "�L�����N�^�[�����" : "�X�e�[�W������";

        });  

    }
}
