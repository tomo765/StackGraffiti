using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonManager : MonoBehaviour
{
    // �X�^�[�g�{�^������������X�e�[�W�Z���N�g��ʂɍs��
    public void StartButton()
    {
        SceneManager.LoadScene("1_StageSelect");
        // "1_StageSelect"��Unity�ŕt�����X�e�[�W�Z���N�g�p�V�[���̖��O
    }


    // �Q�[���I���{�^������������Q�[�����I��点��X�N���v�g
    public void EndButton()
    {
// Unity�̒��ŃQ�[�����I��������ׂ̃X�N���v�g
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
// �r���h���������Q�[�����I��������ׂ̃X�N���v�g
#else
    Application.Quit();
#endif
    }
}
