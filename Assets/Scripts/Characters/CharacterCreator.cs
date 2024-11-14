using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static void SetCreateCharaTransform(Vector3 pos, Vector3 size)
    {
        if(!CanCreateChara) { return; }
        m_CreateChara.transform.position = pos;
        m_CreateChara.transform.localScale = size;
    }
    /// <summary> �L�������������ԂŃ}�E�X�����N���b�N�����u�Ԃ̏��� </summary>
    public static void OnClick(Vector2 position)
    {
        CreateCharacter(position);

        m_Mesh = new Mesh();
        m_CreateChara.MeshFilter.mesh = m_Mesh;

        m_Vlist.Clear();
    }

    /// <summary> �L�������������ԂŃ}�E�X�����z�[���h���Ă鎞�̏��� </summary>
    public static void OnHold(Vector2 position)
    {
        var pos = InputExtension.WorldMousePos - position;
        m_Vlist.Add(pos);

        CreateMesh();
    }

    /// <summary> �L�����������Ă���Ƃ��Ƀ}�E�X����b�������̏��� </summary>
    public static void OnRelease()
    {
        FinishWrite();
    }

    /// <summary> �L�����̐������� </summary>
    private static void CreateCharacter(Vector3 position)
    {
        if (m_CreateChara != null) { return; }
        position += GeneralSettings.Instance.Priorities.CreateCharaLayer;

        m_CreateChara = MonoBehaviour.Instantiate(GeneralSettings.Instance.Prehab.Character,
                                      position,
                                      Quaternion.identity);
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
        var polyColliderPos = CreateMeshToPolyCollider(m_Mesh);
        m_CreateChara.Poly2D.SetPath(0, polyColliderPos.ToArray());
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
}