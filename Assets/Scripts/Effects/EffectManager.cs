using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour  //ToDo : ������g��Ȃ�
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)/* && FadeCanvasUI.Instance.OnFade*/)
        {
            EffectContainer.Instance.PlayEffect(GeneralSettings.Instance.Prehab.ClickEffect, InputExtension.WorldMousePos);
        }
    }
}