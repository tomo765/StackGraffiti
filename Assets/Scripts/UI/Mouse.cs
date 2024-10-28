using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    // このスクリプトはDrawAreaに付けてます。
    // 画像を入れる場所
    public Texture2D cursor;
    private Vector2 hotspot = new Vector2(10, 4);

    // マウスが重なったらcursorの画像を表示させる
    public void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, hotspot, CursorMode.ForceSoftware);
    }

    // マウスが離れたら元のカーソルに戻す
    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }

    // 参考→https://youtu.be/yjhH70oiEvk?si=9HnkInvDzsmFMlPk
}
