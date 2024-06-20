using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GimmickReceiver : MonoBehaviour  //ToDo : やりやすい方だけ残す
{
    /// <summary> ギミックのオンオフを反転 </summary>
    public abstract void ChangeActivate();

    /// <summary> ギミックのオンオフを指定 </summary>
    public abstract void SetActivate(bool b);
}
