using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    private static bool initialized = false;

    void Awake()
    {
        if(initialized) { return; }

        if(DontDestroyCanvas.Instance == null)
        {
            DontDestroyCanvas.CreateCanvas().CreateAllUI();
        }

        initialized = true;
    }
}
