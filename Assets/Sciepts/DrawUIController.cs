using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawUIController : MonoBehaviour
{
    public GameObject drawWindow;   // Unity��DrawWindow������ꏊ
    public Button lookButton;    // Unity��LookButton������ꏊ
    public TMP_Text buttonText;     // �e�L�X�g�̓��e��؂�ւ���

    private const string LookStage = "�X�e�[�W������";
    private const string CreateCharacter = "�L�����N�^�[�����";

    private bool m_IsActive = false;

    //void Start()
    //{
    //    // �{�^�����N���b�N������E�B���h�E�̕\���E��\����؂�ւ���
    //    lookButton.onClick.AddListener(() =>
    //    {
    //        m_IsActive = !m_IsActive;
    //        drawWindow.SetActive(m_IsActive);
    //        buttonText.text = m_IsActive ? LookStage : CreateCharacter;
    //    });
    //}
}
