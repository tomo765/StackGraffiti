using UnityEngine;

// �{�^���Ńp�l���\���E��\����؂�ւ���X�N���v�g

public class Panel : MonoBehaviour
{
    [SerializeField]
    GameObject[] panels;
   
    public void ChangePanel(GameObject panel)
    {
        for (int i = panels.Length - 1; i >= 0; i--)
        {
            if (panels[i] == panel)
            {
                panels[i].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
            }
        }
    }
}
