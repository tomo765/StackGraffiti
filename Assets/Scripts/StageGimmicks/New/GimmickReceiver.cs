using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GimmickReceiver : MonoBehaviour
{
    /// <summary> ギミックのオンオフを反転 </summary>
    public abstract void ChangeActivate();
}
