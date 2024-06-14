using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private Image m_Star1;
    [SerializeField] private Image m_Star2;
    [SerializeField] private Image m_Star3;
    private Image[] m_Stars;


    [SerializeField] private Button m_StageSelectBtn;
    [SerializeField] private Button m_ReturnTitleBtn;

    void Start()
    {
        Init();
        SetStar(0);
    }

    private void Init()
    {
        m_StageSelectBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("1_StageSelect");
        });
        m_ReturnTitleBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("0_Title");
        });

        m_Stars = new Image[]
        {
            m_Star1,
            m_Star2,
            m_Star3
        };
    }

    public void SetStar(int level)
    {
        for (int i = 0; i < level; i++)
        {
            m_Stars[i].sprite = GeneralSettings.Instance.Sprite.ClearStar;
        }
        for(int i = m_Stars.Length - 1; i >= level; i--)
        {
            m_Stars[i].sprite = GeneralSettings.Instance.Sprite.UnclearStar;
        }
    }
}