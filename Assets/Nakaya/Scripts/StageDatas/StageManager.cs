using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] Transform m_CharacterSpawnPos;
    void Start()
    {
        GameManager.SetSpawnPos(m_CharacterSpawnPos.position);
    }
}
