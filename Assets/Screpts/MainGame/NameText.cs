using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameText : MonoBehaviour
{
    public Text displayText;

    public void OnEndEdit()
    {
        // �v���C���[�����͂������O��ϐ��ɑ��
        string inputFieldText = GetComponent<InputField>().text;

        // �L�����̓��̏�ɏ悹��p�e�L�X�g�ɑ��
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
