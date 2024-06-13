using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPText = TMPro.TextMeshProUGUI;

public class GameCanvasUI : MonoBehaviour
{
    [SerializeField] TMPText m_SleepCount;
    [SerializeField] DrawUI m_DrawUI;
    [SerializeField] private Button m_LookButton;
    [SerializeField] private TMPText m_LookButtonText;

    private bool m_isVisible = true;

    private const string ViewStage = "ステージを見る";
    private const string CreateCharacter = "キャラクターを作る";


    void Start()
    {
        Init();
    }

    private void Init()
    {
        m_LookButton.onClick.RemoveAllListeners();
        m_LookButton.onClick.AddListener(() =>
        {
            ChangeVisible();
            m_DrawUI.gameObject.SetActive(m_isVisible);
            m_LookButtonText.text = m_isVisible ? CreateCharacter : ViewStage;
        });
    }
    private void ChangeVisible()
    {
        m_isVisible = !m_isVisible;
    }



    void Update()
    {
        
    }

    private void UpdateSleepCount()
    {
        m_SleepCount.text = "1";  //ToDo : 引数で持ってくるか static で管理している物を引っ張ってくる
    }
}
