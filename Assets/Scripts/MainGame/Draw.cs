using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Draw : MonoBehaviour
{
    [SerializeField] private float width;
    private GameObject targetObject;
    private Mesh mesh;
    private List<Vector2> vlist = new List<Vector2>();
    private GameObject newObject;
    bool canDraw = false;
    public Material drawMaterial;   // マテリアルの種類

    // メッシュを作るためのスクリプト  https://qiita.com/divideby_zero/items/491d18cbc91d7fabd700
    private void CreateMesh(Mesh mesh, List<Vector2> vlist)
    {
        mesh.Clear();

        var vCnt = vlist.Count;
        var vertices = new List<Vector3>();
        for (int i = 0; i < vCnt - 1; i++)
        {
            var currentPos = vlist[i];
            var nextPos = vlist[i + 1];
            var vec = currentPos - nextPos;//今と、一つ先のベクトルから、進行方向を得る
            if (vec.magnitude < 0.01f) continue;  //あまり頂点の間が空いてないのであればスキップする
            var v = new Vector2(-vec.y, vec.x).normalized * width; //90度回転させてから正規化*widthで左右への幅ベクトルを得る

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

        mesh.SetVertices(vertices);
        mesh.SetIndices(indices.ToArray(), MeshTopology.Quads, 0);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && canDraw)    // マウスの左クリックをしたら
        {
            targetObject = new GameObject("MeshObject");    // ゲーム上にMeshObject(ヒエラルキー上の名前)という名前のオブジェクトを新しく出現させる

            var meshRenderer = targetObject.AddComponent<MeshRenderer>();   // targetObject(ヒエラルキー上の名前はMeshObject)MeshRendererのコンポーネントを追加する
            // meshRenderer.sharedMaterial = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Sprites-Default.mat");   // UnityのAseetフォルダの中にあるスプライトのマテリアルを取得(エディタ上でのみ)
            meshRenderer.sharedMaterial = drawMaterial;
            // MeshFilterはアセットからメッシュを取得し、画面上でのレンダリングするために使う。
            var meshFilter = targetObject.AddComponent<MeshFilter>();   // targetObjectにMeshFilterを追加する。
            mesh = new Mesh();      // 新しくMeshを作る
            meshFilter.mesh = mesh;     // MeshFilterの中に、作ったMeshを入れる

            // 既にオブジェクトが存在していれば破棄
            if (newObject != null)
            {
                newObject = GameObject.Find("new Object");
                Destroy(this.newObject);
            }
            vlist.Clear();  // リストの中身をクリアする
        }

        if (Input.GetMouseButton(0) && targetObject != null && canDraw)
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            vlist.Add(pos);
            CreateMesh(mesh, vlist);
        }

        // 左クリックを離して、書いたやつが存在してる時
        if (Input.GetMouseButtonUp(0) && targetObject != null)
        {
            Finish();
            newObject = GameObject.Find("MeshObject");
            newObject.name = "new Object";
        }
    }

    // 線を書き終わったら呼び出されるやつ
    private void Finish()
    {
        if (mesh.vertexCount < 4)
        {
            Destroy(targetObject);
            return;
        }

        var polyColliderPos = CreateMeshToPolyCollider(mesh);
        var polyCollider = targetObject.AddComponent<PolygonCollider2D>();
        polyCollider.SetPath(0, polyColliderPos.ToArray());
        targetObject = null;

    }

    // 触ったら危険
    private List<Vector2> CreateMeshToPolyCollider(Mesh mesh)
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

    // DrawArea内で描いているかどうか
    public void OnPointerEnter()
    {
        canDraw = true;
    }

    public void OnPointExit()
    {
        canDraw = false;
    }

}
