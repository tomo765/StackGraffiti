using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManualUI : MonoBehaviour
{
    [SerializeField] private TMPTextLocalizer m_SleepText;
    [SerializeField] private TMPTextLocalizer m_DeleteText;
    [SerializeField] private TMPTextLocalizer m_ResetText;
    [SerializeField] private TMPTextLocalizer m_HowToPlayText;

    void Start()
    {
        InitSleepText();
        InitDeleteText();
        InitResetText();
        InitHowToPlayText();
    }

    private void InitSleepText()
    {
        m_SleepText.SetBackAndForthText("", " : E");
        m_SleepText.SetLocalizeText();
    }
    private void InitDeleteText()
    {
        m_DeleteText.SetBackAndForthText("", " : G");
        m_DeleteText.SetLocalizeText();
    }
    private void InitResetText()
    {
        m_ResetText.SetBackAndForthText("", " : R");
        m_ResetText.SetLocalizeText();
    }
    private void InitHowToPlayText()
    {
        m_HowToPlayText.SetBackAndForthText("", " : Y");
        m_HowToPlayText.SetLocalizeText();
    }
}
