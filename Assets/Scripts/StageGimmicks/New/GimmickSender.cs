using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GimmickSender : MonoBehaviour
{
    public abstract void StartGimmick(Collider2D col);
    public abstract void StopGimmick(Collider2D col);
}
