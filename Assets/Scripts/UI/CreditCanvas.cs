using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

public class CreditCanvas : MonoBehaviour
{
    [SerializeField] private VideoPlayer m_VideoPlayer;

    void Start()
    {
        m_VideoPlayer.SetLoopPointReached(() => ReturnTitle().FireAndForget());
        StartPlayVideo().FireAndForget();
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

        await SceneLoadExtension.StartFadeWait(Config.SceneNames.Title);
        SoundManager.Instance.PlayNewSE(GeneralSettings.Instance.Sound.Fade1.FadeOut);

        await SceneLoadExtension.StartFadeOut();
    }
}

public static class VidoPlayerExtension
{
    public static void SetLoopPointReached(this VideoPlayer player, Action action)
    {
        player.loopPointReached += (player) =>
        {
            action();
        };
    }
}