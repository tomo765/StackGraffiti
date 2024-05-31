using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Draw: MonoBehaviour
{
    [SerializeField] private float width;
    private GameObject targetObject;
    private Mesh mesh;
    private List<Vector2> vlist = new List<Vector2>();
    private GameObject newObject;
    bool canDraw = false;
    public Material drawMaterial;   // �}�e���A���̎��

    // ���b�V������邽�߂̃X�N���v�g
    private void CreateMesh(Mesh mesh, List<Vector2> vlist)
    {
        mesh.Clear();

        var vCnt = vlist.Count;
        var vertices = new List<Vector3>();
        for (int i = 0; i < vCnt - 1; i++)
        {
            var currentPos = vlist[i];
            var nextPos = vlist[i + 1];
            var vec = currentPos - nextPos;//���ƁA���̃x�N�g������A�i�s�����𓾂�
            if (vec.magnitude < 0.01f) continue;  //���܂蒸�_�̊Ԃ��󂢂ĂȂ��̂ł���΃X�L�b�v����
            var v = new Vector2(-vec.y, vec.x).normalized * width; //90�x��]�����Ă��琳�K��*width�ō��E�ւ̕��x�N�g���𓾂�

            //�w�肵�������ɍL����
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
        if (Input.GetMouseButtonDown(0) && canDraw)    // �}�E�X�̍��N���b�N��������
        //if (Input.GetMouseButtonDown(0))    // �}�E�X�̍��N���b�N��������
        {
            targetObject = new GameObject("MeshObject");    // �Q�[�����MeshObject(�q�G�����L�[��̖��O)�Ƃ������O�̃I�u�W�F�N�g��V�����o��������

            var meshRenderer = targetObject.AddComponent<MeshRenderer>();   // targetObject(�q�G�����L�[��̖��O��MeshObject)MeshRenderer�̃R���|�[�l���g��ǉ�����
            // meshRenderer.sharedMaterial = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Sprites-Default.mat");   // Unity��Aseet�t�H���_�̒��ɂ���X�v���C�g�̃}�e���A�����擾(�G�f�B�^��ł̂�)
            meshRenderer.sharedMaterial = drawMaterial;
            // MeshFilter�̓A�Z�b�g���烁�b�V�����擾���A��ʏ�ł̃����_�����O���邽�߂Ɏg���B
            var meshFilter = targetObject.AddComponent<MeshFilter>();   // targetObject��MeshFilter��ǉ�����B
            mesh = new Mesh();      // �V����Mesh�����
            meshFilter.mesh = mesh;     // MeshFilter�̒��ɁA�����Mesh������

            // ���ɃI�u�W�F�N�g�����݂��Ă���Δj��
            if (newObject != null)
            {
                newObject = GameObject.Find("new Object");
                Destroy(this.newObject);
            }
            vlist.Clear();  // ���X�g�̒��g���N���A����

        }

        if (Input.GetMouseButton(0) && targetObject != null && canDraw)
        {

            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            vlist.Add(pos);
            CreateMesh(mesh, vlist);
        }

        // ���N���b�N�𗣂��āA������������݂��Ă鎞
        if (Input.GetMouseButtonUp(0) && targetObject != null)
        {

            // private void Finish()�����Ăяo������B
            Finish();
            newObject = GameObject.Find("MeshObject");
            newObject.name = "new Object";
        }
    }

    // ���������I�������Ăяo�������
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

    // �G������댯
    private List<Vector2> CreateMeshToPolyCollider(Mesh mesh)
    {
        var polyColliderPos = new List<Vector2>();
        //����������������
        for (int index = 0; index < mesh.vertices.Length; index += 2)
        {
            var pos = mesh.vertices[index];
            polyColliderPos.Add(pos);
        }
        //���傫������
        for (int index = mesh.vertices.Length - 1; index > 0; index -= 2)
        {
            var pos = mesh.vertices[index];
            polyColliderPos.Add(pos);
        }
        return polyColliderPos;
    }

    // DrawArea���ŕ`���Ă��邩�ǂ���
    public void OnPointerEnter()
    {
        canDraw = true;
    }

    public void OnPointExit()
    {
        canDraw = false;
    }

}
