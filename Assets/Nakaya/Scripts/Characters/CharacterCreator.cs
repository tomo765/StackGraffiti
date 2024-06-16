using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterCreator  //https://qiita.com/divideby_zero/items/491d18cbc91d7fabd700
{
    private static float m_Width = 0.1f;
    private static Mesh m_Mesh;
    private static List<Vector2> m_Vlist = new List<Vector2>();

    private static CharacterManager m_CreateChara;

    public static bool CanCreateChara => m_Mesh.vertexCount < 4;

    public static void OnClick(Vector2 position)  //Input.GetMouseButtonDown(0)
    {
        CreateCharacter(position);

        m_Mesh = new Mesh();
        m_Mesh.name = "CharaMesh";
        m_CreateChara.MeshFilter.mesh = m_Mesh;

        m_Vlist.Clear();
    }

    public static void OnHold(Vector2 position)  //Input.GetMouseButton(0) && targetObject != null
    {
        var pos = InputExtension.WorldMousePos - position;
        m_Vlist.Add(pos);

        CreateMesh();
    }

    public static void OnRelease()  //Input.GetMouseButtonUp(0) && targetObject != null
    {
        Finish();
    }

    public static void CreateCharacter(Vector2 position)
    {
        if (m_CreateChara != null) { return; }

        m_CreateChara = MonoBehaviour.Instantiate(GeneralSettings.Instance.Prehab.Character,
                                      position,
                                      Quaternion.identity);
    }

    public static void CreateOnStage()
    {
        GameManager.SetGameState(GameState.Playing);
        m_CreateChara.CreateOnStage("なまえ");
        
        m_CreateChara = null;
    }


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
            var v = new Vector2(-vec.y, vec.x).normalized * m_Width; //90度回転させてから正規化*widthで左右への幅ベクトルを得る

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

    private static void Finish()
    {
        if (CanCreateChara) { return; }

        m_CreateChara.Rb2D.useAutoMass = true;
        var polyColliderPos = CreateMeshToPolyCollider(m_Mesh);
        m_CreateChara.Poly2D.SetPath(0, polyColliderPos.ToArray());
    }

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
