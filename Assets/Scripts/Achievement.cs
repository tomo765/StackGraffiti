using Steamworks;
using UnityEngine;

// タイトルシーンのAchievementManagerオブジェクトでエンディングを見たか判定して、見た場合エンディングを見た実績解除がされるようにしているスクリプトです。
public class Achievement : MonoBehaviour
{
    void Start()
    {
        EndingAchievement();
    }

    //　エンディング(クレジット)を見たら実績解除
    public void EndingAchievement()
    {
        // エンディングアチーブメント解除
        if (!SteamManager.Initialized) { return; }

        Debug.Log("Inited");
        // エンディングを見た判定になったら
        if(StageDataUtility.StageDatas == null) {  return; }
        if (!StageDataUtility.StageDatas.SawCredit) { return; }
        Debug.Log("SawCredit");

        SteamUserStats.RequestCurrentStats();
        SteamUserStats.SetAchievement("ENDING_ACHIEVEMENT");
        SteamUserStats.StoreStats();

        SteamUserStats.RequestCurrentStats();
        bool isCleared; // 達成なら true, 未達成なら false が入ります
        SteamUserStats.GetAchievement(name.ToString(), out isCleared);
    }

    //private void ResetAchievementSample()
    //{
    //    SteamUserStats.ClearAchievement("ENDING_ACHIEVEMENT");
    //    SteamUserStats.StoreStats();
    //}
}
