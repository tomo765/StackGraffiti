using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // �{�^��
    [SerializeField]
    Button[] comandButton;
    // �{�^���������Ƃ���UI�ɕς���
    [SerializeField]
    Sprite[] buttonImage;
    // �{�^�����ɖ߂�
    [SerializeField]
    Sprite[] buttonImageDefault;


    // �{�^���ɃJ�[�\�������킹����
    public void OnpointEnter(int i)
    {
        // �F�ς���
        comandButton[i].image.color = new Color(0.5f, 0.5f, 0.5f, 1);
    }

    // �{�^������}�E�X�J�[�\���𗣂�����
    public void OnpointExit(int i) 
    {
        // �F�߂�
        comandButton[i].image.color = new Color(1, 1, 1, 1);
        // ���̉摜�ɖ߂�
        comandButton[i].image.sprite = buttonImageDefault[i];

    }

    public void OnClick(int i)
    {
        // �{�^��������UI�ɕς���
        comandButton[i].image.sprite = buttonImage[i];
    }
}
