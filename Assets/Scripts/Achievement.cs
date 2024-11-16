using Steamworks;
using UnityEngine;

// �^�C�g���V�[����AchievementManager�I�u�W�F�N�g�ŃG���f�B���O�����������肵�āA�����ꍇ�G���f�B���O���������щ����������悤�ɂ��Ă���X�N���v�g�ł��B
public class Achievement : MonoBehaviour
{
    void Start()
    {
        EndingAchievement();
    }

    //�@�G���f�B���O(�N���W�b�g)����������щ���
    public void EndingAchievement()
    {
        // �G���f�B���O�A�`�[�u�����g����
        if (!SteamManager.Initialized) { return; }

        Debug.Log("Inited");
        // �G���f�B���O����������ɂȂ�����
        if(StageDataUtility.StageDatas == null) {  return; }
        if (!StageDataUtility.StageDatas.SawCredit) { return; }
        Debug.Log("SawCredit");

        SteamUserStats.RequestCurrentStats();
        SteamUserStats.SetAchievement("ENDING_ACHIEVEMENT");
        SteamUserStats.StoreStats();

        SteamUserStats.RequestCurrentStats();
        bool isCleared; // �B���Ȃ� true, ���B���Ȃ� false ������܂�
        SteamUserStats.GetAchievement(name.ToString(), out isCleared);
    }

    //private void ResetAchievementSample()
    //{
    //    SteamUserStats.ClearAchievement("ENDING_ACHIEVEMENT");
    //    SteamUserStats.StoreStats();
    //}
}