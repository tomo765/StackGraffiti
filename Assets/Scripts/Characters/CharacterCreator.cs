using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> キャラの生成に関する設定や処理 </summary>
public static class CharacterCreator  //参考サイト : https://qiita.com/divideby_zero/items/491d18cbc91d7fabd700
{
    private static float m_LineWidth = 0.1f;
    private static Mesh m_Mesh;
    private static List<Vector2> m_Vlist = new List<Vector2>();

    private static CharacterManager m_CreateChara;

    public static bool CanCreateChara => m_CreateChara != null && m_Mesh.vertexCount >= 4;

    /// <summary> キャラを書くUIの表示状態の変更と同時に、書いているキャラの表示状態も変更させる </summary>
    public static void SetCreatingCharaVisible(bool b)
    {
        if(m_CreateChara == null) { return; }
        m_CreateChara.gameObject.SetActive(b);
    }
    public static void SetCreateCharaTransform(Vector3 pos, Vector3 size)
    {
        if(!CanCreateChara) { return; }
        m_CreateChara.transform.position = pos;
        m_CreateChara.transform.localScale = size;
    }
    /// <summary> キャラを書ける状態でマウス左をクリックした瞬間の処理 </summary>
    public static void OnClick(Vector2 position)
    {
        CreateCharacter(position);

        m_Mesh = new Mesh();
        m_CreateChara.MeshFilter.mesh = m_Mesh;

        m_Vlist.Clear();
    }

    /// <summary> キャラを書ける状態でマウス左をホールドしてる時の処理 </summary>
    public static void OnHold(Vector2 position)
    {
        var pos = InputExtension.WorldMousePos - position;
        m_Vlist.Add(pos);

        CreateMesh();
    }

    /// <summary> キャラを書いているときにマウス左を話した時の処理 </summary>
    public static void OnRelease()
    {
        FinishWrite();
    }

    /// <summary> キャラの生成処理 </summary>
    private static void CreateCharacter(Vector3 position)
    {
        if (m_CreateChara != null) { return; }
        position += GeneralSettings.Instance.Priorities.CreateCharaLayer;

        m_CreateChara = MonoBehaviour.Instantiate(GeneralSettings.Instance.Prehab.Character,
                                      position,
                                      Quaternion.identity);
    }
    /// <summary> キャラを操作可能にするための処理 </summary>
    public static void CreateOnStage(string charaName)
    {
        if (!CanCreateChara) { return; }

        if (string.IsNullOrEmpty(charaName)) 
        { 
            charaName = GeneralSettings.Instance.PlayerSetting.GetRandomName(); 
        }

        GameManager.SetGameState(GameState.Playing);
        var charaNameCanvas = GameObject.Instantiate(GeneralSettings.Instance.Prehab.CharacterNameCanvas);
        charaNameCanvas.SetCharacterName(charaName);
        m_CreateChara.StartOperation(charaNameCanvas);

        m_CreateChara = null;
    }

    /// <summary> キャラを書き終わった時の処理 </summary>
    private static void FinishWrite()
    {
        if (!CanCreateChara) { return; }

        m_CreateChara.Rb2D.useAutoMass = true;
        var polyColliderPos = CreateMeshToPolyCollider(m_Mesh);
        m_CreateChara.Poly2D.SetPath(0, polyColliderPos.ToArray());
    }

    /// <summary> キャラの見た目を更新する </summary>
    private static void CreateMesh()
    {
        m_Mesh.Clear();

        var vCnt = m_Vlist.Count;
        var vertices = new List<Vector3>();
        for (int i = 0; i < vCnt - 1; i++)
        {
            var currentPos = m_Vlist[i];
            var nextPos = m_Vlist[i + 1];
            var vec = currentPos - nextPos;//今と、一つ先のベクトルから、進行方向を得る
            if (vec.magnitude < 0.01f) continue;  //あまり頂点の間が空いてないのであればスキップする
            var v = new Vector2(-vec.y, vec.x).normalized * m_LineWidth; //左右への幅ベクトルを得る

            //指定した横幅に広げる
            vertices.Add(currentPos - v);
            vertices.Add(currentPos + v);
        }

        var indices = new List<int>();
        for (int index = 0; index < vertices.Count - 2; index += 2)
        {
            indices.Add(index);
            indices.Add(index + 2);
            indices.Add(index + 3);
            indices.Add(index + 1);
        }

        m_Mesh.SetVertices(vertices);
        m_Mesh.SetIndices(indices.ToArray(), MeshTopology.Quads, 0);
    }

    /// <summary> 現在の見た目から当たり判定を作る </summary>
    private static List<Vector2> CreateMeshToPolyCollider(Mesh mesh)
    {
        var polyColliderPos = new List<Vector2>();
        //偶数を小さい順に
        for (int index = 0; index < mesh.vertices.Length; index += 2)
        {
            var pos = mesh.vertices[index];
            polyColliderPos.Add(pos);
        }
        //奇数を大きい順に
        for (int index = mesh.vertices.Length - 1; index > 0; index -= 2)
        {
            var pos = mesh.vertices[index];
            polyColliderPos.Add(pos);
        }
        return polyColliderPos;
    }
}