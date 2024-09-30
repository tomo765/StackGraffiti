using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

/// <summary> クレジット表示UI </summary>
public class CreditCanvas : MonoBehaviour
{
    [SerializeField] private VideoPlayer m_VideoPlayer;

    void Start()
    {
        StageDataUtility.StageDatas.BrowseCredit();
        DontDestroyCanvas.Instance.ChangeResultUIVisible(false);
        SoundManager.Instance.StopAllBGM();

        m_VideoPlayer.targetTexture.Release();
        m_VideoPlayer.SetLoopPointReached(() => ReturnTitle().FireAndForget());
        StartPlayVideo().FireAndForget();
    }

    private void Update()
    {
        if (InputExtension.Escape)
        {
            m_VideoPlayer.Stop();
            ReturnTitle().FireAndForget();
        }
    }

    private async Task StartPlayVideo()
    {
        m_VideoPlayer.time = 0;
        await TaskExtension.WaitUntiil(() => !SceneLoadExtension.IsFading);
        m_VideoPlayer.Play();
    }

    private async Task ReturnTitle()
    {
        if (SceneLoadExtension.IsFading) { return; }

        SoundManager.Instance.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeIn);
        await SceneLoadExtension.StartFadeIn();

        await SceneLoadExtension.FinishFadeIn(Config.SceneNames.Title);
        SoundManager.Instance.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeOut);

        await SceneLoadExtension.FadeOut();
    }
}

public static class VideoPlayerExtension
{
    public static void SetLoopPointReached(this VideoPlayer player, Action action)
    {
        player.loopPointReached += (player) =>
        {
            action();
        };
    }
}