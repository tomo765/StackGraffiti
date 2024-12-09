using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary> �L�����̐����Ɋւ���ݒ�⏈�� </summary>
public static class CharacterCreator  //�Q�l�T�C�g : https://qiita.com/divideby_zero/items/491d18cbc91d7fabd700
{
    private static float m_LineWidth = 0.1f;
    private static Mesh m_Mesh;
    private static List<Vector2> m_Vlist = new List<Vector2>();

    private static CharacterManager m_CreateChara;

    public static bool CanCreateChara => m_CreateChara != null && m_Mesh.vertexCount >= 4;

    /// <summary> �L����������UI�̕\����Ԃ̕ύX�Ɠ����ɁA�����Ă���L�����̕\����Ԃ��ύX������ </summary>
    public static void SetCreatingCharaVisible(bool b)
    {
        if(m_CreateChara == null) { return; }
        m_CreateChara.gameObject.SetActive(b);
    }
    public static void SetCharacterTransform(Vector3 pos, Vector3 size)
    {
        if (!CanCreateChara) { return; }
        m_CreateChara.transform.position = pos;
        m_CreateChara.transform.localScale = size;
    }

    /// <summary> �L�������������ԂŃ}�E�X�����N���b�N�����u�Ԃ̏��� </summary>
    public static void OnClick(Vector2 position)
    {
        CreateCharacter(position);

        m_Mesh = new Mesh();
        m_CreateChara.MeshFilter.mesh = m_Mesh;
    }

    /// <summary> �L�������������ԂŃ}�E�X�����z�[���h���Ă鎞�̏��� </summary>
    public static void OnHold(Vector2 position)
    {
        AddMesh(InputExtension.WorldMousePos - position);
        
        //var pos = InputExtension.WorldMousePos - position;
        //m_Vlist.Add(pos);
        //CreateMesh();
    }

    /// <summary> �L�����������Ă���Ƃ��Ƀ}�E�X����b�������̏��� </summary>
    public static void OnRelease()
    {
        FinishWrite();
    }

    /// <summary> �L�����̐������� </summary>
    private static void CreateCharacter(Vector3 position)
    {
        if (m_CreateChara != null)
        {
            for(int i = m_CreateChara.Collisions.Count - 1; i >= 0; i--)
            {
                MonoBehaviour.Destroy(m_CreateChara.Collisions[i].gameObject);
            }
            m_CreateChara.Collisions.Clear();
        }
        else
        {
            position += GeneralSettings.Instance.Priorities.CreateCharaLayer;

            m_CreateChara = MonoBehaviour.Instantiate(GeneralSettings.Instance.Prehab.Character,
                                          position,
                                          Quaternion.identity);
        }
        m_Vlist.Clear();
    }
    /// <summary> �L�����𑀍�\�ɂ��邽�߂̏��� </summary>
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

    /// <summary> �L�����������I��������̏��� </summary>
    private static void FinishWrite()
    {
        if (!CanCreateChara) { return; }

        m_CreateChara.Rb2D.useAutoMass = true;
        SetCollider();
    }

    /// <summary> �L�����̌����ڂ��X�V���� </summary>
    private static void CreateMesh()
    {
        m_Mesh.Clear();

        var vCnt = m_Vlist.Count;
        var vertices = new List<Vector3>();
        for (int i = 0; i < vCnt - 1; i++)
        {
            var currentPos = m_Vlist[i];
            var nextPos = m_Vlist[i + 1];
            var vec = currentPos - nextPos;//���ƁA���̃x�N�g������A�i�s�����𓾂�
            if (vec.magnitude < 0.01f) continue;  //���܂蒸�_�̊Ԃ��󂢂ĂȂ��̂ł���΃X�L�b�v����
            var v = new Vector2(-vec.y, vec.x).normalized * m_LineWidth; //���E�ւ̕��x�N�g���𓾂�

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

        m_Mesh.SetVertices(vertices);
        m_Mesh.SetIndices(indices.ToArray(), MeshTopology.Quads, 0);
    }

    private static void AddMesh(Vector2 addVec)
    {
        if(m_Vlist.Count != 0 && (addVec - m_Vlist.Last()).magnitude <= 0.1f) { return; }

        SetVertices();
        SetIndices();

        void SetVertices()
        {
            m_Vlist.Add(addVec);
            if(m_Vlist.Count < 2) { return; }

            var vertices = m_Mesh.vertices.ToList();

            var delta = m_Vlist[m_Vlist.Count - 2] - m_Vlist[m_Vlist.Count - 1];
            if (delta.magnitude < 0.01f) { return; }

            var normalVec = new Vector2(-delta.y, delta.x).normalized * m_LineWidth;

            vertices.Add(m_Vlist[m_Vlist.Count - 2] - normalVec);
            vertices.Add(m_Vlist[m_Vlist.Count - 2] + normalVec);

            m_Mesh.SetVertices(vertices);
        }
        void SetIndices()
        {
            var vertCount = m_Mesh.vertices.Length;
            if (vertCount < 4) { return; }
            var indices = m_Mesh.GetIndices(0).ToList();

            indices.Add(vertCount - 4);
            indices.Add(vertCount - 2);
            indices.Add(vertCount - 1);
            indices.Add(vertCount - 3);

            m_Mesh.SetIndices(indices.ToArray(), MeshTopology.Quads, 0);
        }
    }

    /// <summary> ���݂̌����ڂ��瓖���蔻������ </summary>
    private static List<Vector2> CreateMeshToPolyCollider(Mesh mesh)
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

    private static void SetCollider()
    {
        for (int i = 0; i < m_Vlist.Count-2; i++)
        {
            var col = new GameObject("Collider").AddComponent<BoxCollider2D>();

            col.enabled = false;
            col.gameObject.tag = m_CreateChara.gameObject.tag;
            col.gameObject.layer = m_CreateChara.gameObject.layer;

            col.transform.position = (m_Vlist[i] + m_Vlist[i + 1]) / 2f + (Vector2)m_CreateChara.transform.position;
            col.size = new Vector2((m_Vlist[i + 1] - m_Vlist[i]).magnitude, m_LineWidth * 2);
            var angle = Mathf.Atan2(m_Vlist[i + 1].y - m_Vlist[i].y, m_Vlist[i + 1].x - m_Vlist[i].x) * Mathf.Rad2Deg;
            col.transform.rotation = Quaternion.Euler(0, 0, angle);

            m_CreateChara.Collisions.Add(col);
            col.transform.SetParent(m_CreateChara.ColliderContainer);
        }
    }
}