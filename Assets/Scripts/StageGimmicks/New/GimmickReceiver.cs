using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GimmickReceiver : MonoBehaviour  //ToDo : ���₷���������c��
{
    /// <summary> �M�~�b�N�̃I���I�t�𔽓] </summary>
    public abstract void ChangeActivate();

    /// <summary> �M�~�b�N�̃I���I�t���w�� </summary>
    public abstract void SetActivate(bool b);
}
